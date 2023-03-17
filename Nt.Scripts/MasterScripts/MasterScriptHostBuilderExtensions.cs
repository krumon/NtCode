﻿using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Scripts.Configuration;
using System;

namespace Nt.Scripts.MasterScripts
{
    public static class MasterScriptHostBuilderExtensions
    {

        /// <summary>
        /// Adds a delegate for configuring the provided <see cref="INinjascriptBuilder"/>. This may be called multiple times.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <param name="configureMasterScripts">The delegate that configures the <see cref="INinjascriptBuilder"/>.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder ConfigureMasterScripts(this IHostBuilder hostBuilder, Action<HostBuilderContext> configureMasterScripts)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.Configure<MasterScriptOptions>(MasterScriptOptions.MasterStats, context.Configuration.GetSection(MasterScriptSections.MasterStats));
                services.Configure<MasterScriptOptions>(MasterScriptOptions.MasterSwings, context.Configuration.GetSection(MasterScriptSections.MasterSwings));

                services.AddMasterScripts();
            });
        }

        /// <summary>
        /// Adds a delegate for configuring the provided <see cref="INinjascriptBuilder"/>. This may be called multiple times.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <param name="configureMasterScripts">The delegate that configures the <see cref="INinjascriptBuilder"/>.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder ConfigureMasterScripts(this IHostBuilder hostBuilder, string ninjascriptName, Action<HostBuilderContext> configureMasterScripts)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.Configure<MasterScriptFilters>(context.Configuration.GetSection("NinjaScript"));
                foreach (IConfigurationSection configurationSection in context.Configuration.GetSection("MasterScripts").GetChildren())
                {
                    string name = configurationSection.Key;
                    string key = SectionNames.MasteScripts + ":" + configurationSection.Key;

                    services.Configure<MasterScriptOptions>(name, context.Configuration.GetSection(key));
                }
                //services.Configure<MasterScriptOptions>(MasterScriptOptions.MasterStats, context.Configuration.GetSection(MasterScriptSections.MasterStats));
                //services.Configure<MasterScriptOptions>(MasterScriptOptions.MasterSwings, context.Configuration.GetSection(MasterScriptSections.MasterSwings));

                services.AddMasterScripts();
            });
        }
    }
}
