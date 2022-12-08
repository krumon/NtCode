
namespace ConsoleApp
{
    internal interface IServiceFactoryAdapter
    {

        object CreateBuilder(INinjascriptServiceCollection services);

        INinjascriptServiceProvider CreateServiceProvider(object containerBuilder);
    }
}
