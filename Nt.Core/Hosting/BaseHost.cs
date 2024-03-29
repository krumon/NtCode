﻿using Nt.Core.DependencyInjection;
using Nt.Core.FileProviders;
using Nt.Core.Hosting.Internal;
using Nt.Core.Logging;
using Nt.Core.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Represents the host.
    /// </summary>
    public abstract class BaseHost : IHost
    {

        private readonly ILogger<BaseHost> _logger;
        private readonly IHostLifetime _hostLifetime;
        private readonly ApplicationLifetime _applicationLifetime;
        private readonly HostOptions _options;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly PhysicalFileProvider _defaultProvider;
        private IEnumerable<IHostedService> _hostedServices;
        private volatile bool _stopCalled;

        public IServiceProvider Services { get; private set; }

        /// <summary>
        /// Create <see cref="Host"/> default instance.
        /// </summary>
        /// <param name="services">The host service provider.</param>
        /// <param name="options">The host options.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BaseHost(
            IServiceProvider services,
            IHostEnvironment hostEnvironment,
            PhysicalFileProvider defaultProvider,
            IHostApplicationLifetime applicationLifetime,
            ILogger<BaseHost> logger,
            IOptions<HostOptions> options
            //IHostLifetime hostLifetime
            )
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _applicationLifetime = (applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime))) as ApplicationLifetime;
            _hostEnvironment = hostEnvironment;
            _defaultProvider = defaultProvider;

            if (_applicationLifetime is null)
                throw new ArgumentException("Replacing IHostApplicationLifetime is not supported.", nameof(applicationLifetime));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            //_hostLifetime = hostLifetime ?? throw new ArgumentNullException(nameof(hostLifetime));

            //// Fire IHostApplicationLifetime.Started
            //_lifetime.NotifyStarted();

        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            _logger.Starting();

            using (var combinedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _applicationLifetime.ApplicationStopping))
            {
                CancellationToken combinedCancellationToken = combinedCancellationTokenSource.Token;

                await _hostLifetime.WaitForStartAsync(combinedCancellationToken).ConfigureAwait(false);
                combinedCancellationToken.ThrowIfCancellationRequested();

                _hostedServices = Services.GetService<IEnumerable<IHostedService>>();

                foreach (IHostedService hostedService in _hostedServices)
                {
                    //// Fire IHostedService.Start
                    //await hostedService.StartAsync(combinedCancellationToken).ConfigureAwait(false);

                    //if (hostedService is BackgroundService backgroundService)
                    //{
                    //    _ = TryExecuteBackgroundServiceAsync(backgroundService);
                    //}
                }

                // Fire IHostApplicationLifetime.Started
                _applicationLifetime.NotifyStarted();

                _logger.Started();
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            _stopCalled = true;
            _logger.Stopping();

            using (var cts = new CancellationTokenSource(_options.ShutdownTimeout))
            using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken))
            {
                CancellationToken token = linkedCts.Token;
                // Trigger IHostApplicationLifetime.ApplicationStopping
                _applicationLifetime.StopApplication();

                IList<Exception> exceptions = new List<Exception>();
                if (_hostedServices != null) // Started?
                {
                    //foreach (IHostedService hostedService in _hostedServices.Reverse())
                    //{
                    //    try
                    //    {
                    //        await hostedService.StopAsync(token).ConfigureAwait(false);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        exceptions.Add(ex);
                    //    }
                    //}
                }

                // Fire IHostApplicationLifetime.Stopped
                _applicationLifetime.NotifyStopped();

                try
                {
                    await _hostLifetime.StopAsync(token).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }

                if (exceptions.Count > 0)
                {
                    var ex = new AggregateException("One or more hosted services failed to stop.", exceptions);
                    _logger.StoppedWithException(ex);
                    throw ex;
                }
            }

            _logger.Stopped();
        }
        public void Dispose() => DisposeAsync().AsTask().GetAwaiter().GetResult();
        public async ValueTask DisposeAsync()
        {
            // The user didn't change the ContentRootFileProvider instance, we can dispose it
            if (ReferenceEquals(_hostEnvironment.ContentRootFileProvider, _defaultProvider))
            {
                // Dispose the content provider
                await DisposeAsync(_hostEnvironment.ContentRootFileProvider).ConfigureAwait(false);
            }
            else
            {
                // In the rare case that the user replaced the ContentRootFileProvider, dispose it and the one
                // we originally created
                await DisposeAsync(_hostEnvironment.ContentRootFileProvider).ConfigureAwait(false);
                await DisposeAsync(_defaultProvider).ConfigureAwait(false);
            }

            // Dispose the service provider
            await DisposeAsync(Services).ConfigureAwait(false);

        }
        static async ValueTask DisposeAsync(object o)
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
}
