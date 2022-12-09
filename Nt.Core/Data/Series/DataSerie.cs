﻿using Nt.Core.Services;
using System;

namespace Nt.Core.Data
{
    public class DataSerie
    {

        #region Private members

        private readonly bool _instanceError;
        private readonly InstrumentCode _instrumentKey;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the unique key of the service.
        /// </summary>
        //public OptionalServiceType Key { get; private set; }

        /// <summary>
        /// The instrument unique code.
        /// </summary>
        public InstrumentCode InstrumentKey => _instrumentKey;

        /// <summary>
        /// The market exchange owner of the instrument.
        /// </summary>
        public MarketExchange MarketExchange => _instrumentKey.ToMarketExchange();

        /// <summary>
        /// The trading hours key.
        /// </summary>
        public TradingHoursCode TradingHoursKey { get; set; } = TradingHoursCode.Default;

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
                if (TradingHoursKey == TradingHoursCode.Default)
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
        public DataSerie(string stringKey)
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
