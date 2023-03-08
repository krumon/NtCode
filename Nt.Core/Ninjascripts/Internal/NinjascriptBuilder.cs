using Nt.Core.DependencyInjection;

namespace Nt.Core.Ninjascripts.Internal
{
    internal class NinjascriptBuilder : INinjascriptBuilder
    {

        public NinjascriptBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

    }
}
