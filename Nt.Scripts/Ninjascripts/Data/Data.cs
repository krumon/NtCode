using NinjaTrader.NinjaScript;
using Nt.Core.Services;

namespace Nt.Scripts.Ninjascripts.Data
{
    public class Data : ChartDataService, IData
    {
        public NinjaScriptBase Ninjascript { get; private set; }

        public Data()
        {
        }

        public void Configure(NinjaScriptBase ninjascript)
        {
            Ninjascript = ninjascript;
        }
    }
}
