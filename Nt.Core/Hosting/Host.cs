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
        private ConcurrentDictionary<Type, IEnumerable<object>> _enumerableServices = new ConcurrentDictionary<Type,IEnumerable<object>>();
        private HostOptions _options;

        #endregion

        #region Public properties

        /// <inheritdoc/>
        public IServiceProvider Services { get; private set; }

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
            IEnumerable<IOnBarUpdateService> onBarUpdateServices,
            IEnumerable<IOnMarketDataService> onMarketDataServices
            )
        {
            // _logger.Starting();

            Services = services ?? throw new ArgumentNullException(nameof(services));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            if (onBarUpdateServices != null)
                _enumerableServices.TryAdd(typeof(IOnBarUpdateService), onBarUpdateServices);
            if (onMarketDataServices != null)
                _enumerableServices.TryAdd(typeof(IOnMarketDataService), onMarketDataServices);

            // Fire IHostApplicationLifetime.Started
            //_lifetime.NotifyStarted();

        }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public void Configure(object[] ninjascriptObjects)
        {
            //_logger.Configuring();

            IList<IHostedService> configureServices = (IList<IHostedService>)Services.GetServices<IHostedService>();
                        
            if (configureServices != null && configureServices.Count > 0)
            {
                foreach (IHostedService configureService in configureServices)
                {
                    // Fire IHostedService.Start
                    configureService.Configure(ninjascriptObjects);
                }
            }

            //_logger.Configured();

        }

        /// <inheritdoc/>
        public void DataLoaded(object[] ninjascriptObjects)
        {
            //_logger.ConfiguringWhenDataLoaded();

            IList<IHostedService> dataLoadedServices = (IList<IHostedService>)Services.GetServices<IHostedService>();

            if (dataLoadedServices != null && dataLoadedServices.Count > 0)
            {
                foreach (IHostedService dataLoadedService in dataLoadedServices)
                {
                    // Fire IHostedService.Start
                    dataLoadedService.DataLoaded(ninjascriptObjects);
                }
            }

            //_logger.ConfiguredWhenDataLoaded();
        }

        /// <inheritdoc/>
        public void OnBarUpdate()
        {
            ExecuteServices<IOnBarUpdateService>();
        }

        /// <inheritdoc/>
        public void OnMarketData()
        {
            ExecuteServices<IOnMarketDataService>();
        }

        /// <inheritdoc/>
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

        private void ExecuteServices<T>()
        {
            if (!_enumerableServices.TryGetValue(typeof(T), out IEnumerable<object> services))
                return;

            switch (services)
            {
                case IEnumerable<IOnBarUpdateService> onBarUpdateServices:
                    ForEach(onBarUpdateServices, (onBarUpdateService) => onBarUpdateService.OnBarUpdate());
                    break;

                case IEnumerable<IOnMarketDataService> onMarketDataServices:
                    ForEach(onMarketDataServices, (onMarketDataService) => onMarketDataService.OnMarketData());
                    break;

            }
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
