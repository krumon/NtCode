using NinjaTrader.Cbi;
using Nt.Core.Providers;

namespace Nt.Scripts.Providers
{
    public class InstrumentProvider : BaseInstrumentProvider
    {
        public InstrumentProvider(Instrument instrument) : base(instrument?.MasterInstrument.Name)
        {
            TradingHoursName = instrument.MasterInstrument.TradingHours.Name;
            
        }
    }
}
