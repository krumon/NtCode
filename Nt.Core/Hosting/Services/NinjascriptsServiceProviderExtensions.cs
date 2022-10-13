﻿using System;
using System.Collections.Generic;

namespace Nt.Core.Hosting.Services
{
    /// <summary>
    /// Extension methods for getting services from an <see cref="IServiceProvider" />.
    /// </summary>
    public static class NinjascriptsServiceProviderExtensions
    {

        /// <summary>
        /// Get service of type <typeparamref name="T"/> from the <see cref="INinjascriptsServiceProvider"/>.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <param name="provider">The <see cref="INinjascriptsServiceProvider"/> to retrieve the service object from.</param>
        /// <returns>A service object of type <typeparamref name="T"/> or null if there is no such service.</returns>
        public static T GetService<T>(this INinjascriptsServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return (T)provider.GetService(typeof(T));
        }

        /// <summary>
        /// Get service of type <paramref name="serviceType"/> from the <see cref="INinjascriptsServiceProvider"/>.
        /// </summary>
        /// <param name="provider">The <see cref="INinjascriptsServiceProvider"/> to retrieve the service object from.</param>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>A service object of type <paramref name="serviceType"/>.</returns>
        /// <exception cref="System.InvalidOperationException">There is no service of type <paramref name="serviceType"/>.</exception>
        public static object GetRequiredService(this INinjascriptsServiceProvider provider, Type serviceType)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (provider is IHostRequiredService requiredServiceSupportingProvider)
            {
                return requiredServiceSupportingProvider.GetRequiredService(serviceType);
            }

            object service = provider.GetService(serviceType);
            if (service == null)
            {
                throw new InvalidOperationException("The required service cannot be null.");
            }

            return service;
        }

        /// <summary>
        /// Get service of type <typeparamref name="T"/> from the <see cref="INinjascriptsServiceProvider"/>.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <param name="provider">The <see cref="INinjascriptsServiceProvider"/> to retrieve the service object from.</param>
        /// <returns>A service object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="System.InvalidOperationException">There is no service of type <typeparamref name="T"/>.</exception>
        public static T GetRequiredService<T>(this INinjascriptsServiceProvider provider) // where T : notnull
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return (T)provider.GetRequiredService(typeof(T));
        }

        /// <summary>
        /// Get an enumeration of services of type <typeparamref name="T"/> from the <see cref="INinjascriptsServiceProvider"/>.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <param name="provider">The <see cref="INinjascriptsServiceProvider"/> to retrieve the services from.</param>
        /// <returns>An enumeration of services of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> GetServices<T>(this INinjascriptsServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return provider.GetRequiredService<IEnumerable<T>>();
        }

        /// <summary>
        /// Get an enumeration of services of type <paramref name="serviceType"/> from the <see cref="INinjascriptsServiceProvider"/>.
        /// </summary>
        /// <param name="provider">The <see cref="INinjascriptsServiceProvider"/> to retrieve the services from.</param>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>An enumeration of services of type <paramref name="serviceType"/>.</returns>
        public static IEnumerable<object> GetServices(this INinjascriptsServiceProvider provider, Type serviceType)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            Type genericEnumerable = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>)provider.GetRequiredService(genericEnumerable);
        }

        ///// <summary>
        ///// Creates a new <see cref="IServiceScope"/> that can be used to resolve scoped services.
        ///// </summary>
        ///// <param name="provider">The <see cref="IServiceProvider"/> to create the scope from.</param>
        ///// <returns>A <see cref="IServiceScope"/> that can be used to resolve scoped services.</returns>
        //public static IServiceScope CreateScope(this IServiceProvider provider)
        //{
        //    return provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        //}

        ///// <summary>
        ///// Creates a new <see cref="AsyncServiceScope"/> that can be used to resolve scoped services.
        ///// </summary>
        ///// <param name="provider">The <see cref="IServiceProvider"/> to create the scope from.</param>
        ///// <returns>An <see cref="AsyncServiceScope"/> that can be used to resolve scoped services.</returns>
        //public static AsyncServiceScope CreateAsyncScope(this IServiceProvider provider)
        //{
        //    return new AsyncServiceScope(provider.CreateScope());
        //}

        ///// <summary>
        ///// Creates a new <see cref="AsyncServiceScope"/> that can be used to resolve scoped services.
        ///// </summary>
        ///// <param name="serviceScopeFactory">The <see cref="IServiceScopeFactory"/> to create the scope from.</param>
        ///// <returns>An <see cref="AsyncServiceScope"/> that can be used to resolve scoped services.</returns>
        //public static AsyncServiceScope CreateAsyncScope(this IServiceScopeFactory serviceScopeFactory)
        //{
        //    return new AsyncServiceScope(serviceScopeFactory.CreateScope());
        //}
    }
}
