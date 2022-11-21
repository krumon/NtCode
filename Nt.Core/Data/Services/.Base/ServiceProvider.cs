using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Nt.Core.Data
{
    /// <summary>
    /// Default <see cref="IServiceProvider"/>.
    /// </summary>
    public sealed class ServiceProvider : IServiceProvider, IDisposable, IAsyncDisposable
    {

        #region Private members

        // Check the service provider is disposed.
        private bool _disposed;
        // Store the service descriptors.
        private ServiceDescriptor[] _descriptors;
        // Collection of the realized services.
        private ConcurrentDictionary<Type, object> _realizedServices;
        // The factory of the calls.
        internal CallSiteFactory CallSiteFactory { get; }

        //// Collection of the realized services.
        //private ConcurrentDictionary<Type, Func<Type, object>> _realizedServices;
        //// Delagate to create the service
        //private Func<Type, object> _createServiceAccessor;

        //private readonly CallSiteValidator _callSiteValidator;

        //private object _createService;

        #endregion

        #region Public properties

        #endregion

        #region Constructor

        internal ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors, ServiceProviderOptions options)
        {
            // Initilize the realized services.
            _realizedServices = new ConcurrentDictionary<Type, object>();
            // Initialize the call site factory.
            CallSiteFactory = new CallSiteFactory(serviceDescriptors);

            //// The list of built in services that aren't part of the list of service descriptors
            //// keep this in sync with CallSiteFactory.IsService
            //CallSiteFactory.Add(typeof(INinjascriptServiceProvider), new ServiceProviderCallSite());
            //CallSiteFactory.Add(typeof(IServiceScopeFactory), new ConstantCallSite(typeof(IServiceScopeFactory), Root));
            //CallSiteFactory.Add(typeof(IServiceProviderIsService), new ConstantCallSite(typeof(IServiceProviderIsService), CallSiteFactory));

            // Default is false
            if (options.ValidateOnBuild)
            {
                List<Exception> exceptions = null;
                foreach (ServiceDescriptor serviceDescriptor in serviceDescriptors)
                {
                    try
                    {
                        ValidateService(serviceDescriptor);
                    }
                    catch (Exception e)
                    {
                        exceptions = exceptions ?? new List<Exception>();
                        exceptions.Add(e);
                    }
                }

                if (exceptions != null)
                {
                    throw new AggregateException("Some services are not able to be constructed", exceptions.ToArray());
                }
            }

            //DependencyInjectionEventSource.Log.ServiceProviderBuilt(this);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service to get.</param>
        /// <returns>The service that was produced.</returns>
        public object GetService(Type serviceType)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ServiceProvider));
            var realizedService = _realizedServices.GetOrAdd(serviceType, CreateService);
            Debug.Assert(realizedService is null);
            return realizedService;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            DisposeCore();
        }

        /// <inheritdoc/>
        public ValueTask DisposeAsync()
        {
            DisposeCore();
            return default;
        }

        #endregion

        #region Private methods

        private void ValidateService(ServiceDescriptor descriptor)
        {
            Type serviceType = descriptor.ServiceType;
            
            if (serviceType.IsGenericType && !serviceType.IsConstructedGenericType)
                return;

            try
            {
                ServiceCallSite callSite = CallSiteFactory.GetCallSite(descriptor);
                if (callSite == null)
                    throw new Exception();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error while validating the service descriptor '{descriptor}': {e.Message}", e);
            }
        }

        private object CreateService(Type serviceType)
        {
            ServiceDescriptor descriptor = GetServiceDescriptor(serviceType);
            if (descriptor == null)
                return null;

            else if(descriptor.ImplementationInstance != null)
                return descriptor.ImplementationInstance;

            else if(descriptor.ImplementationType != null)
            {
                ConstructorCallSite constructor = GetConstructor(descriptor);
            }

            return null;
        }

        private ConstructorCallSite GetConstructor(ServiceDescriptor serviceDescriptor)
        {
            try
            {
                ConstructorInfo[] constructors = serviceDescriptor.ImplementationType.GetConstructors();
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
                        return new ConstructorCallSite(serviceDescriptor.ServiceType, constructor);
                    }

                    return new ConstructorCallSite(serviceDescriptor.ServiceType, constructor, parameters);
                }

                //Array.Sort(constructors,
                //    (a, b) => b.GetParameters().Length.CompareTo(a.GetParameters().Length));

                //ConstructorInfo bestConstructor = null;
                //HashSet<Type> bestConstructorParameterTypes = null;
                //for (int i = 0; i < constructors.Length; i++)
                //{
                //    parameters = constructors[i].GetParameters();

                //    currentParameterCallSites = CreateArgumentCallSites(
                //        implementationType,
                //        parameters);

                //    if (currentParameterCallSites != null)
                //    {
                //        if (bestConstructor == null)
                //        {
                //            bestConstructor = constructors[i];
                //            parameterCallSites = currentParameterCallSites;
                //        }
                //        else
                //        {
                //            // Since we're visiting constructors in decreasing order of number of parameters,
                //            // we'll only see ambiguities or supersets once we've seen a 'bestConstructor'.

                //            if (bestConstructorParameterTypes == null)
                //            {
                //                bestConstructorParameterTypes = new HashSet<Type>();
                //                foreach (ParameterInfo p in bestConstructor.GetParameters())
                //                {
                //                    bestConstructorParameterTypes.Add(p.ParameterType);
                //                }
                //            }

                //            foreach (ParameterInfo p in parameters)
                //            {
                //                if (!bestConstructorParameterTypes.Contains(p.ParameterType))
                //                {
                //                    // Ambiguous match exception
                //                    throw new InvalidOperationException(string.Join(
                //                        Environment.NewLine,
                //                        "AmbiguousConstructorException",
                //                        bestConstructor,
                //                        constructors[i]));
                //                }
                //            }
                //        }
                //    }
                //}

                //if (bestConstructor == null)
                //{
                //    throw new InvalidOperationException("UnableToActivateTypeException");
                //}
                //else
                //{
                //    Debug.Assert(parameters != null);
                //    return new ConstructorCallSite(serviceType, bestConstructor, parameters);
                //}
            }
            finally
            {
                //callSiteChain.Remove(serviceType);
            }

            return null;
        }

        private object[] CreateArguments()
        {
            return Array.Empty<Object>();
        }

        private ServiceDescriptor GetServiceDescriptor(Type serviceType)
        {
            foreach (ServiceDescriptor descriptor in _descriptors)
            {
                if (descriptor.ServiceType == serviceType)
                    return descriptor;
            }

            return null;
        }

        private void DisposeCore()
        {
            _disposed = true;
        }

        #endregion

    }
}
