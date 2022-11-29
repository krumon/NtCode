using NinjaTrader.NinjaScript;
using Nt.Core.Services;

namespace Nt.Scripts.Ninjascripts.Data
{
    public class Data : DataService, IData
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
