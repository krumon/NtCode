using Nt.Core.DependencyInjection;
using Nt.Core.FileProviders;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Core.Options;
using System;

namespace Nt.Scripts.Hosting
{
    /// <summary>
    /// Default services host builder.
    /// </summary>
    public class NinjaHostBuilder : HostBuilder
    {
        protected override void AddNinjascriptServices<T>(IServiceProvider provider,PhysicalFileProvider fileProvider, IServiceCollection services, T ninjaScript, object[] ninjatraderObjects)
        {
            services.AddSingleton((Func<IServiceProvider, IHost>)(_ =>
            {
                return new Internal.NinjaHost(
                    provider
                    , provider.GetRequiredService<IHostEnvironment>()
                    , fileProvider
                    , provider.GetRequiredService<IHostApplicationLifetime>()
                    , provider.GetRequiredService<ILogger<Internal.NinjaHost>>()
                    //, _services.GetRequiredService<IHostLifetime>()
                    , provider.GetRequiredService<IOptions<HostOptions>>()
                    );
            }));

        }
    }
}
