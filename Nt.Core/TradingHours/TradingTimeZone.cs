using NinjaTrader.NinjaScript;
using System;

namespace NtCore
{
    /// <summary>
    /// Contents properties and methods of specific trading time zone.
    /// </summary>
    public class TradingTimeZone
    {

        #region Public properties

        /// <summary>
        /// The initial trading hour.
        /// </summary>
        public TradingHour InitialTadingHour { get; set; }

        /// <summary>
        /// The final trading hour.
        /// </summary>
        public TradingHour FinalTadingHour { get; set; }

        #endregion

        #region Constructors

        private TradingTimeZone(TradingHourType initialTradingHourType, TradingHourType finalTradingHourType)
        {
            this.InitialTadingHour = TradingHour.CreateTradingHourByType(initialTradingHourType);
            this.FinalTadingHour = TradingHour.CreateTradingHourByType(finalTradingHourType);
        }

        private TradingTimeZone(TradingTimeZoneType type)
        {
            this.InitialTadingHour = type.ToInitialTradingHour();
            this.FinalTadingHour = type.ToFinalTradingHour();
        }

        #endregion

        #region Public methods

        public static TradingTimeZone CreateTimeZoneByType(TradingTimeZoneType type)
        {
            return new TradingTimeZone(type);
        }

        //public DateTime GetInitialTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        //{
        //    return TimeZoneInfo.ConvertTime(currentDate.Date + Time,TimeZoneInfo,targetTimeZoneInfo);
        //}

        #endregion

    }
}
