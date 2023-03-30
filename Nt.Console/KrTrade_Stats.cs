using NinjaTrader.NinjaScript;
using Nt.Scripts.Hosting;
using Nt.Scripts.Ninjatrader;

namespace ConsoleApp
{
    internal class KrTrade_Stats : BaseNinjaScript
    {
        private INinjaHost _host;

        public KrTrade_Stats()
        {
            _host = (INinjaHost)NinjaHost.CreateDesignNinjaHostDefaultBuilder<KrTrade_Stats>().Build();
        }

        protected override void Configure()
        {
            _host.Configure();
        }
        protected override void DataLoaded()
        {
            _host.DataLoaded();
        }
        protected override void OnBarUpdate()
        {
            _host.OnBarUpdate();
        }
    }
}
