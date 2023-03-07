using Nt.Core.DependencyInjection;

namespace Nt.Core.Ninjascript
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
