using Nt.Core.DependencyInjection;

namespace Nt.Scripts.Indicators.Internal
{
    internal sealed class IndicatorBuilder : IIndicatorBuilder
    {
        public IndicatorBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
