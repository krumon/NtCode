using Nt.Core.DependencyInjection;
using Nt.Core.Logging;
using Nt.Core.Options;
using Nt.Scripts.Indicators.Internal;
using System;
using System.Collections.Generic;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Object used to configure the indicators system and create instances of
    /// <see cref="IIndicator"/> from the registered <see cref="IIndicatorProvider"/>.
    /// </summary>
    public class IndicatorFactory : IIndicatorFactory
    {
        private readonly Dictionary<string, Indicator> _indicators = new Dictionary<string, Indicator>(StringComparer.Ordinal);
        private readonly List<ProviderRegistration> _providerRegistrations = new List<ProviderRegistration>();
        private readonly object _sync = new object();
        private volatile bool _disposed;
        private readonly IDisposable _changeTokenRegistration;
        private IndicatorFilterOptions _filterOptions;

        /// <summary>
        /// Creates a new <see cref="IndicatorFactory"/> instance.
        /// </summary>
        public IndicatorFactory() : this(Array.Empty<IIndicatorProvider>())
        {
        }

        /// <summary>
        /// Creates a new <see cref="IndicatorFactory"/> instance.
        /// </summary>
        /// <param name="providers">The providers to use in producing <see cref="IIndicator"/> instances.</param>
        public IndicatorFactory(IEnumerable<IIndicatorProvider> providers) : this(providers, new StaticIndicatorFilterOptionsMonitor(new IndicatorFilterOptions()))
        {
        }

        /// <summary>
        /// Creates a new <see cref="IndicatorFactory"/> instance.
        /// </summary>
        /// <param name="providers">The providers to use in producing <see cref="IIndicator"/> instances.</param>
        /// <param name="filterOptions">The filter options to use.</param>
        public IndicatorFactory(IEnumerable<IIndicatorProvider> providers, IndicatorFilterOptions filterOptions) : this(providers, new StaticIndicatorFilterOptionsMonitor(filterOptions))
        {
        }

        /// <summary>
        /// Creates a new <see cref="IndicatorFactory"/> instance.
        /// </summary>
        /// <param name="providers">The providers to use in producing <see cref="IIndicator"/> instances.</param>
        /// <param name="filterOption">The filter option to use.</param>
        public IndicatorFactory(IEnumerable<IIndicatorProvider> providers, IOptionsMonitor<IndicatorFilterOptions> filterOption)
        {
            // Register the indicators providers
            foreach (IIndicatorProvider provider in providers)
            {
                // Check filters and adds the provider if the indicator is enabled.
                if(CheckFilters(provider, filterOption.CurrentValue))
                    AddProviderRegistration(provider, dispose: false);
            }

            // Register OnChange method and refresh the filters in the registered indicators
            _changeTokenRegistration = filterOption.OnChange(RefreshFilters);
            RefreshFilters(filterOption.CurrentValue);
        }

        /// <summary>
        /// Creates new instance of <see cref="IIndicatorFactory"/> configured using provided <paramref name="configure"/> delegate.
        /// </summary>
        /// <param name="configure">A delegate to configure the <see cref="IIndicatorBuilder"/>.</param>
        /// <returns>The <see cref="IIndicatorFactory"/> that was created.</returns>
        public static IIndicatorFactory Create(Action<IIndicatorBuilder> configure)
        {
            // Create the service collection
            var serviceCollection = new ServiceCollection();
            // Add required services (IIndicatorFactory, IIndicator<> and IConfigureOptions<ConfigureFiltersOptions>)
            // and create the IIndicatorBuilder with the configured services to add the services to the collection.
            serviceCollection.AddIndicators(configure);
            // Create a service provider with the required and configure services.
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            // Gets the IIndicatorFactory.
            IIndicatorFactory loggerFactory = serviceProvider.GetService<IIndicatorFactory>();
            // Returns the disposing logger factory.
            return new DisposingIndicatorFactory(loggerFactory, serviceProvider);
        }

        /// <summary>
        /// Creates an <see cref="IIndicator"/> with the given <paramref name="categoryName"/>.
        /// </summary>
        /// <param name="categoryName">The category name for indicators produced by the indicator.</param>
        /// <returns>The <see cref="IIndicator"/> that was created.</returns>
        public IIndicator CreateIndicator(string categoryName)
        {
            if (CheckDisposed())
            {
                throw new ObjectDisposedException(nameof(IndicatorFactory));
            }

            lock (_sync)
            {
                if (!_indicators.TryGetValue(categoryName, out Indicator indicator))
                {
                    indicator = new Indicator
                    {
                        IndicatorsInfo = CreateIndicators(categoryName),
                    };

                    //logger.MessageLoggers = ApplyFilters(logger.Loggers);

                    _indicators[categoryName] = indicator;
                }

                return indicator;
            }
        }

        /// <summary>
        /// Adds the given provider to those used in creating <see cref="IIndicator"/> instances.
        /// </summary>
        /// <param name="provider">The <see cref="IIndicatorProvider"/> to add.</param>
        public void AddProvider(IIndicatorProvider provider)
        {
            if (CheckDisposed())
                throw new ObjectDisposedException(nameof(IndicatorFactory));

            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            lock (_sync)
            {
                AddProviderRegistration(provider, dispose: true);

                foreach (KeyValuePair<string, Indicator> existingIndicator in _indicators)
                {
                    Indicator indicator = existingIndicator.Value;
                    IndicatorInfo[] indicatorInformation = indicator.IndicatorsInfo;

                    int newIndicatorIndex = indicatorInformation.Length;
                    Array.Resize(ref indicatorInformation, indicatorInformation.Length + 1);
                    indicatorInformation[newIndicatorIndex] = new IndicatorInfo(provider, existingIndicator.Key);

                    indicator.IndicatorsInfo = indicatorInformation;
                    //logger.MessageLoggers = ApplyFilters(logger.Loggers);
                }
            }
        }

        private void RefreshFilters(IndicatorFilterOptions filterOptions)
        {
            lock (_sync)
            {
                _filterOptions = filterOptions;
                foreach (KeyValuePair<string, Indicator> regiteredIndicator in _indicators)
                {
                    Indicator indicator = regiteredIndicator.Value;
                    //indicator.MessageLoggers = ApplyFilters(logger.Loggers);
                }
            }
        }

        private void RefreshFilters(IConfigureOptions<IndicatorFilterOptions> filterOptions)
        {
            if (_filterOptions == null) _filterOptions = new IndicatorFilterOptions();
            lock (_sync)
            {
                filterOptions.Configure(_filterOptions);
                foreach (KeyValuePair<string, Indicator> registeredIndicator in _indicators)
                {
                    Indicator indicator = registeredIndicator.Value;
                    //logger.MessageLoggers = ApplyFilters(logger.Loggers);
                }
            }
        }

        private void AddProviderRegistration(IIndicatorProvider provider, bool dispose)
        {
            _providerRegistrations.Add(new ProviderRegistration
            {
                Provider = provider,
                ShouldDispose = dispose
            });
        }

        private IndicatorInfo[] CreateIndicators(string indicatorName)
        {
            var indicators = new IndicatorInfo[_providerRegistrations.Count];
            for (int i = 0; i < _providerRegistrations.Count; i++)
            {
                indicators[i] = new IndicatorInfo(_providerRegistrations[i].Provider, indicatorName);
            }
            return indicators;
        }

        //private MessageLogger[] ApplyFilters(LoggerInformation[] loggers)
        //{
        //    var messageLoggers = new List<MessageLogger>();

        //    foreach (LoggerInformation loggerInformation in loggers)
        //    {
        //        LoggerRuleSelector.Select(_filterOptions,
        //            loggerInformation.ProviderType,
        //            loggerInformation.Category,
        //            out LogLevel? minLevel,
        //            out Func<string, string, LogLevel, bool> filter);

        //        if (minLevel != null && minLevel > LogLevel.Critical)
        //        {
        //            continue;
        //        }

        //        messageLoggers.Add(new MessageLogger(loggerInformation.Logger, loggerInformation.Category, loggerInformation.ProviderType.FullName, minLevel, filter));

        //    }

        //    return messageLoggers.ToArray();
        //}

        private bool CheckFilters(IIndicatorProvider provider, IndicatorFilterOptions filterOptions)
        {
            Type t = provider.GetType();
            string fullName = t.FullName;
            string alias = ProviderAliasAttribute.GetAlias(t);
            if (alias == null)
                return false;
            if (filterOptions == null)
                return false;
            foreach (IndicatorFilterRule rule in filterOptions.Rules)
                if (rule.ProviderName == alias || rule.ProviderName == fullName)
                    return true;

            return false;
        }

        /// <summary>
        /// Check if the factory has been disposed.
        /// </summary>
        /// <returns>True when <see cref="Dispose()"/> as been called</returns>
        protected virtual bool CheckDisposed() => _disposed;

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                _changeTokenRegistration?.Dispose();

                foreach (ProviderRegistration registration in _providerRegistrations)
                {
                    try
                    {
                        if (registration.ShouldDispose)
                        {
                            registration.Provider.Dispose();
                        }
                    }
                    catch
                    {
                        // Swallow exceptions on dispose
                    }
                }
            }
        }

        private struct ProviderRegistration
        {
            public IIndicatorProvider Provider;
            public bool ShouldDispose;
        }
        private sealed class DisposingIndicatorFactory : IIndicatorFactory
        {
            private readonly IIndicatorFactory _indicatorFactory;

            private readonly ServiceProvider _serviceProvider;

            public DisposingIndicatorFactory(IIndicatorFactory indicatorFactory, ServiceProvider serviceProvider)
            {
                _indicatorFactory = indicatorFactory;
                _serviceProvider = serviceProvider;
            }

            public void Dispose()
            {
                _serviceProvider.Dispose();
            }

            public IIndicator CreateIndicator(string categoryName)
            {
                return _indicatorFactory.CreateIndicator(categoryName);
            }

            public void AddProvider(IIndicatorProvider provider)
            {
                _indicatorFactory.AddProvider(provider);
            }
        }
    }
}
