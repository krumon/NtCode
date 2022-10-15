﻿using Nt.Core.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Hosting.Internal
{
    internal class NinjascriptsHost : INinjascriptsHost, IAsyncDisposable
    {

        #region Private members

        //private readonly ILogger<Host> _logger;
        private readonly IHostLifetime _hostLifetime;
        private readonly NinjascriptLifetime _ninjascriptLifetime;
        //private readonly HostOptions _options;
        private readonly INinjascriptsHostEnvironment _hostEnvironment;
        //private readonly PhysicalFileProvider _defaultProvider;
        //private IEnumerable<IHostedService> _hostedServices;
        private volatile bool _stopCalled;

        #endregion

        #region Public properties

        /// <inheritdoc/>
        public INinjascriptsServiceProvider Services { get; }

        #endregion

        #region Constructors

        public NinjascriptsHost(
            INinjascriptsServiceProvider services,
            INinjascriptsHostEnvironment hostEnvironment,
            //PhysicalFileProvider defaultProvider,
            INinjascriptLifetime ninjascriptLifetime,
            //ILogger<Host> logger,
            IHostLifetime hostLifetime
            //IOptions<HostOptions> options
            )
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _ninjascriptLifetime = (ninjascriptLifetime ?? throw new ArgumentNullException(nameof(ninjascriptLifetime))) as NinjascriptLifetime;
            _hostEnvironment = hostEnvironment;
            //_defaultProvider = defaultProvider;

            if (_ninjascriptLifetime is null)
            {
                throw new ArgumentException("Replacing IHostApplicationLifetime is not supported.", nameof(ninjascriptLifetime));
            }
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hostLifetime = hostLifetime ?? throw new ArgumentNullException(nameof(hostLifetime));
            //_options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }


        #endregion

        #region Public mnethods



        #endregion

        #region Implementation methods

        /// <inheritdoc/>
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            //_logger.Starting();

            using (var combinedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)) // _applicationLifetime.ApplicationStopping))
            {
                CancellationToken combinedCancellationToken = combinedCancellationTokenSource.Token;
                //await _hostLifetime.WaitForStartAsync(combinedCancellationToken).ConfigureAwait(false);

                //combinedCancellationToken.ThrowIfCancellationRequested();

                //_hostedServices = Services.GetService<IEnumerable<IHostedService>>();
                //foreach (IHostedService hostedService in _hostedServices)
                //{
                //    // Fire IHostedService.Start
                //    await hostedService.StartAsync(combinedCancellationToken).ConfigureAwait(false);

                //    if (hostedService is BackgroundService backgroundService)
                //    {
                //        _ = TryExecuteBackgroundServiceAsync(backgroundService);
                //    }
                //}
            }

            // Fire IHostApplicationLifetime.Started
            //_ninjascriptLifetime.NotifyStarted();

            //_logger.Started();

            // Remove
            await Task.Delay(1);
        }

        /// <inheritdoc/>
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            //_stopCalled = true;
            //_logger.Stopping();

            //using (var cts = new CancellationTokenSource(_options.ShutdownTimeout))
            //using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken))
            //{
            //    CancellationToken token = linkedCts.Token;
            //    // Trigger IHostApplicationLifetime.ApplicationStopping
            //    _applicationLifetime.StopApplication();

            //    IList<Exception> exceptions = new List<Exception>();
            //    if (_hostedServices != null) // Started?
            //    {
            //        foreach (IHostedService hostedService in _hostedServices.Reverse())
            //        {
            //            try
            //            {
            //                await hostedService.StopAsync(token).ConfigureAwait(false);
            //            }
            //            catch (Exception ex)
            //            {
            //                exceptions.Add(ex);
            //            }
            //        }
            //    }

            //    // Fire IHostApplicationLifetime.Stopped
            //    _applicationLifetime.NotifyStopped();

            //    try
            //    {
            //        await _hostLifetime.StopAsync(token).ConfigureAwait(false);
            //    }
            //    catch (Exception ex)
            //    {
            //        exceptions.Add(ex);
            //    }

            //    if (exceptions.Count > 0)
            //    {
            //        var ex = new AggregateException("One or more hosted services failed to stop.", exceptions);
            //        _logger.StoppedWithException(ex);
            //        throw ex;
            //    }
            //}

            //_logger.Stopped();

            // Remove
            await Task.Delay(1);
        }

        /// <inheritdoc/>
        public void Dispose() => DisposeAsync().AsTask().GetAwaiter().GetResult();

        public async ValueTask DisposeAsync()
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
            await DisposeAsync(Services).ConfigureAwait(false);

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

        #endregion

        #region Private methods

        private async Task TryExecuteBackgroundServiceAsync() //BackgroundService backgroundService)
        {
            //// backgroundService.ExecuteTask may not be set (e.g. if the derived class doesn't call base.StartAsync)
            //Task backgroundTask = backgroundService.ExecuteTask;
            //if (backgroundTask == null)
            //{
            //    return;
            //}

            //try
            //{
            //    await backgroundTask.ConfigureAwait(false);
            //}
            //catch (Exception ex)
            //{
            //    // When the host is being stopped, it cancels the background services.
            //    // This isn't an error condition, so don't log it as an error.
            //    if (_stopCalled && backgroundTask.IsCanceled && ex is OperationCanceledException)
            //    {
            //        return;
            //    }

            //    _logger.BackgroundServiceFaulted(ex);
            //    if (_options.BackgroundServiceExceptionBehavior == BackgroundServiceExceptionBehavior.StopHost)
            //    {
            //        _logger.BackgroundServiceStoppingHost(ex);
            //        _applicationLifetime.StopApplication();
            //    }
            //}

            // Remove
            await Task.Delay(1);
        }

        #endregion

        #region Copy from MS

        //private readonly ILogger<Host> _logger;
        //private readonly IHostLifetime _hostLifetime;
        //private readonly ApplicationLifetime _applicationLifetime;
        //private readonly HostOptions _options;
        //private readonly IHostEnvironment _hostEnvironment;
        //private readonly PhysicalFileProvider _defaultProvider;
        //private IEnumerable<IHostedService> _hostedServices;
        //private volatile bool _stopCalled;

        //public Host(IServiceProvider services,
        //            IHostEnvironment hostEnvironment,
        //            PhysicalFileProvider defaultProvider,
        //            IHostApplicationLifetime applicationLifetime,
        //            ILogger<Host> logger,
        //            IHostLifetime hostLifetime,
        //            IOptions<HostOptions> options)
        //{
        //    Services = services ?? throw new ArgumentNullException(nameof(services));
        //    _applicationLifetime = (applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime))) as ApplicationLifetime;
        //    _hostEnvironment = hostEnvironment;
        //    _defaultProvider = defaultProvider;

        //    if (_applicationLifetime is null)
        //    {
        //        throw new ArgumentException("Replacing IHostApplicationLifetime is not supported.", nameof(applicationLifetime));
        //    }
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //    _hostLifetime = hostLifetime ?? throw new ArgumentNullException(nameof(hostLifetime));
        //    _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        //}

        //public IServiceProvider Services { get; }

        //public async Task StartAsync(CancellationToken cancellationToken = default)
        //{
        //    _logger.Starting();

        //    using var combinedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _applicationLifetime.ApplicationStopping);
        //    CancellationToken combinedCancellationToken = combinedCancellationTokenSource.Token;

        //    await _hostLifetime.WaitForStartAsync(combinedCancellationToken).ConfigureAwait(false);

        //    combinedCancellationToken.ThrowIfCancellationRequested();
        //    _hostedServices = Services.GetService<IEnumerable<IHostedService>>();

        //    foreach (IHostedService hostedService in _hostedServices)
        //    {
        //        // Fire IHostedService.Start
        //        await hostedService.StartAsync(combinedCancellationToken).ConfigureAwait(false);

        //        if (hostedService is BackgroundService backgroundService)
        //        {
        //            _ = TryExecuteBackgroundServiceAsync(backgroundService);
        //        }
        //    }

        //    // Fire IHostApplicationLifetime.Started
        //    _applicationLifetime.NotifyStarted();

        //    _logger.Started();
        //}

        //private async Task TryExecuteBackgroundServiceAsync(BackgroundService backgroundService)
        //{
        //    // backgroundService.ExecuteTask may not be set (e.g. if the derived class doesn't call base.StartAsync)
        //    Task backgroundTask = backgroundService.ExecuteTask;
        //    if (backgroundTask == null)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        await backgroundTask.ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        // When the host is being stopped, it cancels the background services.
        //        // This isn't an error condition, so don't log it as an error.
        //        if (_stopCalled && backgroundTask.IsCanceled && ex is OperationCanceledException)
        //        {
        //            return;
        //        }

        //        _logger.BackgroundServiceFaulted(ex);
        //        if (_options.BackgroundServiceExceptionBehavior == BackgroundServiceExceptionBehavior.StopHost)
        //        {
        //            _logger.BackgroundServiceStoppingHost(ex);
        //            _applicationLifetime.StopApplication();
        //        }
        //    }
        //}

        //public async Task StopAsync(CancellationToken cancellationToken = default)
        //{
        //    _stopCalled = true;
        //    _logger.Stopping();

        //    using (var cts = new CancellationTokenSource(_options.ShutdownTimeout))
        //    using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken))
        //    {
        //        CancellationToken token = linkedCts.Token;
        //        // Trigger IHostApplicationLifetime.ApplicationStopping
        //        _applicationLifetime.StopApplication();

        //        IList<Exception> exceptions = new List<Exception>();
        //        if (_hostedServices != null) // Started?
        //        {
        //            foreach (IHostedService hostedService in _hostedServices.Reverse())
        //            {
        //                try
        //                {
        //                    await hostedService.StopAsync(token).ConfigureAwait(false);
        //                }
        //                catch (Exception ex)
        //                {
        //                    exceptions.Add(ex);
        //                }
        //            }
        //        }

        //        // Fire IHostApplicationLifetime.Stopped
        //        _applicationLifetime.NotifyStopped();

        //        try
        //        {
        //            await _hostLifetime.StopAsync(token).ConfigureAwait(false);
        //        }
        //        catch (Exception ex)
        //        {
        //            exceptions.Add(ex);
        //        }

        //        if (exceptions.Count > 0)
        //        {
        //            var ex = new AggregateException("One or more hosted services failed to stop.", exceptions);
        //            _logger.StoppedWithException(ex);
        //            throw ex;
        //        }
        //    }

        //    _logger.Stopped();
        //}

        //public void Dispose() => DisposeAsync().AsTask().GetAwaiter().GetResult();

        //public async ValueTask DisposeAsync()
        //{
        //    // The user didn't change the ContentRootFileProvider instance, we can dispose it
        //    if (ReferenceEquals(_hostEnvironment.ContentRootFileProvider, _defaultProvider))
        //    {
        //        // Dispose the content provider
        //        await DisposeAsync(_hostEnvironment.ContentRootFileProvider).ConfigureAwait(false);
        //    }
        //    else
        //    {
        //        // In the rare case that the user replaced the ContentRootFileProvider, dispose it and the one
        //        // we originally created
        //        await DisposeAsync(_hostEnvironment.ContentRootFileProvider).ConfigureAwait(false);
        //        await DisposeAsync(_defaultProvider).ConfigureAwait(false);
        //    }

        //    // Dispose the service provider
        //    await DisposeAsync(Services).ConfigureAwait(false);

        //    static async ValueTask DisposeAsync(object o)
        //    {
        //        switch (o)
        //        {
        //            case IAsyncDisposable asyncDisposable:
        //                await asyncDisposable.DisposeAsync().ConfigureAwait(false);
        //                break;
        //            case IDisposable disposable:
        //                disposable.Dispose();
        //                break;
        //        }
        //    }
        //}

        #endregion

    }
}
