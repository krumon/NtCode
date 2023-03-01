using Nt.Core.Data;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents a ninjascript instrument.
    /// </summary>
    public class DataSeriesDescriptor
    {

        #region Private members

        private int _periodValue = 1;

        #endregion

        #region Public properties

        /// <inheritdoc/>
        public string Key => $"{InstrumentKey}-{(int)PeriodType}-{PeriodValue}";

        /// <inheritdoc/>
        public InstrumentCode InstrumentKey { get; set; }

        /// <summary>
        /// Gets or sets the instrument name.
        /// </summary>
        public string InstrumentName { get; set; }

        /// <inheritdoc/>
        public PeriodType PeriodType { get; set; }

        /// <inheritdoc/>
        public int PeriodValue
        {
            get => _periodValue;
            set
            {
                if (_periodValue != value)
                {
                    if (value < 1)
                        return;

                    _periodValue = value;
                }
            }
        }

        /// <inheritdoc/>
        public TradingHoursCode TradingHoursKey { get; set; }

        /// <summary>
        /// Gets or sets the trading hours template name.
        /// </summary>
        public string TradingHoursName { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="DataSeriesDescriptor"/> default instance.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="periodType"></param>
        /// <param name="periodValue"></param>
        /// <param name="tradingHoursKey"></param>
        public DataSeriesDescriptor(InstrumentCode key, PeriodType periodType, int periodValue, TradingHoursCode tradingHoursKey)
        {
            InstrumentKey = key;
            PeriodType = periodType;
            PeriodValue = periodValue;
            TradingHoursKey = tradingHoursKey;
        }

        /// <summary>
        /// Create <see cref="DataSeriesDescriptor"/> default instance.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="periodType"></param>
        /// <param name="periodValue"></param>
        /// <param name="tradingHoursKey"></param>
        public DataSeriesDescriptor(string instrumentName, PeriodType periodType, int periodValue, string tradingHoursName)
        {
            InstrumentName = instrumentName;
            PeriodType = periodType;
            PeriodValue = periodValue;
            TradingHoursName = tradingHoursName;
        }

        #endregion
    }
}
