namespace Nt.Core.Data
{
    /// <summary>
    /// Represents a ninjascript instrument.
    /// </summary>
    public class InstrumentServiceDescriptor : IServiceDescriptor
    {

        #region Private members

        private int _periodValue = 1;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the unique key of the ninjascript.
        /// </summary>
        public string Key => $"{InstrumentKey}-{(int)PeriodType}-{PeriodValue}"; 

        /// <summary>
        /// Gets or sets the instrument key.
        /// </summary>
        public InstrumentKey InstrumentKey { get; set; } 

        /// <summary>
        /// Gets or sets the series period type.
        /// </summary>
        public PeriodType PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the series period value.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the trading hours key.
        /// </summary>
        public TradingHoursKey TradingHoursKey { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="InstrumentServiceDescriptor"/> default instance.
        /// The <see cref="PeriodType"/> default value is Minute.
        /// The <see cref="PeriodValue"/> default value is one.
        /// </summary>
        public InstrumentServiceDescriptor(InstrumentKey key) : this(key,PeriodType.Minute,1, key.ToDefaultTradingHoursKey())
        {
        }

        public InstrumentServiceDescriptor(InstrumentKey key, PeriodType periodType) : this(key, periodType, 1, key.ToDefaultTradingHoursKey())
        {
        }

        public InstrumentServiceDescriptor(InstrumentKey key, PeriodType periodType, int periodValue) : this(key, periodType, periodValue, key.ToDefaultTradingHoursKey())
        {
        }

        public InstrumentServiceDescriptor(InstrumentKey key, PeriodType periodType, int periodValue, TradingHoursKey tradingHoursKey)
        {
            InstrumentKey = key;
            PeriodType = periodType;
            PeriodValue = periodValue;
            TradingHoursKey = tradingHoursKey;
        }

        #endregion
    }
}
