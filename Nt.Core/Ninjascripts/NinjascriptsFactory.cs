using Nt.Core.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// Object used to configure the indicators system and create instances of
    /// <see cref="IIndicators"/> from the registered <see cref="IIndicatorsProvider"/>.
    /// </summary>
    public class NinjascriptsFactory : INinjascriptsFactory
    {
        private readonly Dictionary<string, Ninjascripts> _ninjascripts = new Dictionary<string, Ninjascripts>(StringComparer.Ordinal);
        private readonly List<ProviderRegistration> _providerRegistrations = new List<ProviderRegistration>();
        private readonly object _sync = new object();
        private volatile bool _disposed;
        private readonly IDisposable _changeTokenRegistration;
        //private IndicatorFilterOptions _filterOptions;

        ///// <summary>
        ///// Creates a new <see cref="NinjascriptFactory"/> instance.
        ///// </summary>
        //public NinjascriptFactory() : this(Array.Empty<INinjascriptProvider>())
        //{
        //}

        ///// <summary>
        ///// Creates a new <see cref="IndicatorFactory"/> instance.
        ///// </summary>
        ///// <param name="providers">The providers to use in producing <see cref="IIndicator"/> instances.</param>
        //public NinjascriptFactory(IEnumerable<INinjascriptProvider> providers) : this(providers, new StaticIndicatorFilterOptionsMonitor(new IndicatorFilterOptions()))
        //{
        //}

        ///// <summary>
        ///// Creates a new <see cref="IndicatorFactory"/> instance.
        ///// </summary>
        ///// <param name="providers">The providers to use in producing <see cref="IIndicator"/> instances.</param>
        ///// <param name="filterOptions">The filter options to use.</param>
        //public NinjascriptFactory(IEnumerable<INinjascriptProvider> providers, IndicatorFilterOptions filterOptions) : this(providers, new StaticIndicatorFilterOptionsMonitor(filterOptions))
        //{
        //}

        ///// <summary>
        ///// Creates a new <see cref="IndicatorFactory"/> instance.
        ///// </summary>
        ///// <param name="providers">The providers to use in producing <see cref="IIndicator"/> instances.</param>
        ///// <param name="filterOption">The filter option to use.</param>
        //public NinjascriptFactory(IEnumerable<INinjascriptProvider> providers, IOptionsMonitor<IndicatorFilterOptions> filterOption)
        //{
        //    // Register the indicators providers
        //    foreach (INinjascriptProvider provider in providers)
        //    {
        //        //// Check filters and adds the provider if the indicator is enabled.
        //        //if(CheckFilters(provider, filterOption.CurrentValue))
        //        //    AddProviderRegistration(provider, dispose: false);
        //    }

        //    //// Register OnChange method and refresh the filters in the registered indicators
        //    //_changeTokenRegistration = filterOption.OnChange(RefreshFilters);
        //    //RefreshFilters(filterOption.CurrentValue);
        //}

        // Provisional
        public NinjascriptsFactory(IEnumerable<INinjascriptsProvider> providers)
        {
            // Register the indicators providers
            foreach (INinjascriptsProvider provider in providers)
            {
                //// Check filters and adds the provider if the indicator is enabled.
                //if(CheckFilters(provider, filterOption.CurrentValue))
                //    AddProviderRegistration(provider, dispose: false);
            }

            //// Register OnChange method and refresh the filters in the registered indicators
            //_changeTokenRegistration = filterOption.OnChange(RefreshFilters);
            //RefreshFilters(filterOption.CurrentValue);
        }

        /// <summary>
        /// Creates new instance of <see cref="IIndicatorsFactory"/> configured using provided <paramref name="configure"/> delegate.
        /// </summary>
        /// <param name="configure">A delegate to configure the <see cref="IIndicatorsBuilder"/>.</param>
        /// <returns>The <see cref="IIndicatorFactory"/> that was created.</returns>
        public static INinjascriptsFactory Create(Action<INinjascriptsBuilder> configure)
        {
            // Create the service collection
            var serviceCollection = new ServiceCollection();
            // Add required services (IIndicatorFactory, IIndicator<> and IConfigureOptions<ConfigureFiltersOptions>)
            // and create the IIndicatorBuilder with the configured services to add the services to the collection.
            //serviceCollection.AddIndicators(configure);
            // Create a service provider with the required and configure services.
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            // Gets the IIndicatorFactory.
            INinjascriptsFactory ninjascriptsFactory = serviceProvider.GetService<INinjascriptsFactory>();
            // Returns the disposing logger factory.
            return new DisposingNinjascriptFactory(ninjascriptsFactory, serviceProvider);
        }

        /// <summary>
        /// Creates an <see cref="IIndicators"/> with the given <paramref name="categoryName"/>.
        /// </summary>
        /// <param name="categoryName">The category name for indicators produced by the indicator.</param>
        /// <returns>The <see cref="IIndicators"/> that was created.</returns>
        public INinjascripts CreateNinjascript(string categoryName)
        {
            if (CheckDisposed())
            {
                throw new ObjectDisposedException(nameof(NinjascriptsFactory));
            }

            lock (_sync)
            {
                if (!_ninjascripts.TryGetValue(categoryName, out Ninjascripts ninjascript))
                {
                    ninjascript = new Ninjascripts
                    {
                        //IndicatorsInfo = CreateIndicators(categoryName),
                    };

                    //logger.MessageLoggers = ApplyFilters(logger.Loggers);

                    _ninjascripts[categoryName] = ninjascript;
                }

                return ninjascript;
            }
        }

        /// <summary>
        /// Adds the given provider to those used in creating <see cref="IIndicator"/> instances.
        /// </summary>
        /// <param name="provider">The <see cref="IIndicatorProvider"/> to add.</param>
        public void AddProvider(INinjascriptsProvider provider)
        {
            if (CheckDisposed())
                throw new ObjectDisposedException(nameof(NinjascriptsFactory));

            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            lock (_sync)
            {
                AddProviderRegistration(provider, dispose: true);

                //foreach (KeyValuePair<string, Ninjascript> existingIndicator in _ninjascripts)
                //{
                //    Indicator indicator = existingIndicator.Value;
                //    IndicatorInfo[] indicatorInformation = indicator.IndicatorsInfo;

                //    int newIndicatorIndex = indicatorInformation.Length;
                //    Array.Resize(ref indicatorInformation, indicatorInformation.Length + 1);
                //    indicatorInformation[newIndicatorIndex] = new IndicatorInfo(provider, existingIndicator.Key);

                //    indicator.IndicatorsInfo = indicatorInformation;
                //    //logger.MessageLoggers = ApplyFilters(logger.Loggers);
                //}
            }
        }

        //private void RefreshFilters(IndicatorFilterOptions filterOptions)
        //{
        //    lock (_sync)
        //    {
        //        _filterOptions = filterOptions;
        //        foreach (KeyValuePair<string, Indicator> regiteredIndicator in _ninjascripts)
        //        {
        //            Indicator indicator = regiteredIndicator.Value;
        //            //indicator.MessageLoggers = ApplyFilters(logger.Loggers);
        //        }
        //    }
        //}

        //private void RefreshFilters(IConfigureOptions<IndicatorFilterOptions> filterOptions)
        //{
        //    if (_filterOptions == null) _filterOptions = new IndicatorFilterOptions();
        //    lock (_sync)
        //    {
        //        filterOptions.Configure(_filterOptions);
        //        foreach (KeyValuePair<string, Indicator> registeredIndicator in _ninjascripts)
        //        {
        //            Indicator indicator = registeredIndicator.Value;
        //            //logger.MessageLoggers = ApplyFilters(logger.Loggers);
        //        }
        //    }
        //}

        private void AddProviderRegistration(INinjascriptsProvider provider, bool dispose)
        {
            _providerRegistrations.Add(new ProviderRegistration
            {
                Provider = provider,
                ShouldDispose = dispose
            });
        }

        //private IndicatorInfo[] CreateIndicators(string indicatorName)
        //{
        //    var indicators = new IndicatorInfo[_providerRegistrations.Count];
        //    for (int i = 0; i < _providerRegistrations.Count; i++)
        //    {
        //        indicators[i] = new IndicatorInfo(_providerRegistrations[i].Provider, indicatorName);
        //    }
        //    return indicators;
        //}

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

        //private bool CheckFilters(IIndicatorProvider provider, IndicatorFilterOptions filterOptions)
        //{
        //    Type t = provider.GetType();
        //    string fullName = t.FullName;
        //    string alias = ProviderAliasAttribute.GetAlias(t);
        //    if (alias == null)
        //        return false;
        //    if (filterOptions == null)
        //        return false;
        //    foreach (IndicatorFilterRule rule in filterOptions.Rules)
        //        if (rule.ProviderName == alias || rule.ProviderName == fullName)
        //            return true;

        //    return false;
        //}

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
            public INinjascriptsProvider Provider;
            public bool ShouldDispose;
        }
        private sealed class DisposingNinjascriptFactory : INinjascriptsFactory
        {
            private readonly INinjascriptsFactory _indicatorFactory;

            private readonly ServiceProvider _serviceProvider;

            public DisposingNinjascriptFactory(INinjascriptsFactory ninjascriptsFactory, ServiceProvider serviceProvider)
            {
                _indicatorFactory = ninjascriptsFactory;
                _serviceProvider = serviceProvider;
            }

            public void Dispose()
            {
                _serviceProvider.Dispose();
            }

            public INinjascripts CreateNinjascript(string categoryName)
            {
                return _indicatorFactory.CreateNinjascript(categoryName);
            }

            public void AddProvider(INinjascriptsProvider provider)
            {
                _indicatorFactory.AddProvider(provider);
            }
        }
    }
}
