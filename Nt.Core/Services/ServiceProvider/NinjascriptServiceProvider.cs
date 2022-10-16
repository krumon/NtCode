using Nt.Core.Exceptions;
using Nt.Core.Services.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nt.Core.Services
{
    /// <summary>
    /// Default <see cref="INinjascriptServiceProvider"/>.
    /// </summary>
    public sealed class NinjascriptServiceProvider : INinjascriptServiceProvider, IDisposable, IAsyncDisposable
    {

        #region Private members

        private readonly CallSiteValidator _callSiteValidator;
        private readonly Func<Type, Func<ServiceProviderEngineScope, object>> _createServiceAccessor;
        //internal ServiceProviderEngine _engine;
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

        #region Constructor

        internal NinjascriptServiceProvider(ICollection<NinjascriptServiceDescriptor> serviceDescriptors) //, ServiceProviderOptions options)
        {
            //// note that Root needs to be set before calling GetEngine(), because the engine may need to access Root
            //Root = new ServiceProviderEngineScope(this, isRootScope: true);
            ////_engine = GetEngine();
            //_createServiceAccessor = CreateServiceAccessor;
            //_realizedServices = new ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object>>();

            //CallSiteFactory = new CallSiteFactory(serviceDescriptors);
            //// The list of built in services that aren't part of the list of service descriptors
            //// keep this in sync with CallSiteFactory.IsService
            //CallSiteFactory.Add(typeof(INinjascriptServiceProvider), new ServiceProviderCallSite());
            //CallSiteFactory.Add(typeof(IServiceScopeFactory), new ConstantCallSite(typeof(IServiceScopeFactory), Root));
            //CallSiteFactory.Add(typeof(IServiceProviderIsService), new ConstantCallSite(typeof(IServiceProviderIsService), CallSiteFactory));

            //// Replace the options in the constructor
            //string options = "ValidateOnBuild";

            //if (options == "ValidateScopes")
            //{
            //    _callSiteValidator = new CallSiteValidator();
            //}

            //if (options == "ValidateOnBuild")
            //{
            //    List<Exception> exceptions = null;
            //    foreach (NinjascriptServiceDescriptor serviceDescriptor in serviceDescriptors)
            //    {
            //        try
            //        {
            //            ValidateService(serviceDescriptor);
            //        }
            //        catch (Exception e)
            //        {
            //            exceptions = exceptions ?? new List<Exception>();
            //            exceptions.Add(e);
            //        }
            //    }

            //    if (exceptions != null)
            //    {
            //        throw new AggregateException("Some services are not able to be constructed", exceptions.ToArray());
            //    }
            //}

            ////DependencyInjectionEventSource.Log.ServiceProviderBuilt(this);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service to get.</param>
        /// <returns>The service that was produced.</returns>
        public object GetService(Type serviceType) => GetService(serviceType, Root);

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

        //public abstract object CreateService(Type serviceType);
        //public abstract bool ValidateService(Type serviceType);

        #endregion

        #region Private methods

        internal object GetService(Type serviceType, ServiceProviderEngineScope serviceProviderEngineScope)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(NinjascriptServiceProvider));

            //if (ValidateService(serviceType))
            //{
            //    Func<Type, object> realizedService = _realizedServicesTest.GetOrAdd(serviceType.GetType(), CreateService);
            //    var result = realizedService.Invoke(serviceType);
            //    return result;
            //}

            return default;

            //Func<ServiceProviderEngineScope, object> realizedService = _realizedServices.GetOrAdd(serviceType, _createServiceAccessor);
            //OnResolve(serviceType, serviceProviderEngineScope);
            //DependencyInjectionEventSource.Log.ServiceResolved(this, serviceType);
            //var result = realizedService.Invoke(serviceProviderEngineScope);
            //System.Diagnostics.Debug.Assert(result is null || CallSiteFactory.IsService(serviceType));
            //return result;
        }

        private void ValidateService(NinjascriptServiceDescriptor descriptor)
        {
            if (descriptor.ServiceType.IsGenericType && !descriptor.ServiceType.IsConstructedGenericType)
            {
                return;
            }

            try
            {
                //ServiceCallSite callSite = CallSiteFactory.GetCallSite(descriptor, new CallSiteChain());
                //if (callSite != null)
                //{
                //    OnCreate(callSite);
                //}
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error while validating the service descriptor '{descriptor}': {e.Message}", e);
            }
        }

        //private Func<ServiceProviderEngineScope, object> CreateServiceAccessor(Type serviceType)
        //{
        //    ServiceCallSite callSite = CallSiteFactory.GetCallSite(serviceType, new CallSiteChain());
        //    if (callSite != null)
        //    {
        //        //DependencyInjectionEventSource.Log.CallSiteBuilt(this, serviceType, callSite);
        //        OnCreate(callSite);

        //        // Optimize singleton case
        //        if (callSite.Cache.Location == CallSiteResultCacheLocation.Root)
        //        {
        //            object value = CallSiteRuntimeResolver.Instance.Resolve(callSite, Root);
        //            return scope => value;
        //        }

        //        return _engine.RealizeService(callSite);
        //    }

        //    return _ => null;
        //}

        //internal void ReplaceServiceAccessor(ServiceCallSite callSite, Func<ServiceProviderEngineScope, object> accessor)
        //{
        //    _realizedServices[callSite.ServiceType] = accessor;
        //}

        internal IServiceScope CreateScope()
        {
            if (_disposed)
            {
                ThrowHelper.ThrowObjectDisposedException();
            }

            return new ServiceProviderEngineScope(this, isRootScope: false);
        }

//        private ServiceProviderEngine GetEngine()
//        {
//            ServiceProviderEngine engine;

//#if NETFRAMEWORK || NETSTANDARD2_0
//            engine = new DynamicServiceProviderEngine(this);
//#else
//            if (RuntimeFeature.IsDynamicCodeCompiled)
//            {
//                engine = new DynamicServiceProviderEngine(this);
//            }
//            else
//            {
//                // Don't try to compile Expressions/IL if they are going to get interpreted
//                engine = RuntimeServiceProviderEngine.Instance;
//            }
//#endif
//            return engine;
//        }

        private void DisposeCore()
        {
            _disposed = true;
        }

        //private void OnCreate(ServiceCallSite callSite)
        //{
        //    _callSiteValidator?.ValidateCallSite(callSite);
        //}

        //private void OnResolve(Type serviceType, IServiceScope scope)
        //{
        //    _callSiteValidator?.ValidateResolution(serviceType, scope, Root);
        //}


        // Esto es mío
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
