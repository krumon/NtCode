﻿using System;
using System.Diagnostics;

namespace Nt.Core.Services
{
    public class NinjascriptServiceDescriptor
    {

        #region Public properties

        /// <summary>
        /// Gets the service lifetime.
        /// </summary>
        public ServiceLifetime Lifetime { get; }

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
        /// Gets the action thats factory (fabrica) the ninjascript service.
        /// Can be null.
        /// </summary>
        public Func<INinjascriptServiceProvider, object> ImplementationFactory { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="NinjascriptServiceDescriptor"/> with the specified <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="implementationType">The <see cref="Type"/> implementing the service.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the service.</param>
        public NinjascriptServiceDescriptor(
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifeTime)
            : this(serviceType, lifeTime)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NinjascriptServiceDescriptor"/> with the specified <paramref name="instance"/>
        /// as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="instance">The instance implementing the service.</param>
        public NinjascriptServiceDescriptor(
            Type serviceType,
            object instance)
            : this(serviceType, ServiceLifetime.Singleton)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            ImplementationInstance = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NinjascriptServiceDescriptor"/> with the specified <paramref name="factory"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="factory">A factory used for creating service instances.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the service.</param>
        public NinjascriptServiceDescriptor(
            Type serviceType,
            Func<INinjascriptServiceProvider, object> factory,
            ServiceLifetime lifetime)
            : this(serviceType, lifetime)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            ImplementationFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        private NinjascriptServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
        {
            //Lifetime = lifetime;
            Lifetime = lifetime;
            ServiceType = serviceType;
        }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public override string ToString()
        {
            string lifetime = $"{nameof(ServiceType)}: {ServiceType} {nameof(Lifetime)}: {Lifetime} "; // string debe poder ser null

            if (ImplementationType != null)
                return lifetime + $"{nameof(ImplementationType)}: {ImplementationType}";

            if (ImplementationFactory != null)
                return lifetime + $"{nameof(ImplementationFactory)}: {ImplementationFactory.Method}";

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
                Type[] typeArguments = ImplementationFactory.GetType().GenericTypeArguments; // Type[] debe poder ser null

                Debug.Assert(typeArguments.Length == 2);

                return typeArguments[1];
            }

            Debug.Assert(false, "ImplementationType, ImplementationInstance or ImplementationFactory must be non null");
            return null;
        }

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>A new instance of <see cref="NinjascriptServiceDescriptor"/>.</returns>
        public static NinjascriptServiceDescriptor Singleton<TService,TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptServiceDescriptor"/> with the specified
        /// <paramref name="service"/> and <paramref name="implementationType"/>
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="NinjascriptServiceDescriptor"/>.</returns>
        public static NinjascriptServiceDescriptor Singleton(
            Type service,
            Type implementationType)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (implementationType == null)
                throw new ArgumentNullException(nameof(implementationType));

            return Describe(service, implementationType, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="NinjascriptServiceDescriptor"/>.</returns>
        public static NinjascriptServiceDescriptor Singleton<TService, TImplementation>(
            Func<INinjascriptServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
                throw new ArgumentNullException(nameof(implementationFactory));

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="NinjascriptServiceDescriptor"/>.</returns>
        public static NinjascriptServiceDescriptor Singleton<TService>(
            Func<INinjascriptServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (implementationFactory == null)
                throw new ArgumentNullException(nameof(implementationFactory));

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptServiceDescriptor"/> with the specified
        /// <paramref name="service"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="NinjascriptServiceDescriptor"/>.</returns>
        public static NinjascriptServiceDescriptor Singleton(
            Type service, 
            Func<INinjascriptServiceProvider, object> implementationFactory)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (implementationFactory == null)
                throw new ArgumentNullException(nameof(implementationFactory));

            return Describe(service, implementationFactory, ServiceLifetime.Singleton);
        }


        private static NinjascriptServiceDescriptor Describe<TService, TImplementation>(ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(
                typeof(TService),
                typeof(TImplementation),
                lifetime: lifetime);
        }

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationType"/>,
        /// and <paramref name="lifetime"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <param name="lifetime">The lifetime of the service.</param>
        /// <returns>A new instance of <see cref="NinjascriptServiceDescriptor"/>.</returns>
        public static NinjascriptServiceDescriptor Describe(
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime)
        {
            return new NinjascriptServiceDescriptor(serviceType, implementationType, lifetime);
        }

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationFactory"/>,
        /// and <paramref name="lifetime"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <param name="lifetime">The lifetime of the service.</param>
        /// <returns>A new instance of <see cref="NinjascriptServiceDescriptor"/>.</returns>
        public static NinjascriptServiceDescriptor Describe(
            Type serviceType, 
            Func<INinjascriptServiceProvider, object> implementationFactory, 
            ServiceLifetime lifetime)
        {
            return new NinjascriptServiceDescriptor(serviceType, implementationFactory, lifetime);
        }

        #endregion

    }
}
