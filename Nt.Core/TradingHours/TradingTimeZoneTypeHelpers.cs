﻿using System;
using System.Linq;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="TradingTimeZoneType"/> enum.
    /// </summary>
    public static class TradingTimeZoneTypeHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="TradingTimeZoneType"/> to initial <see cref="TradingHour"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Initial <see cref="TradingHour"/> of the <see cref="TradingTimeZoneType"/>.</returns>
        public static TradingHour ToBeginTradingHour(this TradingTimeZoneType type)
        {

            switch (type)
            {
                case (TradingTimeZoneType.American):
                    return TradingHourType.American_Open.ToTradingHour();
                case (TradingTimeZoneType.American_IB):
                    return TradingHourType.American_IB_Open.ToTradingHour();
                case (TradingTimeZoneType.American_FB):
                    return TradingHourType.American_FB_Open.ToTradingHour();

                case (TradingTimeZoneType.ETH):
                    return TradingHourType.ETH_Open.ToTradingHour();
                case (TradingTimeZoneType.ETH_IB):
                    return TradingHourType.ETH_IB_Open.ToTradingHour();
                case (TradingTimeZoneType.ETH_FB):
                    return TradingHourType.ETH_FB_Open.ToTradingHour();

                case (TradingTimeZoneType.OVN):
                    return TradingHourType.OVN_Open.ToTradingHour();

                case (TradingTimeZoneType.Asian):
                    return TradingHourType.Asian_Open.ToTradingHour();
                case (TradingTimeZoneType.Asian_IB):
                    return TradingHourType.Asian_IB_Open.ToTradingHour();
                case (TradingTimeZoneType.Asian_FB):
                    return TradingHourType.Asian_FB_Open.ToTradingHour();

                case (TradingTimeZoneType.European):
                    return TradingHourType.European_Open.ToTradingHour();
                case (TradingTimeZoneType.European_IB):
                    return TradingHourType.European_IB_Open.ToTradingHour();
                case (TradingTimeZoneType.European_FB):
                    return TradingHourType.European_FB_Open.ToTradingHour();

                default:
                    throw new Exception("The traging time zone doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingTimeZoneType"/> to final <see cref="TradingHour"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Final <see cref="TradingHour"/> of the <see cref="TradingTimeZoneType"/>.</returns>
        public static TradingHour ToEndTradingHour(this TradingTimeZoneType type)
        {

            switch (type)
            {
                case (TradingTimeZoneType.American):
                    return TradingHourType.American_Close.ToTradingHour();
                case (TradingTimeZoneType.American_IB):
                    return TradingHourType.American_IB_Close.ToTradingHour();
                case (TradingTimeZoneType.American_FB):
                    return TradingHourType.American_FB_Close.ToTradingHour();

                case (TradingTimeZoneType.ETH):
                    return TradingHourType.ETH_Close.ToTradingHour();
                case (TradingTimeZoneType.ETH_IB):
                    return TradingHourType.ETH_IB_Close.ToTradingHour();
                case (TradingTimeZoneType.ETH_FB):
                    return TradingHourType.ETH_FB_Close.ToTradingHour();

                case (TradingTimeZoneType.OVN):
                    return TradingHourType.OVN_Close.ToTradingHour();

                case (TradingTimeZoneType.Asian):
                    return TradingHourType.Asian_Close.ToTradingHour();
                case (TradingTimeZoneType.Asian_IB):
                    return TradingHourType.Asian_IB_Close.ToTradingHour();
                case (TradingTimeZoneType.Asian_FB):
                    return TradingHourType.Asian_FB_Close.ToTradingHour();

                case (TradingTimeZoneType.European):
                    return TradingHourType.European_Close.ToTradingHour();
                case (TradingTimeZoneType.European_IB):
                    return TradingHourType.European_IB_Close.ToTradingHour();
                case (TradingTimeZoneType.European_FB):
                    return TradingHourType.European_FB_Close.ToTradingHour();

                default:
                    throw new Exception("The traging time zone doesn't exists.");
            }
        }

        public static TradingTimeZone ToTradingTimeZone(this TradingTimeZoneType type)
        {
            return TradingTimeZone.CreateTimeZoneByType(type);
        }

        public static TradingTimeZoneType[] ToArray(this TradingTimeZoneType type)
        {
            return Enum.GetValues(typeof(TradingTimeZoneType)).Cast<TradingTimeZoneType>().ToArray();
        }
    }
}
