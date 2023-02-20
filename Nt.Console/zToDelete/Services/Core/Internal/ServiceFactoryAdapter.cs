//using System;

//namespace ConsoleApp.Internal
//{
//    internal sealed class ServiceFactoryAdapter<TContainerBuilder> : IServiceFactoryAdapter
//    {

//        private IServiceProviderFactory<TContainerBuilder> _serviceProviderFactory;
//        private readonly Func<NinjascriptHostBuilderContext> _contextResolver;
//        private Func<NinjascriptHostBuilderContext, IServiceProviderFactory<TContainerBuilder>> _factoryResolver;

//        public ServiceFactoryAdapter(IServiceProviderFactory<TContainerBuilder> serviceProviderFactory)
//        {
//            _serviceProviderFactory = serviceProviderFactory ?? throw new ArgumentNullException(nameof(serviceProviderFactory));
//        }

//        public ServiceFactoryAdapter(Func<NinjascriptHostBuilderContext> contextResolver, Func<NinjascriptHostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factoryResolver)
//        {
//            _contextResolver = contextResolver ?? throw new ArgumentNullException(nameof(contextResolver));
//            _factoryResolver = factoryResolver ?? throw new ArgumentNullException(nameof(factoryResolver));
//        }

//        public object CreateBuilder(INinjascriptServiceCollection services)
//        {
//            if (_serviceProviderFactory == null)
//            {
//                _serviceProviderFactory = _factoryResolver(_contextResolver());

//                if (_serviceProviderFactory == null)
//                {
//                    throw new InvalidOperationException("Resolver Returned Null");
//                }
//            }
//            return _serviceProviderFactory.CreateBuilder(services);
//        }

//        public INinjascriptServiceProvider CreateServiceProvider(object containerBuilder)
//        {
//            if (_serviceProviderFactory == null)
//            {
//                throw new InvalidOperationException("CreateInstance Builder Call Before CreateInstance Service Provider");
//            }

//            return _serviceProviderFactory.CreateServiceProvider((TContainerBuilder)containerBuilder);
//        }
//    }
//}
