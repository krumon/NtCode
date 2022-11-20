using Nt.Core.Services.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Nt.Core.Data
{
    public sealed class CallSiteFactory
    {
        private readonly ServiceDescriptor[] _descriptors;
        private readonly ConcurrentDictionary<ServiceCacheKey, ServiceCallSite> _callSiteCache = new ConcurrentDictionary<ServiceCacheKey, ServiceCallSite>();
        private readonly ConcurrentDictionary<Type, object> _callSiteLocks = new ConcurrentDictionary<Type, object>();

        public CallSiteFactory(ICollection<ServiceDescriptor> descriptors)
        {
            _descriptors = new ServiceDescriptor[descriptors.Count];
            descriptors.CopyTo(_descriptors, 0);

            Populate();
        }

        internal ServiceDescriptor[] Descriptors => _descriptors;

        private void Populate()
        {
            foreach (ServiceDescriptor descriptor in _descriptors)
            {
                Type serviceType = descriptor.ServiceType;
                if (serviceType.IsGenericTypeDefinition)
                {
                    Type implementationType = descriptor.ImplementationType;

                    if (implementationType == null || !implementationType.IsGenericTypeDefinition)
                    {
                        throw new ArgumentException("OpenGenericServiceRequiresOpenGenericImplementation");
                    }

                    if (implementationType.IsAbstract || implementationType.IsInterface)
                    {
                        throw new ArgumentException("TypeCannotBeActivated");
                    }

                    Type[] serviceTypeGenericArguments = serviceType.GetGenericArguments();
                    Type[] implementationTypeGenericArguments = implementationType.GetGenericArguments();
                    if (serviceTypeGenericArguments.Length != implementationTypeGenericArguments.Length)
                    {
                        throw new ArgumentException("ArityOfOpenGenericServiceNotEqualArityOfOpenGenericImplementation");
                    }

                }
                else if (descriptor.ImplementationInstance == null)
                {
                    Debug.Assert(descriptor.ImplementationType != null);
                    Type implementationType = descriptor.ImplementationType;

                    if (implementationType.IsGenericTypeDefinition ||
                        implementationType.IsAbstract ||
                        implementationType.IsInterface)
                    {
                        throw new ArgumentException("TypeCannotBeActivated");
                    }
                }

            }
        }

        private ServiceCallSite ConstructorCallSite(
            Type serviceType,
            Type implementationType)
        {
            try
            {
                ConstructorInfo[] constructors = implementationType.GetConstructors();
                ParameterInfo[] parameters;
                if (constructors.Length == 0)
                {
                    throw new InvalidOperationException("NoConstructorMatch");
                }
                else if (constructors.Length == 1)
                {
                    ConstructorInfo constructor = constructors[0];
                    parameters = constructor.GetParameters();
                    if (parameters.Length == 0)
                    {
                        return new ConstructorCallSite(serviceType, constructor);
                    }

                    return new ConstructorCallSite(serviceType, constructor, parameters);
                }

                Array.Sort(constructors,
                    (a, b) => b.GetParameters().Length.CompareTo(a.GetParameters().Length));

                ConstructorInfo bestConstructor = null;
                HashSet<Type> bestConstructorParameterTypes = null;
                for (int i = 0; i < constructors.Length; i++)
                {
                    parameters = constructors[i].GetParameters();

                    currentParameterCallSites = CreateArgumentCallSites(
                        implementationType,
                        parameters);

                    if (currentParameterCallSites != null)
                    {
                        if (bestConstructor == null)
                        {
                            bestConstructor = constructors[i];
                            parameterCallSites = currentParameterCallSites;
                        }
                        else
                        {
                            // Since we're visiting constructors in decreasing order of number of parameters,
                            // we'll only see ambiguities or supersets once we've seen a 'bestConstructor'.

                            if (bestConstructorParameterTypes == null)
                            {
                                bestConstructorParameterTypes = new HashSet<Type>();
                                foreach (ParameterInfo p in bestConstructor.GetParameters())
                                {
                                    bestConstructorParameterTypes.Add(p.ParameterType);
                                }
                            }

                            foreach (ParameterInfo p in parameters)
                            {
                                if (!bestConstructorParameterTypes.Contains(p.ParameterType))
                                {
                                    // Ambiguous match exception
                                    throw new InvalidOperationException(string.Join(
                                        Environment.NewLine,
                                        "AmbiguousConstructorException",
                                        bestConstructor,
                                        constructors[i]));
                                }
                            }
                        }
                    }
                }

                if (bestConstructor == null)
                {
                    throw new InvalidOperationException("UnableToActivateTypeException");
                }
                else
                {
                    Debug.Assert(parameters != null);
                    return new ConstructorCallSite(serviceType, bestConstructor, parameters);
                }
            }
            finally
            {
                callSiteChain.Remove(serviceType);
            }
        }

        private Type[] CreateArgumentCallSites(
            Type implementationType,
            ParameterInfo[] parameters)
        {
            var parameterCallSites = new ServiceCallSite[parameters.Length];
            for (int index = 0; index < parameters.Length; index++)
            {
                Type parameterType = parameters[index].ParameterType;
                ServiceCallSite callSite = GetCallSite(parameterType, callSiteChain);
                object defaultValue = null;

                if (callSite == null && ParameterDefaultValue.TryGetDefaultValue(parameters[index], out defaultValue))
                {
                    callSite = new ConstantCallSite(parameterType, defaultValue);
                }

                if (callSite == null)
                {
                    if (throwIfCallSiteNotFound)
                    {
                        throw new InvalidOperationException("CannotResolveService");
                    }

                    return null;
                }

                parameterCallSites[index] = callSite;
            }

            return parameterCallSites;
        }


        public bool IsService(Type serviceType)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            // Querying for an open generic should return false (they aren't resolvable)
            if (serviceType.IsGenericTypeDefinition)
            {
                return false;
            }

            if (_descriptorLookup.ContainsKey(serviceType))
            {
                return true;
            }

            if (serviceType.IsConstructedGenericType && serviceType.GetGenericTypeDefinition() is Type genericDefinition)
            {
                // We special case IEnumerable since it isn't explicitly registered in the container
                // yet we can manifest instances of it when requested.
                return genericDefinition == typeof(IEnumerable<>) || _descriptorLookup.ContainsKey(genericDefinition);
            }

            // These are the built in service types that aren't part of the list of service descriptors
            // If you update these make sure to also update the code in ServiceProvider.ctor
            return serviceType == typeof(IServiceProvider) ||
                   serviceType == typeof(IServiceScopeFactory) ||
                   serviceType == typeof(IServiceProviderIsService);
        }

        internal ServiceCallSite GetCallSite(Type serviceType, CallSiteChain callSiteChain) =>
            _callSiteCache.TryGetValue(new ServiceCacheKey(serviceType, DefaultSlot), out ServiceCallSite site) ? site :
            CreateCallSite(serviceType, callSiteChain);

    }
}