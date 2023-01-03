using System;

namespace Nt.Core.DependencyInjection.Internal
{
    internal abstract class ServiceProviderEngine
    {
        public abstract Func<ServiceProviderEngineScope, object> RealizeService(ServiceCallSite callSite);
    }
}
