using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;

namespace Nt.Scripts.Hosting
{
    public class KrumonHost : Host
    {
        public KrumonHost(IServiceProvider services, HostOptions options) : base(services, options)
        {
        }


    }
}
