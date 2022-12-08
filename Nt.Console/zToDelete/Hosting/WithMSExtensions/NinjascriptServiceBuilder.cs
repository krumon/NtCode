using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConsoleApp;
using System;

namespace ConsoleApp
{
    public class NinjascriptServiceBuilder
    {

        #region Private members

        /// <summary>
        /// Represents the service collection.
        /// </summary>
        protected IServiceCollection services;

        #endregion

        #region Public properties

        /// <summary>
        /// The ninjascript service provider.
        /// </summary>
        public IServiceProvider Provider { get; protected set; }

        /// <summary>
        /// Gets or sets the service collection.
        /// </summary>
        public IServiceCollection Services
        {
            get => services;
            set
            {
                // Sets the new value.
                services = value;

                // Injects default services (Environment,...)
                //if (services != null)
                //    services.AddSingleton(Environment);
            }
        }

        /// <summary>
        /// The environment used for the ninjascript.
        /// </summary>
        public INinjascriptEnvironment Environment { get; protected set; }

        /// <summary>
        /// Gets or sets the ninjascript configuration.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Create <see cref="NinjascriptServiceBuilder"/> default instance.
        /// </summary>
        /// <param name="createServiceCollection">If true, a new <see cref="ServiceCollection"/> will be created for the Services</param>
        public NinjascriptServiceBuilder(bool createServiceCollection = true)
        {
            // If we should create the service collection
            if (createServiceCollection)
                // Create a new list of dependencies.
                Services = new ServiceCollection();
        }

        #endregion

        #region Build methods

        /// <summary>
        /// Sets or construct a service provider from a service collection.
        /// </summary>
        /// <param name="provider">The servide provider to set.</param>
        public void Build(IServiceProvider provider = null)
        {
            // Use give provider or build it.
            Provider =  provider ?? Services.BuildServiceProvider();
        }

        #endregion

        #region Configure methods

        /// <summary>
        /// Use the given configuration to the <see cref="NinjascriptsService"/>.
        /// </summary>
        /// <param name="configuration">The configuration to use in the <see cref="NinjascriptsService"/>.</param>
        /// <returns></returns>
        public NinjascriptServiceBuilder UseConfiguration (IConfiguration configuration)
        {
            Configuration = configuration;

            // Return self for chaining
            return this;
        }


        #endregion



    }
}
