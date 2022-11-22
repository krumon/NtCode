using System;
using System.Diagnostics;
using System.Reflection;

namespace Nt.Core.Data
{
    /// <summary>
    /// Represents description of any service.
    /// </summary>
    public class ServiceDescriptor
    {

        #region Private members

        private Type[] _implementationParameters = null;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the ninjascript service type.
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// Gets the ninjascript service implementation type. 
        /// Can be null.
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// Gets the ninjascript service implementation instance. 
        /// Can be null.
        /// </summary>
        public object ImplementationInstance { get; }

        /// <summary>
        /// Gets the delegate to construct an object instance.
        /// </summary>
        public Func<IServiceProvider, object> ImplementationFactory { get; }

        /// <summary>
        /// Gets the ninjascript service implementation instance. 
        /// Can be null.
        /// </summary>
        public Type[] ImplementationParameters
        { 
            get
            {
                if (_implementationParameters == null)
                    _implementationParameters = GetImplementationParameters();
                
                return _implementationParameters;
            }  
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="implementationType">The <see cref="Type"/> implementing the service.</param>
        public ServiceDescriptor(
            Type serviceType,
            Type implementationType)
            : this(serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="implementationType">The <see cref="Type"/> implementing the service.</param>
        public ServiceDescriptor(
            Type serviceType,
            object implementationInstance)
            : this(serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            ImplementationInstance = implementationInstance ?? throw new ArgumentNullException(nameof(implementationInstance));
        }

        private ServiceDescriptor(Type serviceType)
        {
            ServiceType = serviceType;
        }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public override string ToString()
        {
            string lifetime = $"{nameof(ServiceType)}: {ServiceType} "; // string debe poder ser null

            if (ImplementationType != null)
                return lifetime + $"{nameof(ImplementationType)}: {ImplementationType}";

            return lifetime + $"{nameof(ImplementationInstance)}: {ImplementationInstance}";
        }

        internal Type GetImplementationType()
        {
            if (ImplementationType != null)
                return ImplementationType;

            else if (ImplementationInstance != null)
                return ImplementationInstance.GetType();

            else if (ImplementationFactory != null)
            {
                Type[] typeArguments = ImplementationFactory.GetType().GenericTypeArguments;

                Debug.Assert(typeArguments.Length == 2);

                return typeArguments[1];
            }

            Debug.Assert(false, "ImplementationType or ImplementationInstance must be non null");
            return null;
        }

        internal Type[] GetImplementationParameters()
        {
            ConstructorInfo[] constructors = GetImplementationType().GetConstructors();

            if (constructors.Length != 1)
                throw new Exception("The public constructor of the implementationType must be unique");

            ParameterInfo[] parameters = constructors[0].GetParameters();
            
            if (parameters.Length > 0)
            {
                Type[] newTypes = new Type[parameters.Length];
                for (int i= 0; i < newTypes.Length; i++)
                    newTypes[i] = parameters[i].ParameterType;

                return newTypes;
            }

            return Type.EmptyTypes;
        }

        internal object GetCallSite()
        {
            // ServiceCallSite callSite;
            object service = null;
            //if (ImplementationInstance != null)
            //{
            //    callSite = new ConstantCallSite(ServiceType, ImplementationInstance);
            //}
            //else if (ImplementationFactory != null)
            //{
            //    callSite = new FactoryCallSite(ServiceType, ImplementationFactory);
            //}
            //else if (ImplementationType != null)
            //{
            //    callSite = CreateConstructorCallSite(ServiceType, ImplementationType);
            //}
            //else
            //{
            //    throw new InvalidOperationException(SR.InvalidServiceDescriptor);
            //}

            return service;

            //return _callSiteCache[callSiteKey] = callSite;
        }

        internal void CreateConstructorCallSite()
        {
            //try
            //{
            //    callSiteChain.Add(serviceType, implementationType);
            //    ConstructorInfo[] constructors = implementationType.GetConstructors();

            //    ServiceCallSite[] parameterCallSites = null;

            //    if (constructors.Length == 0)
            //    {
            //        throw new InvalidOperationException(SR.Format(SR.NoConstructorMatch, implementationType));
            //    }
            //    else if (constructors.Length == 1)
            //    {
            //        ConstructorInfo constructor = constructors[0];
            //        ParameterInfo[] parameters = constructor.GetParameters();
            //        if (parameters.Length == 0)
            //        {
            //            return new ConstructorCallSite(lifetime, serviceType, constructor);
            //        }

            //        parameterCallSites = CreateArgumentCallSites(
            //            implementationType,
            //            callSiteChain,
            //            parameters,
            //            throwIfCallSiteNotFound: true);

            //        return new ConstructorCallSite(lifetime, serviceType, constructor, parameterCallSites);
            //    }

            //    Array.Sort(constructors,
            //        (a, b) => b.GetParameters().Length.CompareTo(a.GetParameters().Length));

            //    ConstructorInfo bestConstructor = null;
            //    HashSet<Type> bestConstructorParameterTypes = null;
            //    for (int i = 0; i < constructors.Length; i++)
            //    {
            //        ParameterInfo[] parameters = constructors[i].GetParameters();

            //        ServiceCallSite[] currentParameterCallSites = CreateArgumentCallSites(
            //            implementationType,
            //            callSiteChain,
            //            parameters,
            //            throwIfCallSiteNotFound: false);

            //        if (currentParameterCallSites != null)
            //        {
            //            if (bestConstructor == null)
            //            {
            //                bestConstructor = constructors[i];
            //                parameterCallSites = currentParameterCallSites;
            //            }
            //            else
            //            {
            //                // Since we're visiting constructors in decreasing order of number of parameters,
            //                // we'll only see ambiguities or supersets once we've seen a 'bestConstructor'.

            //                if (bestConstructorParameterTypes == null)
            //                {
            //                    bestConstructorParameterTypes = new HashSet<Type>();
            //                    foreach (ParameterInfo p in bestConstructor.GetParameters())
            //                    {
            //                        bestConstructorParameterTypes.Add(p.ParameterType);
            //                    }
            //                }

            //                foreach (ParameterInfo p in parameters)
            //                {
            //                    if (!bestConstructorParameterTypes.Contains(p.ParameterType))
            //                    {
            //                        // Ambiguous match exception
            //                        throw new InvalidOperationException(string.Join(
            //                            Environment.NewLine,
            //                            SR.Format(SR.AmbiguousConstructorException, implementationType),
            //                            bestConstructor,
            //                            constructors[i]));
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    if (bestConstructor == null)
            //    {
            //        throw new InvalidOperationException(
            //            SR.Format(SR.UnableToActivateTypeException, implementationType));
            //    }
            //    else
            //    {
            //        Debug.Assert(parameterCallSites != null);
            //        return new ConstructorCallSite(lifetime, serviceType, bestConstructor, parameterCallSites);
            //    }
            //}
            //finally
            //{
            //    callSiteChain.Remove(serviceType);
            //}
        }

        internal void CreateArgumentsCallSite()
        {
            //var parameterCallSites = new ServiceCallSite[parameters.Length];
            //for (int index = 0; index < parameters.Length; index++)
            //{
            //    Type parameterType = parameters[index].ParameterType;
            //    ServiceCallSite callSite = GetCallSite(parameterType, callSiteChain);

            //    if (callSite == null && ParameterDefaultValue.TryGetDefaultValue(parameters[index], out object defaultValue))
            //    {
            //        callSite = new ConstantCallSite(parameterType, defaultValue);
            //    }

            //    if (callSite == null)
            //    {
            //        if (throwIfCallSiteNotFound)
            //        {
            //            throw new InvalidOperationException(SR.Format(SR.CannotResolveService,
            //                parameterType,
            //                implementationType));
            //        }

            //        return null;
            //    }

            //    parameterCallSites[index] = callSite;
            //}

            //return parameterCallSites;
        }

        internal void GetParameterDefaultValue()
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/> and <typeparamref name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor CreateInstance<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>();
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="service"/> and <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor CreateInstance(
            Type service,
            Type implementationType)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (implementationType == null)
                throw new ArgumentNullException(nameof(implementationType));

            return Describe(service, implementationType);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="service"/> and <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor CreateInstance(
            Type service,
            object implementationInstance)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (implementationInstance == null)
                throw new ArgumentNullException(nameof(implementationInstance));

            return Describe(service, implementationInstance);
        }

        private static ServiceDescriptor Describe<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(
                typeof(TService),
                typeof(TImplementation));
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/> and <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        private static ServiceDescriptor Describe(
            Type serviceType,
            Type implementationType)
        {
            return new ServiceDescriptor(serviceType, implementationType);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/> and <paramref name="implementationInstance"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="implementationInstance">The instance of the implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        private static ServiceDescriptor Describe(
            Type serviceType,
            object implementationInstance)
        {
            return new ServiceDescriptor(serviceType, implementationInstance);
        }

        #endregion

    }
}
