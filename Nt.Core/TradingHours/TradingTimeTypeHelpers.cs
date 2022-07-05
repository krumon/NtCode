using System;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="TradingTimeType"/> enum.
    /// </summary>
    public static class TradingTimeTypeHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="TradingTimeType"/> to <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="tradingHourName"></param>
        /// <returns><see cref="TimeZoneInfo"/> of the <see cref="TradingTimeType"/>.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this TradingTimeType tradingHourName)
        {

            switch (tradingHourName)
            {
                case (TradingTimeType.American_Open):
                case (TradingTimeType.American_Close):
                case (TradingTimeType.American_IB_Open):
                case (TradingTimeType.American_IB_Close):
                case (TradingTimeType.American_FB_Open):
                case (TradingTimeType.American_FB_Close):
                case (TradingTimeType.ETH_Open):
                case (TradingTimeType.ETH_Close):
                case (TradingTimeType.ETH_IB_Open):
                case (TradingTimeType.ETH_IB_Close):
                case (TradingTimeType.ETH_FB_Open):
                case (TradingTimeType.ETH_FB_Close):
                case (TradingTimeType.OVN_Open):
                case (TradingTimeType.OVN_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                case (TradingTimeType.Asian_Open):
                case (TradingTimeType.Asian_Close):
                case (TradingTimeType.Asian_IB_Open):
                case (TradingTimeType.Asian_IB_Close):
                case (TradingTimeType.Asian_FB_Open):
                case (TradingTimeType.Asian_FB_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

                case (TradingTimeType.European_Open):
                case (TradingTimeType.European_Close):
                case (TradingTimeType.European_IB_Open):
                case (TradingTimeType.European_IB_Close):
                case (TradingTimeType.European_FB_Open):
                case (TradingTimeType.European_FB_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                default:
                throw new Exception("The traging hour name doesn't exists.");

            }

        }

        /// <summary>
        /// Method to convert the <see cref="TradingTimeType"/> to <see cref="Time"/>.
        /// </summary>
        /// <param name="tradingHourName"></param>
        /// <returns><see cref="TimeSpan"/> of the <see cref="TradingTimeType"/>.</returns>
        public static TimeSpan ToTimeSpan(this TradingTimeType tradingHourName)
        {
            switch (tradingHourName)
            {
                case (TradingTimeType.American_Open):
                    return new TimeSpan(8, 30, 0);
                case (TradingTimeType.American_Close):
                    return new TimeSpan(15, 0, 0);
                case (TradingTimeType.American_IB_Open):
                    return new TimeSpan(8, 30, 0);
                case (TradingTimeType.American_IB_Close):
                    return new TimeSpan(9, 30, 0);
                case (TradingTimeType.American_FB_Open):
                    return new TimeSpan(14, 30, 0);
                case (TradingTimeType.American_FB_Close):
                    return new TimeSpan(15, 0, 0);

                case (TradingTimeType.ETH_Open):
                    return new TimeSpan(17, 0, 0);
                case (TradingTimeType.ETH_Close):
                    return new TimeSpan(16, 0, 0);
                case (TradingTimeType.ETH_IB_Open):
                    return new TimeSpan(17, 0, 0);
                case (TradingTimeType.ETH_IB_Close):
                    return new TimeSpan(17, 5, 0);
                case (TradingTimeType.ETH_FB_Open):
                    return new TimeSpan(15, 55, 0);
                case (TradingTimeType.ETH_FB_Close):
                    return new TimeSpan(16, 0, 0);

                case (TradingTimeType.OVN_Open):
                    return new TimeSpan(16, 0, 0);
                case (TradingTimeType.OVN_Close):
                    return new TimeSpan(8, 30, 0);

                case (TradingTimeType.Asian_Open):
                    return new TimeSpan(9, 0, 0);
                case (TradingTimeType.Asian_Close):
                    return new TimeSpan(15, 0, 0);
                case (TradingTimeType.Asian_IB_Open):
                    return new TimeSpan(9, 0, 0);
                case (TradingTimeType.Asian_IB_Close):
                    return new TimeSpan(9, 15, 0);
                case (TradingTimeType.Asian_FB_Open):
                    return new TimeSpan(14, 45, 0);
                case (TradingTimeType.Asian_FB_Close):
                    return new TimeSpan(15, 0, 0);

                case (TradingTimeType.European_Open):
                    return new TimeSpan(8, 0, 0);
                case (TradingTimeType.European_Close):
                    return new TimeSpan(16, 30, 0);
                case (TradingTimeType.European_IB_Open):
                    return new TimeSpan(8, 0, 0);
                case (TradingTimeType.European_IB_Close):
                    return new TimeSpan(8, 15, 0);
                case (TradingTimeType.European_FB_Open):
                    return new TimeSpan(16, 15, 0);
                case (TradingTimeType.European_FB_Close):
                    return new TimeSpan(16, 30, 0);

                default:
                    throw new Exception("The traging hour name doesn't exists.");

            }
        }
        
        /// <summary>
        /// Returns <see cref="TradingTime"/> by trading hour type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns><see cref="TradingTime"/>.</returns>
        public static TradingTime ToTradingHour(this TradingTimeType type)
        {
            return TradingTime.CreateTradingHourByType(type);
        }

    }
}
