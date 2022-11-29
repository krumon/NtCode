using NinjaTrader.Cbi;
using Nt.Core.Data;
using Instrument = NinjaTrader.Cbi.Instrument;

namespace Nt.Scripts.Providers
{
    /// <summary>
    /// Ninjascripts instrumens provider.
    /// </summary>
    public class InstrumentProvider : Core.Data.InstrumentProvider
    {

        #region Constructors

        /// <summary>
        /// Create <see cref="InstrumentProvider"/> default instance.
        /// </summary>
        /// <param name="instrument">The ninjascript instrument.</param>
        public InstrumentProvider(Instrument instrument) : base(instrument?.MasterInstrument.Name)
        {
            TradingHoursName = instrument.MasterInstrument.TradingHours.Name;
            
        }

        #endregion
    }
}
