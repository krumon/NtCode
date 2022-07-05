using System;
using System.Linq;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="TradingHoursType"/> enum.
    /// </summary>
    public static class TradingHoursTypeHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="TradingHoursType"/> to initial <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Initial <see cref="TradingTime"/> of the <see cref="TradingHoursType"/>.</returns>
        public static TradingTime ToBeginTradingHour(this TradingHoursType type)
        {

            switch (type)
            {
                case (TradingHoursType.American):
                    return TradingTimeType.American_Open.ToTradingHour();
                case (TradingHoursType.American_IB):
                    return TradingTimeType.American_IB_Open.ToTradingHour();
                case (TradingHoursType.American_FB):
                    return TradingTimeType.American_FB_Open.ToTradingHour();

                case (TradingHoursType.ETH):
                    return TradingTimeType.ETH_Open.ToTradingHour();
                case (TradingHoursType.ETH_IB):
                    return TradingTimeType.ETH_IB_Open.ToTradingHour();
                case (TradingHoursType.ETH_FB):
                    return TradingTimeType.ETH_FB_Open.ToTradingHour();

                case (TradingHoursType.OVN):
                    return TradingTimeType.OVN_Open.ToTradingHour();

                case (TradingHoursType.Asian):
                    return TradingTimeType.Asian_Open.ToTradingHour();
                case (TradingHoursType.Asian_IB):
                    return TradingTimeType.Asian_IB_Open.ToTradingHour();
                case (TradingHoursType.Asian_FB):
                    return TradingTimeType.Asian_FB_Open.ToTradingHour();

                case (TradingHoursType.European):
                    return TradingTimeType.European_Open.ToTradingHour();
                case (TradingHoursType.European_IB):
                    return TradingTimeType.European_IB_Open.ToTradingHour();
                case (TradingHoursType.European_FB):
                    return TradingTimeType.European_FB_Open.ToTradingHour();

                default:
                    throw new Exception("The traging time zone doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursType"/> to final <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Final <see cref="TradingTime"/> of the <see cref="TradingHoursType"/>.</returns>
        public static TradingTime ToEndTradingHour(this TradingHoursType type)
        {

            switch (type)
            {
                case (TradingHoursType.American):
                    return TradingTimeType.American_Close.ToTradingHour();
                case (TradingHoursType.American_IB):
                    return TradingTimeType.American_IB_Close.ToTradingHour();
                case (TradingHoursType.American_FB):
                    return TradingTimeType.American_FB_Close.ToTradingHour();

                case (TradingHoursType.ETH):
                    return TradingTimeType.ETH_Close.ToTradingHour();
                case (TradingHoursType.ETH_IB):
                    return TradingTimeType.ETH_IB_Close.ToTradingHour();
                case (TradingHoursType.ETH_FB):
                    return TradingTimeType.ETH_FB_Close.ToTradingHour();

                case (TradingHoursType.OVN):
                    return TradingTimeType.OVN_Close.ToTradingHour();

                case (TradingHoursType.Asian):
                    return TradingTimeType.Asian_Close.ToTradingHour();
                case (TradingHoursType.Asian_IB):
                    return TradingTimeType.Asian_IB_Close.ToTradingHour();
                case (TradingHoursType.Asian_FB):
                    return TradingTimeType.Asian_FB_Close.ToTradingHour();

                case (TradingHoursType.European):
                    return TradingTimeType.European_Close.ToTradingHour();
                case (TradingHoursType.European_IB):
                    return TradingTimeType.European_IB_Close.ToTradingHour();
                case (TradingHoursType.European_FB):
                    return TradingTimeType.European_FB_Close.ToTradingHour();

                default:
                    throw new Exception("The traging time zone doesn't exists.");
            }
        }

        public static TradingHours ToTradingTimeZone(this TradingHoursType type)
        {
            return TradingHours.CreateTimeZoneByType(type);
        }

        public static TradingHoursType[] ToArray(this TradingHoursType type)
        {
            return Enum.GetValues(typeof(TradingHoursType)).Cast<TradingHoursType>().ToArray();
        }
    }
}
