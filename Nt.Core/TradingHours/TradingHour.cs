using System;

namespace NtCore
{
    public class TradingHour
    {

        #region Public properties

        public TimeZoneInfo TimeZoneInfo { get; set; }
        public TimeSpan Time { get; set; }

        #endregion

        #region Constructors

        public TradingHour()
        {

        }

        #endregion

        #region Public methods

        public DateTime GetTradingHour(TradingHourType tradingHourType)
        {
            return DateTime.Now;
        }

        #endregion

    }
}
