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
        public static TradingTime ToBeginTradingHour(this TradingHoursType type, InstrumentCode code = InstrumentCode.Default)
        {

            switch (type)
            {
                case (TradingHoursType.American):
                    return GeneralTradingTime.American_Open.ToTradingHour();
                case (TradingHoursType.American_IB):
                    return GeneralTradingTime.American_IB_Open.ToTradingHour();
                case (TradingHoursType.American_FB):
                    return GeneralTradingTime.American_FB_Open.ToTradingHour();

                case (TradingHoursType.Electronic):
                    return SpecificTradingTime.Electronic_Open.ToTradingHour(code);
                case (TradingHoursType.Electronic_IB):
                    return SpecificTradingTime.Electronic_IB_Open.ToTradingHour(code);
                case (TradingHoursType.Electronic_FB):
                    return SpecificTradingTime.Electronic_FB_Open.ToTradingHour(code);

                case (TradingHoursType.OVN):
                    return SpecificTradingTime.OVN_Open.ToTradingHour(code);

                case (TradingHoursType.Asian):
                    return GeneralTradingTime.Asian_Open.ToTradingHour();
                case (TradingHoursType.Asian_IB):
                    return GeneralTradingTime.Asian_IB_Open.ToTradingHour();
                case (TradingHoursType.Asian_FB):
                    return GeneralTradingTime.Asian_FB_Open.ToTradingHour();

                case (TradingHoursType.European):
                    return GeneralTradingTime.European_Open.ToTradingHour();
                case (TradingHoursType.European_IB):
                    return GeneralTradingTime.European_IB_Open.ToTradingHour();
                case (TradingHoursType.European_FB):
                    return GeneralTradingTime.European_FB_Open.ToTradingHour();

                default:
                    throw new Exception("The traging time zone doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursType"/> to final <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Final <see cref="TradingTime"/> of the <see cref="TradingHoursType"/>.</returns>
        public static TradingTime ToEndTradingHour(this TradingHoursType type, InstrumentCode code = InstrumentCode.Default)
        {

            switch (type)
            {
                case (TradingHoursType.American):
                    return GeneralTradingTime.American_Close.ToTradingHour();
                case (TradingHoursType.American_IB):
                    return GeneralTradingTime.American_IB_Close.ToTradingHour();
                case (TradingHoursType.American_FB):
                    return GeneralTradingTime.American_FB_Close.ToTradingHour();

                case (TradingHoursType.Electronic):
                    return SpecificTradingTime.Electronic_Close.ToTradingHour(code);
                case (TradingHoursType.Electronic_IB):
                    return SpecificTradingTime.Electronic_IB_Close.ToTradingHour(code);
                case (TradingHoursType.Electronic_FB):
                    return SpecificTradingTime.Electronic_FB_Close.ToTradingHour(code);

                case (TradingHoursType.OVN):
                    return SpecificTradingTime.OVN_Close.ToTradingHour(code);

                case (TradingHoursType.Asian):
                    return GeneralTradingTime.Asian_Close.ToTradingHour();
                case (TradingHoursType.Asian_IB):
                    return GeneralTradingTime.Asian_IB_Close.ToTradingHour();
                case (TradingHoursType.Asian_FB):
                    return GeneralTradingTime.Asian_FB_Close.ToTradingHour();

                case (TradingHoursType.European):
                    return GeneralTradingTime.European_Close.ToTradingHour();
                case (TradingHoursType.European_IB):
                    return GeneralTradingTime.European_IB_Close.ToTradingHour();
                case (TradingHoursType.European_FB):
                    return GeneralTradingTime.European_FB_Close.ToTradingHour();

                default:
                    throw new Exception("The traging time zone doesn't exists.");
            }
        }

        public static TradingHours ToTradingTimeZone(this TradingHoursType type)
        {
            return TradingHours.CreateTradingHoursByType(type);
        }

        public static TradingHoursType[] ToArray(this TradingHoursType type)
        {
            return Enum.GetValues(typeof(TradingHoursType)).Cast<TradingHoursType>().ToArray();
        }
    }
}
