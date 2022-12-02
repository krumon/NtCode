using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Scripts.Ninjascripts;
using System.Collections.Generic;

namespace Nt.Scripts.Hosting
{
    public static class HostExtensions
    {

        public static void ExecuteScripts<T>(this IHost host, params object[] ninjascriptObjects)
        {
            IList<object> services = (IList<object>)host.Services.GetServices<T>();

            if (services == null || services.Count < 1)
                return;

            foreach (var service in services)
            {
                if (service is IConfigureScript configureScript)
                    configureScript.Configure(ninjascriptObjects);

                else if (service is IDataLoadedScript dataLoaded)
                    dataLoaded.DataLoaded(ninjascriptObjects);
            }
        }
    }
}
