using System;

namespace Nt.Core.Services.Internal
{
    internal abstract class ServiceProviderEngine
    {
        public abstract Func<ServiceProviderEngineScope, object> RealizeService(ServiceCallSite callSite);
    }
}