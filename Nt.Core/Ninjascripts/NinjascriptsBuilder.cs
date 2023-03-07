using Nt.Core.DependencyInjection;

namespace Nt.Core.Ninjascripts
{
    internal class NinjascriptsBuilder : INinjascriptsBuilder
    {

        public NinjascriptsBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

    }
}
