using Nt.Core.Configuration;

namespace Nt.Core.Ninjascripts.Configuration
{
    internal sealed class NinjascriptsConfiguration
    {
        public IConfiguration Configuration { get; }

        public NinjascriptsConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
