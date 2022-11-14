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
        private List<Action<HostOptions>> _configureHostOptionsActions;
        private List<Action<DataSeriesBuilder>> _configureDataSeriesActions;
        private ConcurrentDictionary<object, IHostedService> _services = new ConcurrentDictionary<object, IHostedService>();
        private HostOptions _hostOptions = HostOptions.Default;

        public IHostBuilder ConfigureHostOptions(Action<HostOptions> optionsDelegate)
        {
            if (_configureHostOptionsActions == null)
                _configureHostOptionsActions= new List<Action<HostOptions>>();
            _configureHostOptionsActions.Add(optionsDelegate ?? throw new ArgumentNullException(nameof(optionsDelegate)));
            return this;
        }

        public IHostBuilder UseDataSeries(Action<DataSeriesBuilder> dataSeriesDelegate)
        {
            if (_configureDataSeriesActions == null)
                _configureDataSeriesActions = new List<Action<DataSeriesBuilder>>();
            _configureDataSeriesActions.Add(dataSeriesDelegate ?? throw new ArgumentNullException(nameof(dataSeriesDelegate)));
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
                ConfigureOptions();

            ConfigureDataSeries();

            var hostService = new HostService(_services,_hostOptions);

            return hostService;

        }


        private void ConfigureOptions()
        {
            foreach (Action<HostOptions> action in _configureHostOptionsActions)
                action(_hostOptions);
        }

        private void ConfigureDataSeries()
        {
            DataSeriesBuilder builder = new DataSeriesBuilder();
            foreach (Action<DataSeriesBuilder> action in _configureDataSeriesActions)
            {
                action(builder);
            }
            _services.TryAdd(NinjascriptServiceType.DataSeries,builder.Build());
        }

    }
}
