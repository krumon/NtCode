using System;

namespace Nt.Core.Data
{
    /// <summary>
    /// Represents any financial instrument.
    /// </summary>
    public class InstrumentService
    {

        #region Private members

        private readonly bool _instanceError;
        private readonly InstrumentKey _instrumentKey;

        #endregion

        #region Public properties

        /// <summary>
        /// The instrument unique code.
        /// </summary>
        public InstrumentKey Key => _instrumentKey;

        /// <summary>
        /// The market exchange owner of the instrument.
        /// </summary>
        public MarketExchange MarketExchange => _instrumentKey.ToMarketExchange();

        /// <summary>
        /// The trading hours key.
        /// </summary>
        public TradingHoursKey TradingHoursKey {get;set;} = TradingHoursKey.Default;

        /// <summary>
        /// Gets the instument name.
        /// </summary>
        public string Name => _instanceError ? string.Empty : _instrumentKey.ToString();

        /// <summary>
        /// Gets the instrument description.
        /// </summary>
        public string Description => _instanceError ? string.Empty : _instrumentKey.ToDescription();

        /// <summary>
        /// Gets the default trading hours name.
        /// </summary>
        public string TradingHoursName 
        {
            get
            {
                if (TradingHoursKey == TradingHoursKey.Default)
                    TradingHoursKey = _instrumentKey.ToDefaultTradingHoursKey();

                return TradingHoursKey.ToName();
            }
            set
            {
                TradingHoursKey = _instrumentKey.ToTradingHoursKey(value);
            }
        }

        /// <summary>
        /// Gets or sets the instrument point value.
        /// </summary>
        public double PointValue => _instrumentKey.ToPointValue();

        /// <summary>
        /// Gets or sets the instrument tick size.
        /// </summary>
        public double TickSize => _instrumentKey.ToTickSize();

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="InstrumentProvider"/> default instance.
        /// </summary>
        public InstrumentService(string stringKey)
        {
            if (string.IsNullOrEmpty(stringKey))
                throw new ArgumentException($"the parameter {nameof(stringKey)} cannot be null or empty");

            if (!stringKey.TryGetInstrumentKey(out _instrumentKey))
            {
                _instanceError = true;
                throw new Exception("Unknown string key passed bay parameter.");
            }
        }

        #endregion


    }
}
