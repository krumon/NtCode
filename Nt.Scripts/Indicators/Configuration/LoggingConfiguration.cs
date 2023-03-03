using Nt.Core.Configuration;

namespace Nt.Scripts.Indicators.Configuration
{
    internal sealed class IndicatorConfiguration
    {
        public IConfiguration Configuration { get; }

        public IndicatorConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
