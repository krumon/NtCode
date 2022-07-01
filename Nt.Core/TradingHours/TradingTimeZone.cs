using NinjaTrader.NinjaScript;
using System;

namespace NtCore
{
    /// <summary>
    /// Contents properties and methods of specific trading time zone.
    /// </summary>
    public class TradingTimeZone
    {

        #region Private members

        private TradingTimeZoneType type;

        #endregion

        #region Public properties

        public string DisplayName => type.ToString();
        public string Description { get; set; }

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
            this.type = type;
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
            return currentDate + (targetTimeZoneInfo.BaseUtcOffset - BeginTradingHour.TimeZoneInfo.BaseUtcOffset) + BeginTradingHour.Time;
        }

        public DateTime GetEndDateTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return currentDate + (targetTimeZoneInfo.BaseUtcOffset - EndTradingHour.TimeZoneInfo.BaseUtcOffset) + EndTradingHour.Time;
        }

        public DateTime GetBeginDateTime(TimeZoneInfo targetTimeZoneInfo)
        {
            return GetBeginDateTime(DateTime.Now.Date, targetTimeZoneInfo);
        }

        public DateTime GetEndDateTime(TimeZoneInfo targetTimeZoneInfo)
        {
            var timeSpan = EndTradingHour.Time - BeginTradingHour.Time;
            if (timeSpan.Hours < 0)
                timeSpan += TimeSpan.FromHours(24);
            return GetBeginDateTime(targetTimeZoneInfo) + timeSpan;
        }

        public TimeSpan GetBeginTime(TimeZoneInfo targetTimeZoneInfo)
        {
            return GetBeginDateTime(targetTimeZoneInfo).TimeOfDay;
        }

        public TimeSpan GetEndTime(TimeZoneInfo targetTimeZoneInfo)
        {
            return GetEndDateTime(targetTimeZoneInfo).TimeOfDay;
        }

        public int BeginTimeToInteger(TimeZoneInfo targetTimeZoneInfo)
        {
            TimeSpan time = GetBeginTime(targetTimeZoneInfo);
            return (time.Hours*10000)+(time.Minutes*100)+(time.Seconds);
        }

        public int EndTimeToInteger(TimeZoneInfo targetTimeZoneInfo)
        {
            TimeSpan time = GetEndTime(targetTimeZoneInfo);
            return (time.Hours*10000)+(time.Minutes*100)+(time.Seconds);
        }

        #endregion

        #region Override methods

        public override string ToString()
        {
            return String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", DisplayName, "Begin Time: ", GetBeginDateTime(TimeZoneInfo.Local).ToString(), "End Time: ", GetEndDateTime(TimeZoneInfo.Local).ToString());
        }

        #endregion

    }
}
