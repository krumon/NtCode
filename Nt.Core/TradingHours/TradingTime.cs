using NinjaTrader.NinjaScript;
using System;

namespace NtCore
{
    public class TradingTime
    {
        #region Private members

        private readonly string code;
        private readonly string description;
        protected SpecificTradingTime specificTradingTime;

        #endregion

        #region Public properties

        /// <summary>
        /// The unique code of the trading time.
        /// </summary>
        public string Code => specificTradingTime == SpecificTradingTime.Custom ? code : specificTradingTime.ToCode();

        /// <summary>
        /// the description of the trading time.
        /// </summary>
        public string Description => specificTradingTime == SpecificTradingTime.Custom ? description : specificTradingTime.ToDescription();

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
        private TradingTime(SpecificTradingTime type, InstrumentCode code = InstrumentCode.Default)
        {
            this.TimeZoneInfo = type.ToTimeZoneInfo(code);
            this.Time = type.ToTimeSpan(code);
        }

        /// <summary>
        /// Create instance of <see cref="TradingTime"/> class whith custom values.
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        private TradingTime(TimeZoneInfo timeZoneInfo, int hour, int minute, int seconds, string code, string description = "")
        {
            specificTradingTime = SpecificTradingTime.Custom;
            this.code = code;
            this.description = description;
            this.TimeZoneInfo = timeZoneInfo;
            this.Time = new TimeSpan(hour,minute,seconds);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Create default instance of <see cref="TradingTime"/> class.
        /// </summary>
        /// <param name="type">The trading hour type for create the instance.</param>
        /// <returns>The trading hour instance.</returns>
        public static TradingTime CreateTradingTimeByType(SpecificTradingTime type, InstrumentCode code = InstrumentCode.Default)
        {
            return new TradingTime(type,code);
        }

        /// <summary>
        /// Create default instance of <see cref="TradingTime"/> class whith custom values.
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="seconds"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static TradingTime CreateCustomTradingTime(TimeZoneInfo timeZoneInfo, int hour, int minute, int seconds, string code, string description = "")
        {
            return new TradingTime(timeZoneInfo,hour,minute,seconds,code, description);
        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="currentDate">The current date to create the date time structure.</param>
        /// <param name="targetTimeZoneInfo">The time zone info to convert the date time structure.</param>
        /// <returns></returns>
        public DateTime GetTradingTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(currentDate.Date + Time,TimeZoneInfo,targetTimeZoneInfo);
        }

        #endregion

    }
}
