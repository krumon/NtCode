using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using System.Collections.Generic;
using System;
using IServiceProvider = Nt.Core.DependencyInjection.IServiceProvider;
using Nt.Scripts.Ninjascripts;

namespace Nt.Scripts.Hosting
{
    public class KrumonHost : Host
    {
        public KrumonHost(IServiceProvider services, HostOptions options) : base(services, options)
        {
        }

        public void ExecuteServices<T>()
        {
            var services = (IList<T>)Services.GetServices<T>();

            if (services == null || services.Count < 1)
                return;

            foreach(var service in services)
                ExecuteService(service);
        }

        private void ExecuteService<T>(T service)
        {
            if (service is IConfigureScript configureScript)
                configureScript.Configure();
                
            else if (service is IDataLoadedScript dataLoaded)
                dataLoaded.DataLoaded();
                
        }


    }
}
