using Nt.Core.DependencyInjection.Internal;
using Nt.Core.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//using System.Diagnostics;

namespace Nt.Core.DependencyInjection
{
    /// <summary>
    /// Default <see cref="IServiceProvider"/>.
    /// </summary>
    public sealed class ServiceProvider : IServiceProvider, IDisposable
    {

        #region Members

        // Check the service provider is disposed.
        private bool _disposed;
        // Collection of the realized services.
        private readonly ConcurrentDictionary<Type, object> _realizedServices;
        // The factory of the calls.
        internal CallSiteFactory CallSiteFactory { get; }
        // Delagate to create the service
        private Func<Type,object> _createService;

        internal static bool VerifyOpenGenericServiceTrimmability { get; } =
            AppContext.TryGetSwitch("Nt.Core.DependencyInjection.VerifyOpenGenericServiceTrimmability", out bool verifyOpenGenerics) ? verifyOpenGenerics : false;

        #endregion

        #region Constructor

        public ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors) : this(serviceDescriptors, ServiceProviderOptions.Default)
        {
        }

        public ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors, ServiceProviderOptions options)
        {
            // Initilize the realized services.
            _realizedServices = new ConcurrentDictionary<Type, object>();
            // Initialize the call site factory.
            CallSiteFactory = new CallSiteFactory(serviceDescriptors);
            // Sets the delegate method.
            _createService = CreateService;

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
            //Debug.Assert(result != null);

            return result;
        }

        /// <summary>
        /// Gets all service objects of the specified type.
        /// </summary>
        /// <returns>The service collection that was produced.</returns>
        public IEnumerable<T> GetServices<T>()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ServiceProvider));

            IList<T> result = (IList<T>)CreateServices<T>(typeof(T));

            return result;
        }

        #endregion

        #region Private methods

        private void ValidateService(ServiceDescriptor descriptor)
        {
            Type serviceType = descriptor.ServiceType;

            if (descriptor.ServiceType.IsGenericType && !descriptor.ServiceType.IsConstructedGenericType)
                return;

            try
            {
                ServiceCallSite callSite = CallSiteFactory.GetCallSite(descriptor, new CallSiteChain());
                if (callSite != null)
                {
                    OnCreate(callSite);
                }
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

        private IEnumerable<T> CreateServices<T>(Type serviceType)
        {
            if (!(typeof(T) == serviceType))
                throw new InvalidOperationException();

            Type[] serviceTypeGenericArguments = serviceType.GetGenericArguments();

            IList<T> list = new List<T>();
            foreach (KeyValuePair<Type,object> service in _realizedServices)
                if (serviceType.IsAssignableFrom(service.Value?.GetType()))
                    list.Add((T)service.Value);
            return list.Count > 0 ? list : null;
        }

        private void OnCreate(ServiceCallSite callSite)
        {
        }

        #endregion

        #region Dispose methods

        public void Dispose()
        {
            DisposeCore();
        }

        private void DisposeCore()
        {
            _disposed = true;
        }

        #endregion

    }
}
