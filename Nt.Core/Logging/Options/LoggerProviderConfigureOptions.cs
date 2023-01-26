﻿using Nt.Core.Attributes;
using Nt.Core.DependencyInjection;
using Nt.Core.Logging.Configuration;
using Nt.Core.Options;

namespace Nt.Core.Logging.Options
{
    /// <summary>
    /// Loads settings for <typeparamref name="TProvider"/> into <typeparamref name="TOptions"/> type.
    /// </summary>
    internal sealed class LoggerProviderConfigureOptions<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions, TProvider> : ConfigureFromConfigurationOptions<TOptions> 
        where TOptions : class
    {
        [RequiresUnreferencedCode(LoggerProviderOptions.TrimmingRequiresUnreferencedCodeMessage)]
        public LoggerProviderConfigureOptions(ILoggerProviderConfiguration<TProvider> providerConfiguration)
            : base(providerConfiguration.Configuration)
        {
        }
    }
}
