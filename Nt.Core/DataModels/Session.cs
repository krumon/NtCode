using System;

namespace Nt.Core
{
    /// <summary>
    /// Represents the trading hours session definition.
    /// </summary>
    public class Session
    {

        /// <summary>
        /// A DayOfWeek value representing the begin day.
        /// </summary>
        public DayOfWeek BeginDay { get; set; }

        /// <summary>
        /// An int value representing the begin time.
        /// </summary>
        public int BeginTime { get; set; }

        /// <summary>
        /// A DayOfWeek value representing the end day.
        /// </summary>
        public DayOfWeek EndDay { get; set; }

        /// <summary>
        /// An int value representing the end time.
        /// </summary>
        public int EndTime { get; set; }

        /// <summary>
        /// A DayOfWeek value representing the trading day this session belongs to.
        /// </summary>
        public DayOfWeek TradingDay { get; set; }


    }
}
