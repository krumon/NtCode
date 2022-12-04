using Nt.Core.DependencyInjection;
using Nt.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using IServiceProvider = Nt.Core.DependencyInjection.IServiceProvider;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Default services host builder.
    /// </summary>
    public class HostBuilder : IHostBuilder
    {
        //private bool _requiredServicesIsAdded = true;
        //private ServiceProviderFactory _serviceProviderFactory = new ServiceProviderFactory();
        //private ConcurrentDictionary<RequiredServiceType, IRequiredService> _requiredServices = new ConcurrentDictionary<RequiredServiceType, IRequiredService>();
        //private ConcurrentDictionary<OptionalServiceType, IOptionalService> _optionalServices = new ConcurrentDictionary<OptionalServiceType, IOptionalService>();
        
        private bool _built;
        private List<Action<HostOptions>> _configureHostOptionsActions;
        private List<Action<IServiceCollection>> _configureServicesActions;
        private HostOptions _hostOptions = HostOptions.Default;
        private IServiceProvider _services;

        private List<Action<DataSeriesBuilder>> _configureDataSeriesActions;

        /// <summary>
        /// Set up the options for the builder itself. This will be used to initialize the <see cref="Hosting"/>
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
            if (_configureServicesActions == null)
                _configureServicesActions = new List<Action<IServiceCollection>>();
            _configureServicesActions.Add(configureServicesDelegate ?? throw new ArgumentNullException(nameof(configureServicesDelegate)));
            return this;
        }

        public IHostBuilder ConfigureDefaults(string[] args)
        {
            return this;
        }

        public IHostBuilder UseDataSeries(Action<DataSeriesBuilder> dataSeriesDelegate)
        {
            if (_configureDataSeriesActions == null)
                _configureDataSeriesActions = new List<Action<DataSeriesBuilder>>();
            _configureDataSeriesActions.Add(dataSeriesDelegate ?? throw new ArgumentNullException(nameof(dataSeriesDelegate)));
            return this;
        }

        public IHost Build()
        {
            if (_built)
            {
                throw new InvalidOperationException("The host can only be built once.");
            }
            _built = true;

            // REVIEW: If we want to raise more events outside of these calls then we will need to
            // stash this in a field.
            using (var diagnosticListener = new DiagnosticListener("Nt.InstrumentKey.Hosting"))
            {
                const string hostBuildingEventName = "HostBuilding";
                const string hostBuiltEventName = "HostBuilt";

                if (diagnosticListener.IsEnabled() && diagnosticListener.IsEnabled(hostBuildingEventName))
                {
                    Write(diagnosticListener, hostBuildingEventName, this);
                }

                ConfigHostOptions();
                CreateHostEnvironment();
                CreateHostBuilderContext();
                BuildConfiguration();
                CreateServiceProvider();

                var host = _services.GetRequiredService<IHost>();
                if (diagnosticListener.IsEnabled() && diagnosticListener.IsEnabled(hostBuiltEventName))
                {
                    Write(diagnosticListener, hostBuiltEventName, host);
                }
                return host;
            }
        }

        private void ConfigHostOptions()
        {
            foreach (Action<HostOptions> action in _configureHostOptionsActions)
                action(_hostOptions);
        }
        private void CreateHostEnvironment()
        {
            //_hostEnvironment = new NinjascriptHostEnvironment();
            //            {
            //                ApplicationName = _hostConfiguration[HostDefaults.ApplicationKey],
            //                EnvironmentName = _hostConfiguration[HostDefaults.EnvironmentKey] ?? Environments.Production,
            //                ContentRootPath = ResolveContentRootPath(_hostConfiguration[HostDefaults.ContentRootKey], AppContext.BaseDirectory),
            //            };

            //            if (string.IsNullOrEmpty(_hostingEnvironment.ApplicationName))
            //            {
            //                // Note GetEntryAssembly returns null for the net4x console test runner.
            //                _hostingEnvironment.ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name;
            //            }

            //            _hostingEnvironment.ContentRootFileProvider = _defaultProvider = new PhysicalFileProvider(_hostingEnvironment.ContentRootPath);
        }
        private void CreateHostBuilderContext()
        {
            //_hostBuilderContext = new NinjascriptHostBuilderContext(Properties)
            //{
            //    HostingEnvironment = _hostEnvironment,
            //    Configuration = _hostConfiguration
            //};
        }
        private void BuildConfiguration()
        {
            //            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
            //                .SetBasePath(_hostingEnvironment.ContentRootPath)
            //                .AddConfiguration(_hostConfiguration, shouldDisposeConfiguration: true);

            //            foreach (Action<HostBuilderContext, IConfigurationBuilder> buildAction in _configureAppConfigActions)
            //            {
            //                buildAction(_hostBuilderContext, configBuilder);
            //            }
            //            _appConfiguration = configBuilder.Build();
            //            _hostBuilderContext.Configuration = _appConfiguration;
        }
        private void CreateServiceProvider()
        {
            var services = new ServiceCollection();
            //services.AddSingleton<INinjascriptHostEnvironment>(_hostEnvironment);
            //services.AddSingleton(_hostBuilderContext);
            // register configuration as factory to make it dispose with the service provider
            //services.AddSingleton(_ => _ninjascriptConfiguration);
            //services.AddSingleton<INinjascriptLifetime, NinjascriptLifetime>();
            //AddLifetime(services);
            services.Add<IHost>(_ =>
            {
                return new Host(
                    _services
                    , _hostOptions
                    //, _hostEnvironment
                    //, _fileProvider,
                    //, _ninjascriptServices.GetRequiredService<INinjascriptLifetime>(),
                    //, _ninjascriptServices.GetRequiredService<ILogger<Internal.Host>>(),
                    //, _ninjascriptServices.GetRequiredService<IHostLifetime>()
                    //, _ninjascriptServices.GetRequiredService<IOptions<HostOptions>>()
                    );
            });

            //services.AddOptions().Configure<HostOptions>(options => { options.Initialize(_hostConfiguration); });
            //services.AddLogging();

            // Add the configure services by the delegates.
            foreach (Action<IServiceCollection> configureServicesAction in _configureServicesActions)
            {
                configureServicesAction(services);
            }
            
            _services = new ServiceProvider(services, new ServiceProviderOptions
            {

            });

            if (_services == null)
            {
                throw new InvalidOperationException("NullIServiceProvider");
            }

            //// resolve configuration explicitly once to mark it as resolved within the
            //// service provider, ensuring it will be properly disposed with the provider
            //_ = _ninjascriptServices.GetService<IConfiguration>();
        }
        private void Write(DiagnosticSource diagnosticSource,string name,object value)
        {
            diagnosticSource.Write(name, value);
        }
        private void AddHostOptions()
        {
            //            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
            //                .AddInMemoryCollection(); // Make sure there's some default storage since there are no default providers

            //            foreach (Action<IConfigurationBuilder> buildAction in _configureHostConfigActions)
            //            {
            //                buildAction(configBuilder);
            //            }
            //            _hostConfiguration = configBuilder.Build();
        }
        private string ResolveContentRootPath(string contentRootPath, string basePath)
        {
            if (string.IsNullOrEmpty(contentRootPath))
            {
                return basePath;
            }
            if (Path.IsPathRooted(contentRootPath))
            {
                return contentRootPath;
            }
            return Path.Combine(Path.GetFullPath(basePath), contentRootPath);
        }

    }
}
