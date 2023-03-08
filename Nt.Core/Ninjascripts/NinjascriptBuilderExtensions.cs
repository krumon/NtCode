using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Ninjascripts.Configuration;
using System;

namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// Extension methods for setting up ninjascript services in an <see cref="INinjascriptBuilder" />.
    /// </summary>
    public static class NinjascriptBuilderExtensions
    {

        ///// <summary>
        ///// Sets a minimum <see cref="LogLevel"/> requirement for log messages to be logged.
        ///// </summary>
        ///// <param name="builder">The <see cref="ILoggingBuilder"/> to set the minimum level on.</param>
        ///// <param name="level">The <see cref="LogLevel"/> to set as the minimum.</param>
        ///// <returns>The <see cref="ILoggingBuilder"/> so that additional calls can be chained.</returns>
        //public static ILoggingBuilder SetMinimumLevel(this ILoggingBuilder builder, LogLevel level)
        //{
        //    builder.Services.Add(ServiceDescriptor.Singleton<IConfigureOptions<LoggerFilterOptions>>(
        //        new DefaultLoggerLevelConfigureOptions(level)));
        //    return builder;
        //}

        /// <summary>
        /// Adds the given <see cref="INinjascriptProvider"/> to the <see cref="INinjascriptBuilder"/>
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to add the <paramref name="provider"/> to.</param>
        /// <param name="provider">The <see cref="INinjascriptProvider"/> to add to the <paramref name="builder"/>.</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder AddProvider(this INinjascriptBuilder builder, INinjascriptProvider provider)
        {
            builder.Services.AddSingleton(provider);
            return builder;
        }

        /// <summary>
        /// Removes all <see cref="INinjascriptProvider"/>s from <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to remove <see cref="INinjascriptProvider"/>s from.</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder ClearProviders(this INinjascriptBuilder builder)
        {
            builder.Services.RemoveAll<INinjascriptProvider>();
            return builder;
        }

        /// <summary>
        /// Configure the <paramref name="builder"/> with the <see cref="NinjascriptFactoryOptions"/>.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to be configured with <see cref="NinjascriptFactoryOptions"/></param>
        /// <param name="action">The action used to configure the ninjascript factory</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder Configure(this INinjascriptBuilder builder, Action<NinjascriptFactoryOptions> action)
        {
            builder.Services.Configure(action);
            return builder;
        }

        /// <summary>
        /// Configures <see cref="NinjascriptFilterOptions" /> from an instance of <see cref="IConfiguration" />.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to use.</param>
        /// <param name="configuration">The <see cref="IConfiguration" /> to add.</param>
        /// <returns>The builder.</returns>
        public static INinjascriptBuilder AddConfiguration(this INinjascriptBuilder builder, IConfiguration configuration)
        {
            builder.AddConfiguration();

            //builder.Services.AddSingleton<IConfigureOptions<LoggerFilterOptions>>(new LoggerFilterConfigureOptions(configuration));
            //builder.Services.AddSingleton<IOptionsChangeTokenSource<LoggerFilterOptions>>(new ConfigurationChangeTokenSource<LoggerFilterOptions>(configuration));

            builder.Services.AddSingleton(new NinjascriptConfiguration(configuration));

            return builder;
        }
    }
}
