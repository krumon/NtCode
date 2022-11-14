using System;
using System.Collections.Concurrent;

namespace Nt.Core.Data
{
    /// <summary>
    /// Represents the ninjascript host.
    /// </summary>
    public class HostService : IHostService
    {

        #region Private members

        //private readonly ILogger<Host> _logger;
        //private readonly IHostLifetime _hostLifetime;
        //private readonly NinjascriptLifetime _ninjascriptLifetime;
        private readonly HostOptions _options;
        //private readonly INinjascriptHostEnvironment _hostEnvironment;
        //private readonly PhysicalFileProvider _defaultProvider;
        //private IEnumerable<IHostedService> _hostedServices;
        //private volatile bool _stopCalled;

        #endregion

        #region Public properties

        /// <inheritdoc/>
        public ConcurrentDictionary<object, IHostedService> Services { get; }

        #endregion

        #region Constructors

        public HostService(
            ConcurrentDictionary<object, IHostedService> services,
            HostOptions options
            )
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public object GetService(object key)
        {
            if (Services.TryGetValue(key, out var service))
                return service;
            return null;
        }

        #endregion

    }
}
