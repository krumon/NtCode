using Nt.Core.Configuration;
using Nt.Core.Services;
using Nt.Core.Services.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nt.Core.Hosting
{
    public class NinjascriptHostBuilder : INinjascriptHostBuilder
    {

        #region Private members

        //private List<Action<INinjascriptsConfigurationBuilder>> _configureHostConfigActions = new List<Action<INinjascriptsConfigurationBuilder>>();
        //private List<Action<NinjascriptHostBuilderContext, INinjascriptsConfigurationBuilder>> _configureAppConfigActions = new List<Action<NinjascriptHostBuilderContext, INinjascriptsConfigurationBuilder>>();
        //private List<IConfigureContainerAdapter> _configureContainerActions = new List<IConfigureContainerAdapter>();
        private IServiceFactoryAdapter _serviceProviderFactory = new ServiceFactoryAdapter<INinjascriptServiceCollection>(new DefaultServiceProviderFactory());
        private List<Action<NinjascriptHostBuilderContext, INinjascriptServiceCollection>> _configureServicesActions = new List<Action<NinjascriptHostBuilderContext, INinjascriptServiceCollection>>();
        private bool _hostBuilt;
        private INinjascriptConfiguration _hostConfiguration;
        private INinjascriptConfiguration _ninjascriptConfiguration;
        private NinjascriptHostBuilderContext _hostBuilderContext;
        private NinjascriptHostEnvironment _hostEnvironment;
        private INinjascriptServiceProvider _ninjascriptServices;
        //private PhysicalFileProvider _defaultProvider;

        #endregion

        #region Public properties

        /// <inheritdoc/>
        public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();

        #endregion

        #region Public methods

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        public INinjascriptHostBuilder ConfigureServices(Action<NinjascriptHostBuilderContext, INinjascriptServiceCollection> configureDelegate)
        {
            _configureServicesActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        /// <summary>
        /// Overrides the factory used to create the service provider.
        /// </summary>
        /// <typeparam name="TContainerBuilder">The type of the builder to create.</typeparam>
        /// <param name="factory">A factory used for creating service providers.</param>
        /// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        public INinjascriptHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            _serviceProviderFactory = new ServiceFactoryAdapter<TContainerBuilder>(factory ?? throw new ArgumentNullException(nameof(factory)));
            return this;
        }

        /// <summary>
        /// Overrides the factory used to create the service provider.
        /// </summary>
        /// <param name="factory">A factory used for creating service providers.</param>
        /// <typeparam name="TContainerBuilder">The type of the builder to create.</typeparam>
        /// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        public INinjascriptHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<NinjascriptHostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            _serviceProviderFactory = new ServiceFactoryAdapter<TContainerBuilder>(() => _hostBuilderContext, factory ?? throw new ArgumentNullException(nameof(factory)));
            return this;
        }

        /// <summary>
        /// Set up the configuration for the builder itself. This will be used to initialize the <see cref="IHostEnvironment"/>
        /// for use later in the build process. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public INinjascriptHostBuilder ConfigureHostConfiguration(Action<INinjascriptConfigurationBuilder> configureDelegate)
        {
            //_configureHostConfigActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        /// <summary>
        /// Sets up the configuration for the remainder of the build process and application. This can be called multiple times and
        /// the results will be additive. The results will be available at <see cref="HostBuilderContext.Configuration"/> for
        /// subsequent operations, as well as in <see cref="IHost.Services"/>.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public INinjascriptHostBuilder ConfigureAppConfiguration(Action<NinjascriptHostBuilderContext, INinjascriptConfigurationBuilder> configureDelegate)
        {
            //_configureAppConfigActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        /// <summary>
        /// Enables configuring the instantiated dependency container. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <typeparam name="TContainerBuilder">The type of the builder to create.</typeparam>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public INinjascriptHostBuilder ConfigureContainer<TContainerBuilder>(Action<NinjascriptHostBuilderContext, TContainerBuilder> configureDelegate)
        {
            //_configureContainerActions.Add(new ConfigureContainerAdapter<TContainerBuilder>(configureDelegate
            //    ?? throw new ArgumentNullException(nameof(configureDelegate))));
            return this;
        }

        /// <inheritdoc/>
        public INinjascriptHost Build()
        {
            if (_hostBuilt)
            {
                throw new InvalidOperationException("The Ninjascript host can only be built once.");
            }
            _hostBuilt = true;

            // REVIEW: If we want to raise more events outside of these calls then we will need to
            // stash this in a field.
            using (var diagnosticListener = new DiagnosticListener("Nt.Code.Hosting"))
            {
                const string hostBuildingEventName = "HostBuilding";
                const string hostBuiltEventName = "HostBuilt";

                if (diagnosticListener.IsEnabled() && diagnosticListener.IsEnabled(hostBuildingEventName))
                {
                    Write(diagnosticListener, hostBuildingEventName, this);
                }

                BuildHostConfiguration();
                CreateHostEnvironment();
                CreateHostBuilderContext();
                BuildNinjascriptConfiguration();
                CreateNinjascriptServiceProvider();

                var host = _ninjascriptServices.GetRequiredService<INinjascriptHost>();
                if (diagnosticListener.IsEnabled() && diagnosticListener.IsEnabled(hostBuiltEventName))
                {
                    Write(diagnosticListener, hostBuiltEventName, host);
                }
                return host;
            }
        }

        #endregion

        #region Private methods

        private void BuildHostConfiguration()
        {
//            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
//                .AddInMemoryCollection(); // Make sure there's some default storage since there are no default providers

//            foreach (Action<IConfigurationBuilder> buildAction in _configureHostConfigActions)
//            {
//                buildAction(configBuilder);
//            }
//            _hostConfiguration = configBuilder.Build();
        }

        private void CreateHostEnvironment()
        {
            _hostEnvironment = new NinjascriptHostEnvironment();
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

//        private string ResolveContentRootPath(string contentRootPath, string basePath)
//        {
//            if (string.IsNullOrEmpty(contentRootPath))
//            {
//                return basePath;
//            }
//            if (Path.IsPathRooted(contentRootPath))
//            {
//                return contentRootPath;
//            }
//            return Path.Combine(Path.GetFullPath(basePath), contentRootPath);
//        }

        private void CreateHostBuilderContext()
        {
            _hostBuilderContext = new NinjascriptHostBuilderContext(Properties)
            {
                HostingEnvironment = _hostEnvironment,
                Configuration = _hostConfiguration
            };
        }

        private void BuildNinjascriptConfiguration()
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

        private void CreateNinjascriptServiceProvider()
        {

            var services = new NinjascriptServiceCollection();
            services.AddSingleton<INinjascriptHostEnvironment>(_hostEnvironment);
            services.AddSingleton(_hostBuilderContext);
            // register configuration as factory to make it dispose with the service provider
            services.AddSingleton(_ => _ninjascriptConfiguration);
            services.AddSingleton<INinjascriptLifetime, NinjascriptLifetime>();
            AddLifetime(services);
            services.AddSingleton<INinjascriptHost>(_ =>
            {
                return new Internal.NinjascriptHost(
                    _ninjascriptServices,
                    _hostEnvironment,
                    //_fileProvider,
                    _ninjascriptServices.GetRequiredService<INinjascriptLifetime>(),
                    //_ninjascriptServices.GetRequiredService<ILogger<Internal.Host>>(),
                    _ninjascriptServices.GetRequiredService<IHostLifetime>()
                    //_ninjascriptServices.GetRequiredService<IOptions<HostOptions>>()
                    );
            });

            //services.AddOptions().Configure<HostOptions>(options => { options.Initialize(_hostConfiguration); });
            //services.AddLogging();

            foreach (Action<NinjascriptHostBuilderContext, INinjascriptServiceCollection> configureServicesAction in _configureServicesActions)
            {
                configureServicesAction(_hostBuilderContext, services);
            }

            object containerBuilder = _serviceProviderFactory.CreateBuilder(services);

            //foreach (IConfigureContainerAdapter containerAction in _configureContainerActions)
            //{
            //    containerAction.ConfigureContainer(_hostBuilderContext, containerBuilder);
            //}

            _ninjascriptServices = _serviceProviderFactory.CreateServiceProvider(containerBuilder);

            //if (_ninjascriptServices == null)
            //{
            //    throw new InvalidOperationException(SR.NullIServiceProvider);
            //}

            //// resolve configuration explicitly once to mark it as resolved within the
            //// service provider, ensuring it will be properly disposed with the provider
            //_ = _ninjascriptServices.GetService<IConfiguration>();
        }

        //[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:UnrecognizedReflectionPattern",Justification = "The values being passed into Write are being consumed by the application already.")]
        private void Write(
            DiagnosticSource diagnosticSource, 
            string name, 
            object value)
        {
            diagnosticSource.Write(name, value);
        }

        private static void AddLifetime(NinjascriptServiceCollection services)
        {
            // Add the console lifetime service to listens for Ctrl+C or SIGTERM and initiates shutdown.
            //services.AddSingleton<IHostLifetime, ConsoleLifetime>();
        }

        #endregion
    }
}
