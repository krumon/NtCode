using Nt.Core.DependencyInjection;
using Nt.Core.FileProviders;
using Nt.Core.Logging;
using Nt.Core.Options;
using Nt.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using IServiceProvider = Nt.Core.DependencyInjection.IServiceProvider;

namespace Nt.Core.Hosting.Internal
{
    /// <summary>
    /// Represents the ninjascript host.
    /// </summary>
    internal class Host : IHost
    {

        #region Private members

        private readonly ILogger<Host> _logger;
        private readonly IHostLifetime _hostLifetime;
        private readonly ApplicationLifetime _applicationLifetime;
        private readonly HostOptions _options;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly PhysicalFileProvider _defaultProvider;
        //private IEnumerable<IHostedService> _hostedServices;
        //private volatile bool _stopCalled;
        
        private readonly ConcurrentDictionary<Type, IEnumerable<object>> _enumerableServices = new ConcurrentDictionary<Type,IEnumerable<object>>();
        private readonly ISessionsService _sessions;

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
            IHostEnvironment hostEnvironment,
            PhysicalFileProvider defaultProvider,
            IHostApplicationLifetime applicationLifetime,
            ILogger<Host> logger,
            IHostLifetime hostLifetime,
            IOptions<HostOptions> options,
            ISessionsService sessions,
            IEnumerable<IOnBarUpdateService> onBarUpdateServices,
            IEnumerable<IOnMarketDataService> onMarketDataServices
            )
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _applicationLifetime = (applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime))) as ApplicationLifetime;
            _hostEnvironment = hostEnvironment;
            _defaultProvider = defaultProvider;

            if (_applicationLifetime is null)
                throw new ArgumentException("Replacing IHostApplicationLifetime is not supported.", nameof(applicationLifetime));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hostLifetime = hostLifetime ?? throw new ArgumentNullException(nameof(hostLifetime));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));


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
            Sessions?.OnBarUpdate();
            if (Sessions?.IsInNewSession == true)
                OnSessionUpdate();
        }
        public void OnBarUpdate(Action<object> print = null)
        {
            //ExecuteServices<IOnBarUpdateService>();
            //_sessions.OnBarUpdate();
            Sessions?.OnBarUpdate();
            if (Sessions?.IsInNewSession == true)
                OnSessionUpdate(print);
        }
        public void OnMarketData()
        { 
            //ExecuteServices<IOnMarketDataService>();
            Sessions?.OnMarketData();
            if (Sessions?.IsInNewSession == true)
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
                if (service != null)
                    action(service);
        }

        #endregion

        #region MS_Doc

        //internal sealed class Host : IHost, IAsyncDisposable
        //{
        //    private readonly ILogger<Host> _logger;
        //    private readonly IHostLifetime _hostLifetime;
        //    private readonly ApplicationLifetime _applicationLifetime;
        //    private readonly HostOptions _options;
        //    private readonly IHostEnvironment _hostEnvironment;
        //    private readonly PhysicalFileProvider _defaultProvider;
        //    private IEnumerable<IHostedService> _hostedServices;
        //    private volatile bool _stopCalled;

        //    public Host(IServiceProvider services,
        //                IHostEnvironment hostEnvironment,
        //                PhysicalFileProvider defaultProvider,
        //                IHostApplicationLifetime applicationLifetime,
        //                ILogger<Host> logger,
        //                IHostLifetime hostLifetime,
        //                IOptions<HostOptions> options)
        //    {
        //        Services = services ?? throw new ArgumentNullException(nameof(services));
        //        _applicationLifetime = (applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime))) as ApplicationLifetime;
        //        _hostEnvironment = hostEnvironment;
        //        _defaultProvider = defaultProvider;

        //        if (_applicationLifetime is null)
        //        {
        //            throw new ArgumentException("Replacing IHostApplicationLifetime is not supported.", nameof(applicationLifetime));
        //        }
        //        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //        _hostLifetime = hostLifetime ?? throw new ArgumentNullException(nameof(hostLifetime));
        //        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        //    }

        //    public IServiceProvider Services { get; }

        //    public async Task StartAsync(CancellationToken cancellationToken = default)
        //    {
        //        _logger.Starting();

        //        using var combinedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _applicationLifetime.ApplicationStopping);
        //        CancellationToken combinedCancellationToken = combinedCancellationTokenSource.Token;

        //        await _hostLifetime.WaitForStartAsync(combinedCancellationToken).ConfigureAwait(false);

        //        combinedCancellationToken.ThrowIfCancellationRequested();
        //        _hostedServices = Services.GetService<IEnumerable<IHostedService>>();

        //        foreach (IHostedService hostedService in _hostedServices)
        //        {
        //            // Fire IHostedService.Start
        //            await hostedService.StartAsync(combinedCancellationToken).ConfigureAwait(false);

        //            if (hostedService is BackgroundService backgroundService)
        //            {
        //                _ = TryExecuteBackgroundServiceAsync(backgroundService);
        //            }
        //        }

        //        // Fire IHostApplicationLifetime.Started
        //        _applicationLifetime.NotifyStarted();

        //        _logger.Started();
        //    }

        //    private async Task TryExecuteBackgroundServiceAsync(BackgroundService backgroundService)
        //    {
        //        // backgroundService.ExecuteTask may not be set (e.g. if the derived class doesn't call base.StartAsync)
        //        Task backgroundTask = backgroundService.ExecuteTask;
        //        if (backgroundTask == null)
        //        {
        //            return;
        //        }

        //        try
        //        {
        //            await backgroundTask.ConfigureAwait(false);
        //        }
        //        catch (Exception ex)
        //        {
        //            // When the host is being stopped, it cancels the background services.
        //            // This isn't an error condition, so don't log it as an error.
        //            if (_stopCalled && backgroundTask.IsCanceled && ex is OperationCanceledException)
        //            {
        //                return;
        //            }

        //            _logger.BackgroundServiceFaulted(ex);
        //            if (_options.BackgroundServiceExceptionBehavior == BackgroundServiceExceptionBehavior.StopHost)
        //            {
        //                _logger.BackgroundServiceStoppingHost(ex);
        //                _applicationLifetime.StopApplication();
        //            }
        //        }
        //    }

        //    public async Task StopAsync(CancellationToken cancellationToken = default)
        //    {
        //        _stopCalled = true;
        //        _logger.Stopping();

        //        using (var cts = new CancellationTokenSource(_options.ShutdownTimeout))
        //        using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken))
        //        {
        //            CancellationToken token = linkedCts.Token;
        //            // Trigger IHostApplicationLifetime.ApplicationStopping
        //            _applicationLifetime.StopApplication();

        //            IList<Exception> exceptions = new List<Exception>();
        //            if (_hostedServices != null) // Started?
        //            {
        //                foreach (IHostedService hostedService in _hostedServices.Reverse())
        //                {
        //                    try
        //                    {
        //                        await hostedService.StopAsync(token).ConfigureAwait(false);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        exceptions.Add(ex);
        //                    }
        //                }
        //            }

        //            // Fire IHostApplicationLifetime.Stopped
        //            _applicationLifetime.NotifyStopped();

        //            try
        //            {
        //                await _hostLifetime.StopAsync(token).ConfigureAwait(false);
        //            }
        //            catch (Exception ex)
        //            {
        //                exceptions.Add(ex);
        //            }

        //            if (exceptions.Count > 0)
        //            {
        //                var ex = new AggregateException("One or more hosted services failed to stop.", exceptions);
        //                _logger.StoppedWithException(ex);
        //                throw ex;
        //            }
        //        }

        //        _logger.Stopped();
        //    }

        //    public void Dispose() => DisposeAsync().AsTask().GetAwaiter().GetResult();

        //    public async ValueTask DisposeAsync()
        //    {
        //        // The user didn't change the ContentRootFileProvider instance, we can dispose it
        //        if (ReferenceEquals(_hostEnvironment.ContentRootFileProvider, _defaultProvider))
        //        {
        //            // Dispose the content provider
        //            await DisposeAsync(_hostEnvironment.ContentRootFileProvider).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            // In the rare case that the user replaced the ContentRootFileProvider, dispose it and the one
        //            // we originally created
        //            await DisposeAsync(_hostEnvironment.ContentRootFileProvider).ConfigureAwait(false);
        //            await DisposeAsync(_defaultProvider).ConfigureAwait(false);
        //        }

        //        // Dispose the service provider
        //        await DisposeAsync(Services).ConfigureAwait(false);

        //        static async ValueTask DisposeAsync(object o)
        //        {
        //            switch (o)
        //            {
        //                case IAsyncDisposable asyncDisposable:
        //                    await asyncDisposable.DisposeAsync().ConfigureAwait(false);
        //                    break;
        //                case IDisposable disposable:
        //                    disposable.Dispose();
        //                    break;
        //            }
        //        }
        //    }
        //}

        #endregion
    }
}
