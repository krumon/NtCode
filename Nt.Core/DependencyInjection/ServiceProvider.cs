using Nt.Core.DependencyInjection.Internal;
using Nt.Core.Hosting;
using Nt.Core.Services;
using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nt.Core.DependencyInjection
{
    /// <summary>
    /// Default <see cref="IServiceProvider"/>.
    /// </summary>
    public sealed class ServiceProvider : IServiceProvider, IDisposable, IAsyncDisposable
    {

        #region Private members

        // Check the service provider is disposed.
        private bool _disposed;
        // Collection of the realized services.
        private ConcurrentDictionary<Type, object> _realizedServices;
        // The factory of the calls.
        internal CallSiteFactory CallSiteFactory { get; }
        // Delagate to create the service
        private Func<Type,object> _createService;
        // Delagate to create the enumerable service
        private Func<Type,object> _createEnumerableService;

        #endregion

        #region Constructor

        internal ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors) : this(serviceDescriptors, ServiceProviderOptions.Default)
        {
        }

        internal ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors, ServiceProviderOptions options)
        {
            // Initilize the realized services.
            _realizedServices = new ConcurrentDictionary<Type, object>();
            // Initialize the call site factory.
            CallSiteFactory = new CallSiteFactory(serviceDescriptors);
            // Sets the delegate method.
            _createService = CreateService;
            // Sets the delegate method.
            _createEnumerableService = CreateEnumerableService;

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
            object result = _realizedServices.GetOrAdd(serviceType, _createService);
            Debug.Assert(result != null);

            return result;
        }

        /// <summary>
        /// Gets all service objects of the specified type.
        /// </summary>
        /// <returns>The service collection that was produced.</returns>
        public object GetServices(Type serviceType)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ServiceProvider));

            object result;
            //if (
            //    serviceType.IsAssignableFrom(typeof(IConfigureService)) 
            //    || serviceType.IsAssignableFrom(typeof(IDataLoadedService)))

            //    result = CreateEnumerableService(serviceType);
            //else
            result = _realizedServices.GetOrAdd(serviceType, _createEnumerableService);

            Debug.Assert(result != null);

            return result;
        }

        #endregion

        #region Private methods

        private void ValidateService(ServiceDescriptor descriptor)
        {
            Type serviceType = descriptor.ServiceType;

            if (serviceType == typeof(IHost))
                return;

            if (serviceType.IsGenericType && !serviceType.IsConstructedGenericType)
                throw new Exception();

            try
            {
                object result = _realizedServices.GetOrAdd(serviceType, _createService);
                Debug.Assert(result != null);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error while validating the service descriptor '{descriptor}': {e.Message}", e);
            }
        }

        private object CreateService(Type serviceType)
        {
            ServiceCallSite callSite = CallSiteFactory.GetCallSite(serviceType, new CallSiteChain());
            if (callSite != null)
            {
                object value = CallSiteRuntimeResolver.Instance.Resolve(callSite, this);
                return value;
            }

            return null;
        }

        private object CreateEnumerableService(Type serviceType)
        {
            IList<object> list = new List<object>();
            foreach (KeyValuePair<Type,object> service in _realizedServices)
                //if (service.Value.GetType().IsAssignableFrom(serviceType))
                if (serviceType.IsAssignableFrom(service.Value.GetType()))
                    list.Add(service.Value);
            return list.Count > 0 ? list : null;
        }

        #endregion

        #region Dispose methods

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

        private void DisposeCore()
        {
            _disposed = true;
        }

        #endregion

    }
}
