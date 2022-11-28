using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    /// <summary>
    /// Default services host builder.
    /// </summary>
    public class HostBuilder : IHostBuilder
    {
        private bool _built;
        private bool _requiredServicesIsAdded = true;
        private List<Action<HostOptions>> _configureHostOptionsActions;
        private List<Action<IServiceCollection>> _configureServicesActions;
        private HostOptions _hostOptions = HostOptions.Default;
       
        private List<Action<DataSeriesBuilder>> _configureDataSeriesActions;
        private ConcurrentDictionary<RequiredServiceType, IRequiredService> _requiredServices = new ConcurrentDictionary<RequiredServiceType, IRequiredService>();
        private ConcurrentDictionary<OptionalServiceType, IOptionalService> _optionalServices = new ConcurrentDictionary<OptionalServiceType, IOptionalService>();

        /// <summary>
        /// Set up the options for the builder itself. This will be used to initialize the <see cref="Host"/>
        /// for use later in the build process. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="optionsDelegate">The delegate for configuring the <see cref="IHostBuilder"/> that will be used
        /// to construct the <see cref="IServiceProvider"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public IHostBuilder ConfigureHostOptions(Action<HostOptions> optionsDelegate)
        {
            if (_configureHostOptionsActions == null)
                _configureHostOptionsActions= new List<Action<HostOptions>>();
            _configureHostOptionsActions.Add(optionsDelegate ?? throw new ArgumentNullException(nameof(optionsDelegate)));
            return this;
        }

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureServicesDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IServiceProvider"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public IHostBuilder ConfigureServices(Action<IServiceCollection> configureServicesDelegate)
        {
            _configureServicesActions.Add(configureServicesDelegate ?? throw new ArgumentNullException(nameof(configureServicesDelegate)));
            return this;
        }

        public IHostBuilder UseDataSeries(Action<DataSeriesBuilder> dataSeriesDelegate)
        {
            if (_configureDataSeriesActions == null)
                _configureDataSeriesActions = new List<Action<DataSeriesBuilder>>();
            _configureDataSeriesActions.Add(dataSeriesDelegate ?? throw new ArgumentNullException(nameof(dataSeriesDelegate)));
            return this;
        }

        public IHostBuilder ConfigureDefaults(string[] args)
        {
            AddRequiredServices();
            return this;
        }

        public IHostService Build()
        {
            if (_built)
            {
                throw new InvalidOperationException("The host can only be built once.");
            }
            _built = true;

            if (_configureHostOptionsActions != null)
                ConfigHostOptions();

            if (!_requiredServicesIsAdded)
                AddRequiredServices();
            AddOptionalServices();

            var hostService = new HostService(_requiredServices,_optionalServices,_hostOptions);

            return hostService;

        }

        private void ConfigHostOptions()
        {
            foreach (Action<HostOptions> action in _configureHostOptionsActions)
                action(_hostOptions);
        }

        private void AddRequiredServices()
        {
            ChartDataService data = new ChartDataService();
            if (!_requiredServices.TryAdd(RequiredServiceType.Data, data))
                _requiredServicesIsAdded = false;

        }

        private void AddOptionalServices()
        {
            if (_configureDataSeriesActions != null)
                ConfigureDataSeries();

        }

        private void ConfigureDataSeries()
        {
            DataSeriesBuilder builder = new DataSeriesBuilder();
            foreach (Action<DataSeriesBuilder> action in _configureDataSeriesActions)
            {
                action(builder);
            }
            _optionalServices.TryAdd(OptionalServiceType.DataSeries,builder.Build());
        }

    }
}
