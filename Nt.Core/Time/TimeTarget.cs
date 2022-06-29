using NinjaTrader.Core;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace NtCore
{
    public class TimeTarget
    {
        #region Private members

        /// <summary>
        /// Gets or sets the source time zone info of the <see cref="TimeTarget"/>.
        /// </summary>
        private TimeZoneInfo designTargetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the source time zone info of the <see cref="TimeTarget"/>.
        /// </summary>
        public TimeZoneInfo SourceTimeZone { get; set; } = TimeZoneInfo.Local;

        /// <summary>
        /// Gets or sets the time of the <see cref="TimeTarget"/>.
        /// </summary>
        public TimeSpan TargetTime { get; set; } = new TimeSpan(17, 32, 0);

        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public TimeSpan CurrentTime { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Create default instance of <see cref="TimeTarget"/> object.
        /// </summary>
        public TimeTarget()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the target 
        /// </summary>
        /// <param name="targetTimeZoneInfo"></param>
        /// <returns></returns>
        public DateTime GetTime(TimeZoneInfo targetTimeZoneInfo = null)
        {
            if (targetTimeZoneInfo == null)
                return TimeZoneInfo.ConvertTime(CurrentDate + TargetTime, SourceTimeZone, designTargetTimeZone);
            else
                return TimeZoneInfo.ConvertTime(CurrentDate + TargetTime, SourceTimeZone, targetTimeZoneInfo);
        }


        #endregion

    }
}
