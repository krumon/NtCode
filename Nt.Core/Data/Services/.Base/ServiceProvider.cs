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
        // Store the service descriptors.
        private ServiceDescriptor[] _descriptors;
        // Function to create the service accesor.
        private readonly Func<Type, Func<ServiceProviderEngineScope, object>> _createServiceAccessor;
        // Collection of the realized services.
        private ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object>> _realizedServices;
        // The root of the service providers engine.
        internal ServiceProviderEngineScope Root { get; }

        //internal CallSiteFactory CallSiteFactory { get; }


        //// Collection of the realized services.
        //private ConcurrentDictionary<Type, Func<Type, object>> _realizedServices;
        //// Delagate to create the service
        //private Func<Type, object> _createServiceAccessor;

        //private readonly CallSiteValidator _callSiteValidator;

        //private object _createService;

        #endregion

        #region Public properties

        #endregion

        #region Constructor

        internal ServiceProvider(ICollection<ServiceDescriptor> serviceDescriptors, ServiceProviderOptions options)
        {
            // note that Root needs to be set before calling GetEngine(), because the engine may need to access Root
            Root = new ServiceProviderEngineScope(this, isRootScope: true);
            ////_engine = GetEngine();
            // Sets the service accesor function.
            _createServiceAccessor = CreateServiceAccessor;
            // Initilize the realized services
            _realizedServices = new ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object>>();
            // Initialize the call site factory
            //CallSiteFactory = new CallSiteFactory(serviceDescriptors);
            //// The list of built in services that aren't part of the list of service descriptors
            //// keep this in sync with CallSiteFactory.IsService
            //CallSiteFactory.Add(typeof(INinjascriptServiceProvider), new ServiceProviderCallSite());
            //CallSiteFactory.Add(typeof(IServiceScopeFactory), new ConstantCallSite(typeof(IServiceScopeFactory), Root));
            //CallSiteFactory.Add(typeof(IServiceProviderIsService), new ConstantCallSite(typeof(IServiceProviderIsService), CallSiteFactory));

            // Default is false
            if (options.ValidateScopes)
            {
                //_callSiteValidator = new CallSiteValidator();
            }

            // Default is false
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

            //DependencyInjectionEventSource.Log.ServiceProviderBuilt(this);
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

        #endregion

        #region Private methods

        internal object GetService(Type serviceType, ServiceProviderEngineScope serviceProviderEngineScope) 
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ServiceProvider));

            Func<ServiceProviderEngineScope, object> realizedService = _realizedServices.GetOrAdd(serviceType, _createServiceAccessor);
            OnResolve(serviceType);
            var result = realizedService.Invoke(serviceProviderEngineScope);
            Debug.Assert(result is null); 
            return result;
        }

        private void ValidateService(ServiceDescriptor descriptor)
        {
            Type serviceType = descriptor.ServiceType;
            
            if (serviceType.IsGenericType && !serviceType.IsConstructedGenericType)
            {
                return;
            }

            try
            {
                if (serviceType.IsGenericTypeDefinition)
                {
                    Type implementationType = descriptor.ImplementationType;

                    if (implementationType == null || !implementationType.IsGenericTypeDefinition)
                    {
                        throw new ArgumentException("OpenGenericServiceRequiresOpenGenericImplementation");
                    }

                    if (implementationType.IsAbstract || implementationType.IsInterface)
                    {
                        throw new ArgumentException("TypeCannotBeActivated");
                    }

                    Type[] serviceTypeGenericArguments = serviceType.GetGenericArguments();
                    Type[] implementationTypeGenericArguments = implementationType.GetGenericArguments();
                    if (serviceTypeGenericArguments.Length != implementationTypeGenericArguments.Length)
                    {
                        throw new ArgumentException("ArityOfOpenGenericServiceNotEqualArityOfOpenGenericImplementation");
                    }

                }
                else if (descriptor.ImplementationInstance == null)
                {
                    Debug.Assert(descriptor.ImplementationType != null);
                    Type implementationType = descriptor.ImplementationType;

                    if (implementationType.IsGenericTypeDefinition ||
                        implementationType.IsAbstract ||
                        implementationType.IsInterface)
                    {
                        throw new ArgumentException("TypeCannotBeActivated");
                    }
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error while validating the service descriptor '{descriptor}': {e.Message}", e);
            }
        }

        private Func<ServiceProviderEngineScope,object> CreateServiceAccessor(Type serviceType)
        {


            //ServiceCallSite callSite = CallSiteFactory.GetCallSite(serviceType, new CallSiteChain());
            //if (callSite != null)
            //{
            //    //DependencyInjectionEventSource.Log.CallSiteBuilt(this, serviceType, callSite);
            //    OnCreate(callSite);

            //    // Optimize singleton case
            //    if (callSite.Cache.Location == CallSiteResultCacheLocation.Root)
            //    {
            //        // TODO: To test.
            //        object value = null; // CallSiteRuntimeResolver.Instance.Resolve(callSite, Root);
            //        return scope => value;
            //    }

            //    return _engine.RealizeService(callSite);
            //}

            return null; // _ => null;
        }

        private void DisposeCore()
        {
            _disposed = true;
        }

        private void OnCreate()
        {

        }

        private void OnResolve(Type serviceType)
        {

        }

        public object GetService(object key)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
