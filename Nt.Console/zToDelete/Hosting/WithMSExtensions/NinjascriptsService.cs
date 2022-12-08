using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApp
{
    public static class NinjascriptsService
    {

        #region Public properties

        /// <summary>
        /// The ninjascript service builder.
        /// </summary>
        public static NinjascriptServiceBuilder NinjascriptBuilder { get; private set; }

        /// <summary>
        /// The dependency injection service provider.
        /// </summary>
        public static IServiceProvider Provider => NinjascriptBuilder?.Provider;

        #endregion

        #region Builder instances

        /// <summary>
        /// Creates a <see cref="NinjascriptServiceBuilder"/> with a specific builder type.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <returns>The ninjascript service builder.</returns>
        public static NinjascriptServiceBuilder CreateDefaultBuilder<T>()
            where T : NinjascriptServiceBuilder, new()
        {
            NinjascriptBuilder = new T();

            return NinjascriptBuilder;
        }

        /// <summary>
        /// Creates a <see cref="NinjascriptServiceBuilder"/> with a specific builder instance.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builderInstance">The type instance.</param>
        /// <returns>The ninjascript service builder.</returns>
        public static NinjascriptServiceBuilder CreateDefaultBuilder<T>(T builderInstance)
            where T : NinjascriptServiceBuilder
        {
            NinjascriptBuilder = builderInstance;

            return NinjascriptBuilder;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Build the ninjascript service with an specific provider.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="logStarted"></param>
        public static void Build(IServiceProvider provider, bool logStarted = false)
        {
            // Build the service provider
            NinjascriptBuilder.Build(provider);

            // If logStarted...inject the logger and log a message.
        }

        /// <summary>
        /// Gets the service stored in the service collection.
        /// </summary>
        /// <typeparam name="T">The type of the service to returns.</typeparam>
        /// <returns>The service of the type T.</returns>
        public static T Service<T>()
        {
            // Gets the service
            return Provider.GetService<T>();
        }

        #endregion

        #region Extension methods

        /// <summary>
        /// Should be called once a <see cref="NinjascriptServiceBuilder"/> is finished and we want to build it 
        /// and start our ninjascript.
        /// </summary>
        /// <param name="builder">The construction</param>
        /// <param name="logStarted">Specifies if the Ninjascript Started message should be logged</param>
        public static void Build(this NinjascriptServiceBuilder builder, bool logStarted = false)
        {
            builder.Build();

            // If logStarted...inject the logger and log a message.
        }

        #endregion

    }
}
