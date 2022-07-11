using System;

namespace NtCore
{
    /// <summary>
    /// Contents properties and methods of specific trading time zone.
    /// </summary>
    public class TradingHours
    {

        #region Private members

        private SpecificTradingHours type;

        //private InstrumentCode instrumentCode;

        #endregion

        #region Public properties

        public string DisplayName => type.ToString();
        public string Description { get; set; }

        /// <summary>
        /// The initial trading hour.
        /// </summary>
        public TradingTime BeginTime { get; set; }

        /// <summary>
        /// The final trading hour.
        /// </summary>
        public TradingTime EndTime { get; set; }

        /// <summary>
        /// Trading hours sessions.
        /// </summary>
        public TradingSessions Sessions { get; set; }

        #endregion

        #region Constructors

        private TradingHours(SpecificTradingTime beginTradingTime, SpecificTradingTime endTradingTime)
        {
            this.BeginTime = TradingTime.CreateTradingTimeByType(beginTradingTime);
            this.EndTime = TradingTime.CreateTradingTimeByType(endTradingTime);
        }

        private TradingHours(SpecificTradingHours type)
        {
            this.type = type;
            this.BeginTime = type.ToBeginTradingTime();
            this.EndTime = type.ToEndTradingTime();
        }

        #endregion

        #region Instance methods

        public static TradingHours CreateTradingHoursByType(SpecificTradingHours type)
        {
            return new TradingHours(type);
        }

        public static TradingHours CreateTradingHoursByTradingTimes(SpecificTradingTime beginTradingTime, SpecificTradingTime endTradingTime)
        {
            return new TradingHours(beginTradingTime, endTradingTime);
        }

        //public static TradingHours CreateTradingHoursByTradingTimes(GeneralTradingTime beginTradingTime, SpecificTradingTime endTradingTime, InstrumentCode code = InstrumentCode.Default)
        //{
        //    return new TradingHours(beginTradingTime, endTradingTime);
        //}

        //public static TradingHours CreateTradingHoursByTradingTimes(GeneralTradingTime beginTradingTime, GeneralTradingTime endTradingTime)
        //{
        //    return new TradingHours(beginTradingTime, endTradingTime);
        //}

        #endregion

        #region Public methods

        public DateTime GetBeginDateTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return currentDate + (targetTimeZoneInfo.BaseUtcOffset - BeginTime.TimeZoneInfo.BaseUtcOffset) + BeginTime.Time;
        }

        public DateTime GetEndDateTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return currentDate + (targetTimeZoneInfo.BaseUtcOffset - EndTime.TimeZoneInfo.BaseUtcOffset) + EndTime.Time;
        }

        public DateTime GetBeginDateTime(TimeZoneInfo targetTimeZoneInfo)
        {
            return GetBeginDateTime(DateTime.Now.Date, targetTimeZoneInfo);
        }

        public DateTime GetEndDateTime(TimeZoneInfo targetTimeZoneInfo)
        {
            var timeSpan = EndTime.Time - BeginTime.Time;
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
