using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
        // Collection of the realized services.
        private ConcurrentDictionary<Type, Func<ServiceProvider, object>> _realizedServices;
        // The factory of the calls.
        internal CallSiteFactory CallSiteFactory { get; }
        // Delagate to create the service
        private Func<Type,Func<ServiceProvider,object>> _createServiceAccessor;

        #endregion

        #region Constructor

        internal ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors, ServiceProviderOptions options)
        {
            // Initilize the realized services.
            _realizedServices = new ConcurrentDictionary<Type, Func<ServiceProvider, object>>();
            // Initialize the call site factory.
            CallSiteFactory = new CallSiteFactory(serviceDescriptors);
            // Sets the delegate method.
            _createServiceAccessor = CreateServiceAccessor;

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
            Func<ServiceProvider, object> realizedService = _realizedServices.GetOrAdd(serviceType, _createServiceAccessor);
            var result = realizedService.Invoke(this);
            Debug.Assert(result is null);
            return realizedService;
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
                ServiceCallSite callSite = CallSiteFactory.GetCallSite(serviceType, new CallSiteChain());
                if (callSite != null)
                    ValidateCallSite(callSite);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error while validating the service descriptor '{descriptor}': {e.Message}", e);
            }
        }

        private Func<ServiceProvider,object> CreateServiceAccessor(Type serviceType)
        {
            ServiceCallSite callSite = CallSiteFactory.GetCallSite(serviceType, new CallSiteChain());
            if (callSite != null)
            {
                object value = CallSiteRuntimeResolver.Instance.Resolve(callSite, this);
                return sp => value;
            }

            return null;
        }

        private void ValidateCallSite(ServiceCallSite callSite)
        {
            throw new NotImplementedException();
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
