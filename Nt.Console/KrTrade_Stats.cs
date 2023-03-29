using NinjaTrader.NinjaScript;
using Nt.Core.Hosting;
using Nt.Scripts.Hosting;
using Nt.Scripts.Ninjatrader;

namespace ConsoleApp
{
    internal class KrTrade_Stats : BaseNinjaScript
    {
        private INinjaHost _host;

        public KrTrade_Stats(NinjaScriptBase ninjascript) : base(ninjascript)
        {
            _host = NinjaHost.CreateDesignNinjaHostDefaultBuilder<KrTrade_Stats>(null).Build<KrTrade_Stats>();
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
