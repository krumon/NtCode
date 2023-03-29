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
        }

        protected override void Configure()
        {
            //_ninjascript = Hosting.CreateNinjascript<KrTrade_Stats>(null).Build();
            _host = NinjaHost.CreateDesignNinjaHostDefaultBuilder<KrTrade_Stats>(null).Build<KrTrade_Stats>();
        }
    }
}
