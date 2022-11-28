using System;
using System.Collections.Concurrent;

namespace Nt.Core.Data
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
        private readonly ConcurrentDictionary<RequiredServiceType, IRequiredService> _requiredServices;
        private readonly ConcurrentDictionary<OptionalServiceType, IOptionalService> _optionalServices;

        #endregion

        #region Public properties

        /// <inheritdoc/>
        public ConcurrentDictionary<RequiredServiceType, IRequiredService> RequiredServices => _requiredServices;

        /// <inheritdoc/>
        public ConcurrentDictionary<OptionalServiceType, IOptionalService> OptionalServices => _optionalServices;

        public IServiceProvider Services { get; }

        #endregion

        #region Constructors

        public Host(
            IServiceProvider services,
            HostOptions options
            )
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            //_requiredServices = requiredServices ?? throw new ArgumentNullException(nameof(requiredServices));
            //_optionalServices = optionalServices ?? throw new ArgumentNullException(nameof(optionalServices));
        }

        #endregion

        #region Public methods

        public object GetService<T>(IHostedService<T> key)
            where T : Enum
        {
            if (key is RequiredServiceType requiredServiceKey)
            {
                if (_requiredServices.TryGetValue(requiredServiceKey, out var service))
                    return service;
                throw new Exception("The service doesn't exist and is required.");
            }
                
            if (key is OptionalServiceType optionalServiceKey)
            {
                if (_optionalServices.TryGetValue(optionalServiceKey, out var service))
                    return service;
                return default;
            }

            return default;
                
        }

        /// <inheritdoc/>
        public object GetService(Type serviceType)
        {

            return default;
        }

        /// <inheritdoc/>
        public object GetService(object key)
        {
            if (key is Enum)
            {
                if (key is RequiredServiceType requiredServiceKey)
                    return GetRequiredService(requiredServiceKey);

                if (key is OptionalServiceType optionalServiceKey)
                    return GetOptionalService(optionalServiceKey);            }

            return default;
        }

        /// <inheritdoc/>
        public object GetRequiredService(RequiredServiceType key)
        {
            if (_requiredServices.TryGetValue(key, out var service))
                return service;
            throw new Exception("The service doesn't exist and is required.");
        }

        /// <inheritdoc/>
        public object GetOptionalService(OptionalServiceType key)
        {
            if (_optionalServices.TryGetValue(key, out var service))
                return service;
            return default;
        }

        #endregion

    }
}
