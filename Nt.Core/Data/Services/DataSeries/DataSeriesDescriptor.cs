namespace Nt.Core.Data
{
    /// <summary>
    /// Represents a ninjascript instrument.
    /// </summary>
    public class DataSeriesDescriptor : IDataSeriesDescriptor
    {

        #region Private members

        private int _periodValue = 1;

        #endregion

        #region Public properties

        /// <inheritdoc/>
        public string Key => $"{InstrumentKey}-{(int)PeriodType}-{PeriodValue}";

        /// <inheritdoc/>
        public InstrumentKey InstrumentKey { get; set; }

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
        public TradingHoursKey TradingHoursKey { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="DataSeriesDescriptor"/> default instance.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="periodType"></param>
        /// <param name="periodValue"></param>
        /// <param name="tradingHoursKey"></param>
        public DataSeriesDescriptor(InstrumentKey key, PeriodType periodType, int periodValue, TradingHoursKey tradingHoursKey)
        {
            InstrumentKey = key;
            PeriodType = periodType;
            PeriodValue = periodValue;
            TradingHoursKey = tradingHoursKey;
        }

        #endregion
    }
}
