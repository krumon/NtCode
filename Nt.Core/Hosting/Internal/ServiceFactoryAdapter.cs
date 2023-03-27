﻿using Nt.Core.DependencyInjection;
using System;

namespace Nt.Core.Hosting.Internal
{
    public sealed class ServiceFactoryAdapter<TContainerBuilder> : IServiceFactoryAdapter
    {
        private IServiceProviderFactory<TContainerBuilder> _serviceProviderFactory;
        private readonly Func<HostBuilderContext> _contextResolver;
        private Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> _factoryResolver;

        public ServiceFactoryAdapter(IServiceProviderFactory<TContainerBuilder> serviceProviderFactory)
        {
            _serviceProviderFactory = serviceProviderFactory ?? throw new ArgumentNullException(nameof(serviceProviderFactory));
        }

        public ServiceFactoryAdapter(Func<HostBuilderContext> contextResolver, Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factoryResolver)
        {
            _contextResolver = contextResolver ?? throw new ArgumentNullException(nameof(contextResolver));
            _factoryResolver = factoryResolver ?? throw new ArgumentNullException(nameof(factoryResolver));
        }

        public object CreateBuilder(IServiceCollection services)
        {
            if (_serviceProviderFactory == null)
            {
                _serviceProviderFactory = _factoryResolver(_contextResolver());

                if (_serviceProviderFactory == null)
                {
                    throw new InvalidOperationException("ResolverReturnedNull");
                }
            }
            return _serviceProviderFactory.CreateBuilder(services);
        }

        public IServiceProvider CreateServiceProvider(object containerBuilder)
        {
            if (_serviceProviderFactory == null)
            {
                throw new InvalidOperationException("CreateBuilderCallBeforeCreateServiceProvider");
            }

            return _serviceProviderFactory.CreateServiceProvider((TContainerBuilder)containerBuilder);
        }
    }
}
