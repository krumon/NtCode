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
        public TradingHour BeginTradingHour { get; set; }

        /// <summary>
        /// The final trading hour.
        /// </summary>
        public TradingHour EndTradingHour { get; set; }

        #endregion

        #region Constructors

        private TradingTimeZone(TradingHourType beginTradingHourType, TradingHourType endTradingHourType)
        {
            this.BeginTradingHour = TradingHour.CreateTradingHourByType(beginTradingHourType);
            this.EndTradingHour = TradingHour.CreateTradingHourByType(endTradingHourType);
        }

        private TradingTimeZone(TradingTimeZoneType type)
        {
            this.BeginTradingHour = type.ToBeginTradingHour();
            this.EndTradingHour = type.ToEndTradingHour();
        }

        #endregion

        #region Instance methods

        public static TradingTimeZone CreateTimeZoneByType(TradingTimeZoneType type)
        {
            return new TradingTimeZone(type);
        }

        public static TradingTimeZone CreateTimeZoneByTradingHoursType(TradingHourType beginTradingHourType, TradingHourType endTradingHourType)
        {
            return new TradingTimeZone(beginTradingHourType, endTradingHourType);
        }

        #endregion

        #region Public methods

        public DateTime GetBeginDateTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(currentDate.Date + BeginTradingHour.Time, BeginTradingHour.TimeZoneInfo, targetTimeZoneInfo);
        }

        public DateTime GetEndDateTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(currentDate.Date + EndTradingHour.Time, EndTradingHour.TimeZoneInfo, targetTimeZoneInfo);
        }

        public TimeSpan GetBeginTime(TimeZoneInfo targetTimeZoneInfo)
        {
            return GetBeginDateTime(DateTime.Today,targetTimeZoneInfo).TimeOfDay;
        }

        public TimeSpan GetEndTime(TimeZoneInfo targetTimeZoneInfo)
        {
            return GetEndDateTime(DateTime.Today, targetTimeZoneInfo).TimeOfDay;
        }

        public int BeginTime(TimeZoneInfo targetTimeZoneInfo)
        {
            TimeSpan time = GetBeginTime(targetTimeZoneInfo);
            return (time.Hours*10000)+(time.Minutes*100)+(time.Seconds);
        }

        public int EndTime(TimeZoneInfo targetTimeZoneInfo)
        {
            TimeSpan time = GetEndTime(targetTimeZoneInfo);
            return (time.Hours*10000)+(time.Minutes*100)+(time.Seconds);
        }

        #endregion

    }
}
