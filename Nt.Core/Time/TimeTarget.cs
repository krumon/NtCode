using NinjaTrader.Core;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace NtCore
{
    public class TimeTarget
    {

        #region Public properties

        /// <summary>
        /// Gets or sets the source time zone info of the <see cref="TimeTarget"/>.
        /// </summary>
        TimeZoneInfo SourceTimeZone { get; set; } = TimeZoneInfo.Local;

        /// <summary>
        /// Gets or sets the source time zone info of the <see cref="TimeTarget"/>.
        /// </summary>
        TimeZoneInfo TargetTimeZone { get; set; } = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        /// <summary>
        /// Gets or sets the date of the <see cref="TimeTarget"/>.
        /// </summary>
        DateTime TargetDate { get; set; } = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0,DateTimeKind.Local);

        /// <summary>
        /// Gets or sets the time of the <see cref="TimeTarget"/>.
        /// </summary>
        TimeSpan TargetTime { get; set; } = new TimeSpan(17, 32, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Default instance of <see cref="TimeTarget"/> object.
        /// </summary>
        public TimeTarget()
        {
        }

        #endregion

        #region Public methods

        public DateTime GetTime()
        {
            return TimeZoneInfo.ConvertTime(TargetDate + TargetTime,SourceTimeZone, TargetTimeZone);
        }


        #endregion

    }
}
