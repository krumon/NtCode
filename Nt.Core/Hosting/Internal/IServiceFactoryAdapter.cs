using Nt.Core.DependencyInjection;

namespace Nt.Core.Hosting.Internal
{
    internal interface IServiceFactoryAdapter
    {
        object CreateBuilder(IServiceCollection services);

        IServiceProvider CreateServiceProvider(object containerBuilder);
    }
}
