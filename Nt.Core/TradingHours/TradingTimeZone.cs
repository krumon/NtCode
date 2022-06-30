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

        public TimeZoneInfo TimeZoneInfo { get; set; }
        public TimeSpan Time { get; set; }

        #endregion

        #region Constructors

        private TradingTimeZone(TradingHourType type)
        {
            this.TimeZoneInfo = type.ToTimeZoneInfo();
            this.Time = type.ToTimeSpan();
        }

        #endregion

        #region Public methods

        public static TradingTimeZone CreateTradingHourByType(NinjaScriptBase ninjaScript, TradingHourType type)
        {
            return new TradingTimeZone(type);
        }

        public DateTime GetTradingHour(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(currentDate.Date + Time,TimeZoneInfo,targetTimeZoneInfo);
        }

        #endregion

    }
}
