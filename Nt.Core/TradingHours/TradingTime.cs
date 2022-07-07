﻿using NinjaTrader.NinjaScript;
using System;

namespace NtCore
{
    public class TradingTime
    {

        #region Public properties

        /// <summary>
        /// Gets or sets the <see cref="TimeZoneInfo"/> of the trading hour.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get; set; }

        /// <summary>
        /// Gets or sets the time of the trading hour.
        /// </summary>
        public TimeSpan Time { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create instance of <see cref="TradingTime"/> class.
        /// </summary>
        /// <param name="type"></param>
        private TradingTime(GeneralTradingTime type)
        {
            this.TimeZoneInfo = type.ToTimeZoneInfo();
            this.Time = type.ToTimeSpan();
        }

        /// <summary>
        /// Create instance of <see cref="TradingTime"/> class.
        /// </summary>
        /// <param name="type"></param>
        private TradingTime(SpecificTradingTime type, InstrumentCode code)
        {
            this.TimeZoneInfo = type.ToTimeZoneInfo(code);
            this.Time = type.ToTimeSpan(code);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Create default instance of <see cref="TradingTime"/> class.
        /// </summary>
        /// <param name="type">The trading hour type for create the instance.</param>
        /// <returns>The trading hour instance.</returns>
        public static TradingTime CreateTradingHourByType(GeneralTradingTime type)
        {
            return new TradingTime(type);
        }

        /// <summary>
        /// Create default instance of <see cref="TradingTime"/> class.
        /// </summary>
        /// <param name="type">The trading hour type for create the instance.</param>
        /// <returns>The trading hour instance.</returns>
        public static TradingTime CreateTradingHourByType(SpecificTradingTime type, InstrumentCode code = InstrumentCode.Default)
        {
            return new TradingTime(type,code);
        }

        /// <summary>
        /// Gets the trading hour <see cref="DateTime"/>.
        /// </summary>
        /// <param name="currentDate">The current date to create the date time structure.</param>
        /// <param name="targetTimeZoneInfo">The time zone info to conver the date time structure.</param>
        /// <returns></returns>
        public DateTime GetTradingHour(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(currentDate.Date + Time,TimeZoneInfo,targetTimeZoneInfo);
        }

        #endregion

    }
}
