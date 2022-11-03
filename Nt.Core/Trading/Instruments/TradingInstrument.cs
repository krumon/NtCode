
namespace Nt.Core.Trading
{
    /// <summary>
    /// Represents any financial instrument.
    /// </summary>
    public class TradingInstrument
    {
        #region Public properties

        /// <summary>
        /// The instrument unique code.
        /// </summary>
        public InstrumentCode InstrumentCode { get; set; }

        /// <summary>
        /// The market exchange owner of the instrument.
        /// </summary>
        public MarketExchange MarketExchange { get; set; }

        /// <summary>
        /// The instrument description.
        /// </summary>
        public string Description => InstrumentCode.ToDescription();


        #endregion
    }
}
