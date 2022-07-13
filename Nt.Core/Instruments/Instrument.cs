
namespace NtCore
{
    /// <summary>
    /// Represents any financial instrument.
    /// </summary>
    public class Instrument
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

        /// <summary>
        /// The instrument <see cref="SessionHours"/>.
        /// </summary>
        public SessionHours SessionHours { get; set; }

        #endregion

    }
}
