using Nt.Core.DependencyInjection;

namespace Nt.Core.Ninjascripts
{
    internal class NinjascriptBuilder
    {

        public NinjascriptBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

    }
}
