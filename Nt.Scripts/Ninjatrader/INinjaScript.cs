using NinjaTrader.NinjaScript;
using Nt.Core.Data;

namespace Nt.Scripts.Ninjatrader
{
    public interface INinjaScript
    {
        State State { get; }
        NinjaScriptEvent NinjaScriptEvent { get; }
        void OnStateChange();
        void OnNinjaScriptChange(NinjaScriptEvent ninjascriptEvent);
    }
}
