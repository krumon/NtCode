﻿using System;

namespace Nt.Core.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddServiceType(
            this IServiceCollection services,
            Type serviceType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return services.AddImplementationType(serviceType, serviceType);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddImplementationType(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Add(services, serviceType, implementationType);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddImplementationFactory(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Add(services, serviceType, implementationFactory);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with an
        /// instance specified in <paramref name="implementationInstance"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddImplementationInstance(
            this IServiceCollection services,
            Type serviceType,
            object implementationInstance)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            var serviceDescriptor = new ServiceDescriptor(serviceType, implementationInstance);
            services.Add(serviceDescriptor);
            return services;
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection Add<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddImplementationType(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="INinjascriptServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection Add<TService>(this IServiceCollection services)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddServiceType(typeof(TService));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection Add<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return services.AddImplementationFactory(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection Add<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return services.AddImplementationFactory(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService" /> with an
        /// instance specified in <paramref name="implementationInstance"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection Add<TService>(
            this IServiceCollection services,
            TService implementationInstance)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return services.AddImplementationInstance(typeof(TService), implementationInstance);
        }

        /// <summary>
        /// Creates a <see cref="ServiceProvider"/> containing services from the provided <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> containing service descriptors.</param>
        /// <returns>The <see cref="ServiceProvider"/>.</returns>
        public static ServiceProvider BuildServiceProvider(this IServiceCollection services)
        {
            return BuildServiceProvider(services, ServiceProviderOptions.Default);
        }

        /// <summary>
        /// Creates a <see cref="ServiceProvider"/> containing services from the provided <see cref="IServiceCollection"/>
        /// optionally enabling scope validation.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> containing service descriptors.</param>
        /// <param name="options">
        /// Configures various service provider behaviors.
        /// </param>
        /// <returns>The <see cref="ServiceProvider"/>.</returns>
        public static ServiceProvider BuildServiceProvider(this IServiceCollection services, ServiceProviderOptions options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return new ServiceProvider(services, options);
        }

        private static IServiceCollection Add(
            IServiceCollection collection,
            Type serviceType,
            Type implementationType)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType);
            collection.Add(descriptor);
            return collection;
        }

        private static IServiceCollection Add(
            IServiceCollection collection,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationFactory);
            collection.Add(descriptor);
            return collection;
        }

    }
}
