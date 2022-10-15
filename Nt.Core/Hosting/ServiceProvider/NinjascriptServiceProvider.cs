using Nt.Core.Hosting.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Default <see cref="INinjascriptServiceProvider"/>.
    /// </summary>
    public abstract class NinjascriptServiceProvider : INinjascriptServiceProvider, IDisposable, IAsyncDisposable
    {

        #region Private members

        private readonly CallSiteValidator _callSiteValidator;
        private readonly Func<Type, Func<ServiceProviderEngineScope, object>> _createServiceAccessor;
        internal ServiceProviderEngine _engine;
        private bool _disposed;
        //private ConcurrentDictionary<Type, Func<Type,object>> _realizedServices;
        private ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object>> _realizedServices;
        //For texts
        private ConcurrentDictionary<Type, Func<Type, object>> _realizedServicesTest;
        internal CallSiteFactory CallSiteFactory { get; }
        internal ServiceProviderEngineScope Root { get; }
        internal static bool VerifyOpenGenericServiceTrimmability { get; } =
            AppContext.TryGetSwitch("Microsoft.Extensions.DependencyInjection.VerifyOpenGenericServiceTrimmability", out bool verifyOpenGenerics) ? verifyOpenGenerics : false;
        private object _createService;
        NinjascriptServiceDescriptor[] _descriptors;

        #endregion

        #region Implementation

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service to get.</param>
        /// <returns>The service that was produced.</returns>
        public object GetService(Type serviceType)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(NinjascriptServiceProvider));

            if (ValidateService(serviceType))
            {
                Func<Type,object> realizedService = _realizedServicesTest.GetOrAdd(serviceType.GetType(), CreateService);
                var result = realizedService.Invoke(serviceType);
                return result;
            }

            return default;
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

        public abstract object CreateService(Type serviceType);
        public abstract bool ValidateService(Type serviceType);

        #endregion

        #region Constructors

        //internal ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors, ServiceProviderOptions options)
        //{
        //    // note that Root needs to be set before calling GetEngine(), because the engine may need to access Root
        //    Root = new ServiceProviderEngineScope(this, isRootScope: true);
        //    _engine = GetEngine();
        //    _createServiceAccessor = CreateServiceAccessor;
        //    _realizedServices = new ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object>>();

        //    CallSiteFactory = new CallSiteFactory(serviceDescriptors);
        //    // The list of built in services that aren't part of the list of service descriptors
        //    // keep this in sync with CallSiteFactory.IsService
        //    CallSiteFactory.Add(typeof(IServiceProvider), new ServiceProviderCallSite());
        //    CallSiteFactory.Add(typeof(IServiceScopeFactory), new ConstantCallSite(typeof(IServiceScopeFactory), Root));
        //    CallSiteFactory.Add(typeof(IServiceProviderIsService), new ConstantCallSite(typeof(IServiceProviderIsService), CallSiteFactory));

        //    if (options.ValidateScopes)
        //    {
        //        _callSiteValidator = new CallSiteValidator();
        //    }

        //    if (options.ValidateOnBuild)
        //    {
        //        List<Exception> exceptions = null;
        //        foreach (ServiceDescriptor serviceDescriptor in serviceDescriptors)
        //        {
        //            try
        //            {
        //                ValidateService(serviceDescriptor);
        //            }
        //            catch (Exception e)
        //            {
        //                exceptions = exceptions ?? new List<Exception>();
        //                exceptions.Add(e);
        //            }
        //        }

        //        if (exceptions != null)
        //        {
        //            throw new AggregateException("Some services are not able to be constructed", exceptions.ToArray());
        //        }
        //    }

        //    DependencyInjectionEventSource.Log.ServiceProviderBuilt(this);
        //}
        public NinjascriptServiceProvider(ICollection<NinjascriptServiceDescriptor> descriptors)
        {
            _realizedServicesTest = new ConcurrentDictionary<Type, Func<Type,object>>();
            ValidateDescriptors(descriptors);
        }

        #endregion

        #region Private methods

        private void DisposeCore()
        {
            _disposed = true;
        }

        private NinjascriptServiceDescriptor[] ValidateDescriptors(ICollection<NinjascriptServiceDescriptor> descriptors)
        {
            _descriptors = new NinjascriptServiceDescriptor[descriptors.Count];
            descriptors.CopyTo(_descriptors, 0);
            return _descriptors;
        }

        private object CreateServiceByDefault(Type serviceType)
        {
            try
            {
                if (!ValidateServiceByDefault())
                    return default;

                return _createService = Activator.CreateInstance(serviceType);
            }
            catch
            {
                Debugger.Break();
                return default;
            }
        }

        private bool ValidateServiceByDefault() => true;

        #endregion

    }
}
