using Nt.Core.DependencyInjection;
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

            foreach (IIndicatorProvider provider in providers)
                AddProviderRegistration(provider, dispose: false);

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

        private void RefreshFilters(IndicatorFilterOptions filterOptions)
        {
            lock (_sync)
            {
                _filterOptions = filterOptions;
                foreach (KeyValuePair<string, Indicator> registeredLogger in _indicators)
                {
                    Indicator indicator = registeredLogger.Value;
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
                foreach (KeyValuePair<string, Indicator> registeredLogger in _indicators)
                {
                    Indicator logger = registeredLogger.Value;
                    //logger.MessageLoggers = ApplyFilters(logger.Loggers);
                }
            }
        }

        /// <summary>
        /// Creates an <see cref="IIndicator"/> with the given <paramref name="indicatorName"/>.
        /// </summary>
        /// <param name="indicatorName">The indicator name.</param>
        /// <returns>The <see cref="ILogger"/> that was created.</returns>
        public IIndicator CreateLogger(string indicatorName)
        {
            if (CheckDisposed())
            {
                throw new ObjectDisposedException(nameof(IndicatorFactory));
            }

            lock (_sync)
            {
                if (!_indicators.TryGetValue(indicatorName, out Indicator logger))
                {
                    logger = new Indicator
                    {
                        //Loggers = CreateLoggers(categoryName),
                    };

                    //logger.MessageLoggers = ApplyFilters(logger.Loggers);

                    _indicators[indicatorName] = logger;
                }

                return logger;
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

                foreach (KeyValuePair<string, Indicator> existingLogger in _indicators)
                {
                    Indicator logger = existingLogger.Value;
                    //LoggerInformation[] loggerInformation = logger.Loggers;

                    //int newLoggerIndex = loggerInformation.Length;
                    //Array.Resize(ref loggerInformation, loggerInformation.Length + 1);
                    //loggerInformation[newLoggerIndex] = new LoggerInformation(provider, existingLogger.Key);

                    //logger.Loggers = loggerInformation;
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

        //private LoggerInformation[] CreateLoggers(string categoryName)
        //{
        //    var loggers = new LoggerInformation[_providerRegistrations.Count];
        //    for (int i = 0; i < _providerRegistrations.Count; i++)
        //    {
        //        loggers[i] = new LoggerInformation(_providerRegistrations[i].Provider, categoryName);
        //    }
        //    return loggers;
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
                            //registration.Provider.Dispose();
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

            public IIndicator CreateLogger(string categoryName)
            {
                return _indicatorFactory.CreateLogger(categoryName);
            }

            public void AddProvider(IIndicatorProvider provider)
            {
                _indicatorFactory.AddProvider(provider);
            }
        }
    }
}
