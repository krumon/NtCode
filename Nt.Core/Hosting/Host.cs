using Nt.Core.DependencyInjection;
using Nt.Core.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        ///// <summary>
        ///// <inheritdoc/>
        ///// </summary>
        //public void Configure([CallerMemberName] string name = "", params object[] ninjascriptObjects)
        //{
        //    if (name != "Configure")
        //        throw new Exception("The caller method must be 'Configure()");

        //    IEnumerable<IConfigureService> configureServices = (IEnumerable<IConfigureService>)Services.GetServices<IConfigureService>();
        //    if (configureServices == null)
        //        return;
        //    foreach(var configureService in configureServices)
        //        configureService.Configure(ninjascriptObjects);
        //}

        ///// <summary>
        ///// <inheritdoc/>
        ///// </summary>
        //public void DataLoaded(params object[] ninjascriptObjects)
        //{
        //    IEnumerable<IDataLoadedService> configureServices = (IEnumerable<IDataLoadedService>)Services.GetServices<IDataLoadedService>();
        //    if (configureServices == null)
        //        return;
        //    foreach(var configureService in configureServices)
        //        configureService.DataLoaded(ninjascriptObjects);
        //}

        #endregion

    }
}
