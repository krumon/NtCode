using Nt.Core.DependencyInjection;
using Nt.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using IServiceProvider = Nt.Core.DependencyInjection.IServiceProvider;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Represents the ninjascript host.
    /// </summary>
    public class Host : IHost
    {

        #region Private members

        //private readonly ILogger<Host> _logger;
        //private readonly IHostLifetime _hostLifetime;
        //private readonly NinjascriptLifetime _ninjascriptLifetime;
        //private readonly INinjascriptHostEnvironment _hostEnvironment;
        //private readonly PhysicalFileProvider _defaultProvider;
        //private IEnumerable<IHostedService> _hostedServices;
        //private volatile bool _stopCalled;
        private readonly ISessionsService _sessions;
        private readonly ConcurrentDictionary<Type, IEnumerable<object>> _enumerableServices = new ConcurrentDictionary<Type,IEnumerable<object>>();
        private readonly HostOptions _options;

        #endregion

        #region Public properties

        public IServiceProvider Services { get; private set; }
        public ISessionsService Sessions { get { return _sessions; } }
        //public bool? IsInNewSession => _sessions?.Iterator?.IsSessionUpdated;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="Host"/> default instance.
        /// </summary>
        /// <param name="services">The host service provider.</param>
        /// <param name="options">The host options.</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal Host(
            IServiceProvider services,
            HostOptions options,
            ISessionsService sessions,
            IEnumerable<IOnBarUpdateService> onBarUpdateServices,
            IEnumerable<IOnMarketDataService> onMarketDataServices
            )
        {
            // _logger.Starting();

            Services = services ?? throw new ArgumentNullException(nameof(services));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _sessions = services.GetService<ISessionsService>();

            //_sessionsService?.Iterator?.SessionChanged += OnSessionUpdate;
            _enumerableServices.TryAdd(typeof(IOnBarUpdateService), new List<IOnBarUpdateService> { _sessions });
            _enumerableServices.TryAdd(typeof(IOnMarketDataService), new List<IOnMarketDataService> { _sessions });
            _enumerableServices.TryAdd(typeof(IOnSessionUpdateService), new List<IOnSessionUpdateService> { _sessions });

            //if (onBarUpdateServices != null)
            //    _enumerableServices.TryAdd(typeof(IOnBarUpdateService), onBarUpdateServices);
            //if (onMarketDataServices != null)
            //    _enumerableServices.TryAdd(typeof(IOnMarketDataService), onMarketDataServices);

            // Fire IHostApplicationLifetime.Started
            //_lifetime.NotifyStarted();

        }

        #endregion

        #region Public methods

        public void Configure(params object[] ninjascriptObjects)
        {
            //_logger.Configuring();

            IList<IConfigureService> configureServices = (IList<IConfigureService>)Services.GetServices<IConfigureService>();
                        
            if (configureServices != null && configureServices.Count > 0)
            {
                foreach (IConfigureService configureService in configureServices)
                {
                    // Fire IHostedService.Start
                    configureService.Configure(ninjascriptObjects);
                }
            }

            //_logger.Configured();

        }

        public void DataLoaded(params object[] ninjascriptObjects)
        {
            //_logger.ConfiguringWhenDataLoaded();

            IList<IDataLoadedService> dataLoadedServices = (IList<IDataLoadedService>)Services.GetServices<IDataLoadedService>();

            if (dataLoadedServices != null && dataLoadedServices.Count > 0)
            {
                foreach (IDataLoadedService dataLoadedService in dataLoadedServices)
                {
                    // Fire IHostedService.Start
                    dataLoadedService.DataLoaded(ninjascriptObjects);
                }
            }

            //_logger.ConfiguredWhenDataLoaded();
        }

        public void OnBarUpdate()
        {
            //ExecuteServices<IOnBarUpdateService>();
            //_sessions.OnBarUpdate();
            Sessions.OnBarUpdate();
            if (Sessions.IsInNewSession == true)
                OnSessionUpdate();
        }
        public void OnBarUpdate(Action<object> print = null)
        {
            //ExecuteServices<IOnBarUpdateService>();
            //_sessions.OnBarUpdate();
            Sessions.OnBarUpdate();
            if (Sessions.IsInNewSession == true)
                OnSessionUpdate(print);
        }
        public void OnMarketData()
        { 
            //ExecuteServices<IOnMarketDataService>();
            Sessions.OnMarketData();
            if (Sessions.IsInNewSession == true)
                OnSessionUpdate();
        }
        public void OnSessionUpdate(Action<object> print = null) 
        {
            ExecuteServices<IOnSessionUpdateService>(s => s.OnSessionUpdate());
            //var onSessionUpdateServices = Services.GetServices<IOnSessionUpdateService>();
            //if (onSessionUpdateServices != null)
            //    foreach (var service in onSessionUpdateServices)
            //        service.OnSessionUpdate();

            print?.Invoke(_sessions.Iterator.ToString());
        }

        public void Dispose()
        {
            //_stopCalled = true;
            //_logger.Disposing();

            //    // Trigger IHostApplicationLifetime.ApplicationStopping
            //    _applicationLifetime.StopApplication();

            IList<IHostedService> _hostedServices = (IList<IHostedService>)Services.GetServices<IHostedService>();
            IList<Exception> exceptions = new List<Exception>();
            if (_hostedServices != null) // Started?
            {
                foreach (IHostedService hostedService in _hostedServices)
                {
                    try
                    {
                        hostedService.Dispose();
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }
            }

            //// Fire IHostApplicationLifetime.Stopped
            //_applicationLifetime.NotifyStopped();

            //try
            //{
            //    await _hostLifetime.StopAsync(token).ConfigureAwait(false);
            //}
            //catch (Exception ex)
            //{
            //    exceptions.Add(ex);
            //}

            if (exceptions.Count > 0)
            {
                var ex = new AggregateException("One or more hosted services failed to stop.", exceptions);
                //_logger.StoppedWithException(ex);
                throw ex;
            }

            //// The user didn't change the ContentRootFileProvider instance, we can dispose it
            //if (ReferenceEquals(_hostEnvironment.ContentRootFileProvider, _defaultProvider))
            //{
            //    // Dispose the content provider
            //    await DisposeAsync(_hostEnvironment.ContentRootFileProvider).ConfigureAwait(false);
            //}
            //else
            //{
            //    // In the rare case that the user replaced the ContentRootFileProvider, dispose it and the one
            //    // we originally created
            //    await DisposeAsync(_hostEnvironment.ContentRootFileProvider).ConfigureAwait(false);
            //    await DisposeAsync(_defaultProvider).ConfigureAwait(false);
            //}

            // Dispose the service provider
            //DisposeAsync(Services).ConfigureAwait(false);

            //async ValueTask DisposeAsync(object o)
            //{
            //    switch (o)
            //    {
            //        case IAsyncDisposable asyncDisposable:
            //            await asyncDisposable.DisposeAsync().ConfigureAwait(false);
            //            break;
            //        case IDisposable disposable:
            //            disposable.Dispose();
            //            break;
            //    }
            //}

            //_logger.Disposed();

        }

        #endregion

        #region Private methods

        private void ExecuteServices<T>(Action<T> action)
        {
            if (!_enumerableServices.TryGetValue(typeof(T), out IEnumerable<object> services))
                return;
            if (services is IEnumerable<T> s)
                ForEach(s, action);

            //switch (services)
            //{
            //    case IEnumerable<IOnBarUpdateService> onBarUpdateServices:
            //        ForEach(onBarUpdateServices, (onBarUpdateService) => onBarUpdateService.OnBarUpdate());
            //        break;

            //    case IEnumerable<IOnMarketDataService> onMarketDataServices:
            //        ForEach(onMarketDataServices, (onMarketDataService) => onMarketDataService.OnMarketData());
            //        break;

            //}
        }

        private void ForEach<T>(IEnumerable<T> services, Action<T> action)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            foreach (T service in services)
                action(service);
        }

        #endregion


    }
}
