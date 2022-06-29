using System;

namespace NtCore
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum TradingHourType
    {

        ETH_Open,
        ETH_Close,
        ETH_IB_Open,
        ETH_IB_Close,
        ETH_FB_Open,
        ETH_FB_Close,

        Asian_Open,
        Asian_Close,
        Asian_IB_Open,
        Asian_IB_Close,
        Asian_FB_Open,
        Asian_FB_Close,

        American_Open,
        American_Close,
        American_IB_Open,
        American_IB_Close,
        American_FB_Open,
        American_FB_Close,

        European_Open,
        European_Close,
        European_IB_Open,
        European_IB_Close,
        European_FB_Open,
        European_FB_Close,

    }

    /// <summary>
    /// Helper methods to the label line drawing tool
    /// </summary>
    public static class TradingHourTypeHelpers
    {

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

                case (TradingHourType.Asian_Open):
                    return new TimeSpan(9, 0, 0);
                case (TradingHourType.Asian_Close):
                    return new TimeSpan(15, 0, 0);
                case (TradingHourType.Asian_IB_Open):
                    return new TimeSpan(9, 0, 0);
                case (TradingHourType.Asian_IB_Close):
                    return new TimeSpan(9, 5, 0);
                case (TradingHourType.Asian_FB_Open):
                    return new TimeSpan(14, 50, 0);
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
