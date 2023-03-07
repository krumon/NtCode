﻿using Nt.Core.Configuration;

namespace Nt.Core.Ninjascript.Configuration
{
    internal sealed class NinjascriptProviderConfiguration<T> : INinjascriptProviderConfiguration<T>
    {
        public NinjascriptProviderConfiguration(INinjascriptProviderConfigurationFactory providerConfigurationFactory)
        {
            Configuration = providerConfigurationFactory.GetConfiguration(typeof(T));
        }

        public IConfiguration Configuration { get; }
    }
}
