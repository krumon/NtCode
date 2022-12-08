using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Internal
{
    public class CallSiteTest
    {
        private readonly CallSiteFactory _callSiteFactory;
        private INinjascriptServiceCollection _services;

        public INinjascriptServiceCollection Services => _services;
        public NinjascriptServiceDescriptor[] Descriptors => _callSiteFactory.Descriptors;

        public CallSiteTest()
        {
            _services = CreateServiceCollection();
            _callSiteFactory = new CallSiteFactory(_services);
            var cs = _callSiteFactory.GetCallSite(typeof(SessionStats), new CallSiteChain());

        }

        private INinjascriptServiceCollection CreateServiceCollection()
        {
            return  new NinjascriptServiceCollection()
            .AddSingleton(typeof(SessionFilters), new SessionFilters())
            .AddSingleton<ISession, SessionStats>()
            .AddSingleton(typeof(SessionHoursList))
            .AddSingleton<ISession, SessionHours>();
        }

    }
}
