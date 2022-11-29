
namespace Nt.Core.Services
{
    internal interface IServiceFactoryAdapter
    {

        object CreateBuilder(INinjascriptServiceCollection services);

        INinjascriptServiceProvider CreateServiceProvider(object containerBuilder);
    }
}
