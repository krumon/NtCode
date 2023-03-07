using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Ninjascripts.Configuration;
using System;

namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// Extension methods for setting up ninjascript services in an <see cref="INinjascriptsBuilder" />.
    /// </summary>
    public static class NinjascriptsBuilderExtensions
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
        /// Adds the given <see cref="INinjascriptsProvider"/> to the <see cref="INinjascriptsBuilder"/>
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptsBuilder"/> to add the <paramref name="provider"/> to.</param>
        /// <param name="provider">The <see cref="INinjascriptsProvider"/> to add to the <paramref name="builder"/>.</param>
        /// <returns>The <see cref="INinjascriptsBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptsBuilder AddProvider(this INinjascriptsBuilder builder, INinjascriptsProvider provider)
        {
            builder.Services.AddSingleton(provider);
            return builder;
        }

        /// <summary>
        /// Removes all <see cref="INinjascriptsProvider"/>s from <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptsBuilder"/> to remove <see cref="INinjascriptsProvider"/>s from.</param>
        /// <returns>The <see cref="INinjascriptsBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptsBuilder ClearProviders(this INinjascriptsBuilder builder)
        {
            builder.Services.RemoveAll<INinjascriptsProvider>();
            return builder;
        }

        /// <summary>
        /// Configure the <paramref name="builder"/> with the <see cref="NinjascriptsFactoryOptions"/>.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptsBuilder"/> to be configured with <see cref="NinjascriptsFactoryOptions"/></param>
        /// <param name="action">The action used to configure the ninjascript factory</param>
        /// <returns>The <see cref="INinjascriptsBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptsBuilder Configure(this INinjascriptsBuilder builder, Action<NinjascriptsFactoryOptions> action)
        {
            builder.Services.Configure(action);
            return builder;
        }

        /// <summary>
        /// Configures <see cref="NinjascriptFilterOptions" /> from an instance of <see cref="IConfiguration" />.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptsBuilder"/> to use.</param>
        /// <param name="configuration">The <see cref="IConfiguration" /> to add.</param>
        /// <returns>The builder.</returns>
        public static INinjascriptsBuilder AddConfiguration(this INinjascriptsBuilder builder, IConfiguration configuration)
        {
            builder.AddConfiguration();

            //builder.Services.AddSingleton<IConfigureOptions<LoggerFilterOptions>>(new LoggerFilterConfigureOptions(configuration));
            //builder.Services.AddSingleton<IOptionsChangeTokenSource<LoggerFilterOptions>>(new ConfigurationChangeTokenSource<LoggerFilterOptions>(configuration));

            builder.Services.AddSingleton(new NinjascriptsConfiguration(configuration));

            return builder;
        }
    }
}
