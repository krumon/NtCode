using System;

namespace NtCore
{

    /// <summary>
    /// Helper methods to the label line drawing tool
    /// </summary>
    public static class TradingHourTypeHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="TradingHourType"/> to <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="tradingHourName"></param>
        /// <returns><see cref="TimeZoneInfo"/> of the <see cref="TradingHourType"/>.</returns>
        /// <exception cref="Exception"></exception>
        public static TimeZoneInfo ToTimeZoneInfo(this TradingHourType tradingHourName)
        {

            switch (tradingHourName)
            {
                case (TradingHourType.American_Open):
                case (TradingHourType.American_Close):
                case (TradingHourType.American_IB_Open):
                case (TradingHourType.American_IB_Close):
                case (TradingHourType.American_FB_Open):
                case (TradingHourType.American_FB_Close):
                case (TradingHourType.ETH_Open):
                case (TradingHourType.ETH_Close):
                case (TradingHourType.ETH_IB_Open):
                case (TradingHourType.ETH_IB_Close):
                case (TradingHourType.ETH_FB_Open):
                case (TradingHourType.ETH_FB_Close):
                case (TradingHourType.OVN_Open):
                case (TradingHourType.OVN_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                case (TradingHourType.Asian_Open):
                case (TradingHourType.Asian_Close):
                case (TradingHourType.Asian_IB_Open):
                case (TradingHourType.Asian_IB_Close):
                case (TradingHourType.Asian_FB_Open):
                case (TradingHourType.Asian_FB_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

                case (TradingHourType.European_Open):
                case (TradingHourType.European_Close):
                case (TradingHourType.European_IB_Open):
                case (TradingHourType.European_IB_Close):
                case (TradingHourType.European_FB_Open):
                case (TradingHourType.European_FB_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                default:
                throw new Exception("The traging hour name doesn't exists.");

            }

        }

        /// <summary>
        /// Method to convert the <see cref="TradingHourType"/> to <see cref="Time"/>.
        /// </summary>
        /// <param name="tradingHourName"></param>
        /// <returns><see cref="TimeSpan"/> of the <see cref="TradingHourType"/>.</returns>
        /// <exception cref="Exception"></exception>
        public static TimeSpan ToTimeSpan(this TradingHourType tradingHourName)
        {
            switch (tradingHourName)
            {
                case (TradingHourType.American_Open):
                    return new TimeSpan(8, 30, 0);
                case (TradingHourType.American_Close):
                    return new TimeSpan(16, 0, 0);
                case (TradingHourType.American_IB_Open):
                    return new TimeSpan(8, 30, 0);
                case (TradingHourType.American_IB_Close):
                    return new TimeSpan(9, 30, 0);
                case (TradingHourType.American_FB_Open):
                    return new TimeSpan(15, 30, 0);
                case (TradingHourType.American_FB_Close):
                    return new TimeSpan(16, 0, 0);

                case (TradingHourType.ETH_Open):
                    return new TimeSpan(17, 0, 0);
                case (TradingHourType.ETH_Close):
                    return new TimeSpan(16, 0, 0);
                case (TradingHourType.ETH_IB_Open):
                    return new TimeSpan(17, 0, 0);
                case (TradingHourType.ETH_IB_Close):
                    return new TimeSpan(17, 5, 0);
                case (TradingHourType.ETH_FB_Open):
                    return new TimeSpan(15, 45, 0);
                case (TradingHourType.ETH_FB_Close):
                    return new TimeSpan(16, 0, 0);

                case (TradingHourType.OVN_Open):
                    return new TimeSpan(16, 0, 0);
                case (TradingHourType.OVN_Close):
                    return new TimeSpan(8, 30, 0);

                case (TradingHourType.Asian_Open):
                    return new TimeSpan(9, 0, 0);
                case (TradingHourType.Asian_Close):
                    return new TimeSpan(15, 0, 0);
                case (TradingHourType.Asian_IB_Open):
                    return new TimeSpan(9, 0, 0);
                case (TradingHourType.Asian_IB_Close):
                    return new TimeSpan(9, 15, 0);
                case (TradingHourType.Asian_FB_Open):
                    return new TimeSpan(14, 45, 0);
                case (TradingHourType.Asian_FB_Close):
                    return new TimeSpan(15, 0, 0);

                case (TradingHourType.European_Open):
                    return new TimeSpan(9, 0, 0);
                case (TradingHourType.European_Close):
                    return new TimeSpan(17, 30, 0);
                case (TradingHourType.European_IB_Open):
                    return new TimeSpan(9, 0, 0);
                case (TradingHourType.European_IB_Close):
                    return new TimeSpan(9, 15, 0);
                case (TradingHourType.European_FB_Open):
                    return new TimeSpan(17, 15, 0);
                case (TradingHourType.European_FB_Close):
                    return new TimeSpan(17, 30, 0);

                default:
                    throw new Exception("The traging hour name doesn't exists.");

            }

        }

    }
}
