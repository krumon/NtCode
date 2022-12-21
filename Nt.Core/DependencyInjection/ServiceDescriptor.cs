using System;
using System.Diagnostics;
using System.Reflection;

namespace Nt.Core.DependencyInjection
{
    /// <summary>
    /// Represents description of any service.
    /// </summary>
    public class ServiceDescriptor
    {

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
        /// Initializes a new instance of <see cref="ServiceDescriptor"/> with the specified <paramref name="factory"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="factory">A factory used for creating service instances.</param>
        public ServiceDescriptor(
            Type serviceType,
            Func<IServiceProvider, object> factory)
            : this(serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            ImplementationFactory = factory ?? throw new ArgumentNullException(nameof(factory));
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

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Singleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>();
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="service"/> and <paramref name="implementationType"/>
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Singleton(
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
        /// <paramref name="service"/> and <paramref name="implementationInstance"/>
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationInstance">Instance of the service type.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Singleton(
            Type service,
            object implementationInstance)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (implementationInstance == null)
                throw new ArgumentNullException(nameof(implementationInstance));

            return Describe(service, implementationInstance);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Singleton<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
                throw new ArgumentNullException(nameof(implementationFactory));

            return Describe(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Singleton<TService>(
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (implementationFactory == null)
                throw new ArgumentNullException(nameof(implementationFactory));

            return Describe(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="service"/>, <paramref name="implementationFactory"/>.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Singleton(
            Type service,
            Func<IServiceProvider, object> implementationFactory)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (implementationFactory == null)
                throw new ArgumentNullException(nameof(implementationFactory));

            return Describe(service, implementationFactory);
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
