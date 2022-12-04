using Nt.Core.DependencyInjection;
using Nt.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ConcurrentDictionary<Type, IEnumerable<object>> _hostServices = new ConcurrentDictionary<Type,IEnumerable<object>>();
        private readonly HostOptions _options;

        #endregion

        #region Public properties

        /// <inheritdoc/>
        public IServiceProvider Services { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="Host"/> default instance.
        /// </summary>
        /// <param name="services">The host service provider.</param>
        /// <param name="options">The host options.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Host(
            IServiceProvider services,
            HostOptions options
            )
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public void Start(object[] ninjascriptObjects)
        {
            //_logger.Starting();

            //await _hostLifetime.WaitForStartAsync(combinedCancellationToken).ConfigureAwait(false);


            IList<object> _hostedServices = Services.GetServices<IHostedService>();
                        
            if (_hostedServices != null && _hostedServices.Count > 0 && !_options.IsInDesignMode)
            {
                foreach (IHostedService hostedService in _hostedServices)
                {
                    // Fire IHostedService.Start
                    hostedService.Configure(ninjascriptObjects);
                }
            }

            // Fire IHostApplicationLifetime.Started
            //_ninjascriptLifetime.NotifyStarted();

            //_logger.Started();

        }

        /// <inheritdoc/>
        public void Stop()
        {
            //_stopCalled = true;
            //_logger.Stopping();

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

            //_logger.Stopped();

        }

        /// <inheritdoc/>
        public void Dispose()
        {
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
            DisposeAsync(Services).ConfigureAwait(false);

            async ValueTask DisposeAsync(object o)
            {
                switch (o)
                {
                    case IAsyncDisposable asyncDisposable:
                        await asyncDisposable.DisposeAsync().ConfigureAwait(false);
                        break;
                    case IDisposable disposable:
                        disposable.Dispose();
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public void ExecuteServices<T>()
        {
            IEnumerable<T> services;

            if (typeof(T).IsAssignableFrom(typeof(IDataLoadedService)))
                return;
            
            services = (IEnumerable<T>)_hostServices.GetOrAdd(typeof(T), (IEnumerable<object>)Services.GetServices<T>());
            if (services == null)
                return;

            switch (typeof(T))
            {
                case IOnBarUpdateService configureService:
                    ForEach<T>(services, (_) => configureService.OnBarUpdate());
                    break;

                case IMarketDataService configureService:
                    ForEach<T>(services, (_) => configureService.OnMarketData());
                    break;

            }
        }

        #endregion

        private void ForEach<T>(IEnumerable<T> services, Action<T> action)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            foreach (T service in services)
            {
                action(service);
            }
        }
    }
}
