using System;

namespace NtCore
{

    /// <summary>
    /// Helper methods of trading hours classes.
    /// </summary>
    public static class TradingTimeHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="GeneralTradingTime"/> to <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="tradingHourName"></param>
        /// <returns><see cref="TimeZoneInfo"/> of the <see cref="GeneralTradingTime"/>.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this GeneralTradingTime tradingHourName)
        {

            switch (tradingHourName)
            {
                case (GeneralTradingTime.American_Open):
                case (GeneralTradingTime.American_Close):
                case (GeneralTradingTime.American_IB_Open):
                case (GeneralTradingTime.American_IB_Close):
                case (GeneralTradingTime.American_FB_Open):
                case (GeneralTradingTime.American_FB_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                case (GeneralTradingTime.AmericanAndEuropean_Open):
                case (GeneralTradingTime.AmericanAndEuropean_Close):
                case (GeneralTradingTime.AmericanAndEuropean_IB_Open):
                case (GeneralTradingTime.AmericanAndEuropean_IB_Close):
                case (GeneralTradingTime.AmericanAndEuropean_FB_Open):
                case (GeneralTradingTime.AmericanAndEuropean_FB_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                case (GeneralTradingTime.Asian_Open):
                case (GeneralTradingTime.Asian_Close):
                case (GeneralTradingTime.Asian_IB_Open):
                case (GeneralTradingTime.Asian_IB_Close):
                case (GeneralTradingTime.Asian_FB_Open):
                case (GeneralTradingTime.Asian_FB_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

                case (GeneralTradingTime.European_Open):
                case (GeneralTradingTime.European_Close):
                case (GeneralTradingTime.European_IB_Open):
                case (GeneralTradingTime.European_IB_Close):
                case (GeneralTradingTime.European_FB_Open):
                case (GeneralTradingTime.European_FB_Close):
                    return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                default:
                throw new Exception("The general trading time name doesn't exists.");

            }

        }

        /// <summary>
        /// Method to convert the <see cref="SpecificTradingTime"/> to <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="tradingHourName"></param>
        /// <returns><see cref="TimeZoneInfo"/> of the <see cref="GeneralTradingTime"/>.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this SpecificTradingTime tradingHourName, InstrumentCode code = InstrumentCode.Default)
        {
            switch (code)
            {
                case (InstrumentCode.Default):
                    {
                        switch (tradingHourName)
                        {
                            case (SpecificTradingTime.Electronic_Open):
                            case (SpecificTradingTime.Electronic_Close):
                            case (SpecificTradingTime.Electronic_IB_Open):
                            case (SpecificTradingTime.Electronic_IB_Close):
                            case (SpecificTradingTime.Electronic_FB_Open):
                            case (SpecificTradingTime.Electronic_FB_Close):
                            case (SpecificTradingTime.Regular_Open):
                            case (SpecificTradingTime.Regular_Close):
                            case (SpecificTradingTime.Regular_IB_Open):
                            case (SpecificTradingTime.Regular_IB_Close):
                            case (SpecificTradingTime.Regular_FB_Open):
                            case (SpecificTradingTime.Regular_FB_Close):
                            case (SpecificTradingTime.DAY_Open):
                            case (SpecificTradingTime.DAY_Close):
                            case (SpecificTradingTime.DAY_IB_Open):
                            case (SpecificTradingTime.DAY_IB_Close):
                            case (SpecificTradingTime.DAY_FB_Open):
                            case (SpecificTradingTime.DAY_FB_Close):
                            case (SpecificTradingTime.OVN_Open):
                            case (SpecificTradingTime.OVN_Close):
                            case (SpecificTradingTime.OVN_IB_Open):
                            case (SpecificTradingTime.OVN_IB_Close):
                            case (SpecificTradingTime.OVN_FB_Open):
                            case (SpecificTradingTime.OVN_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            default:
                                throw new Exception("The specific trading time name doesn't exists.");

                        }
                    }

                case (InstrumentCode.MES):
                    {
                        switch (tradingHourName)
                        {
                            case (SpecificTradingTime.Electronic_Open):
                            case (SpecificTradingTime.Electronic_Close):
                            case (SpecificTradingTime.Electronic_IB_Open):
                            case (SpecificTradingTime.Electronic_IB_Close):
                            case (SpecificTradingTime.Electronic_FB_Open):
                            case (SpecificTradingTime.Electronic_FB_Close):
                            case (SpecificTradingTime.Regular_Open):
                            case (SpecificTradingTime.Regular_Close):
                            case (SpecificTradingTime.Regular_IB_Open):
                            case (SpecificTradingTime.Regular_IB_Close):
                            case (SpecificTradingTime.Regular_FB_Open):
                            case (SpecificTradingTime.Regular_FB_Close):
                            case (SpecificTradingTime.DAY_Open):
                            case (SpecificTradingTime.DAY_Close):
                            case (SpecificTradingTime.DAY_IB_Open):
                            case (SpecificTradingTime.DAY_IB_Close):
                            case (SpecificTradingTime.DAY_FB_Open):
                            case (SpecificTradingTime.DAY_FB_Close):
                            case (SpecificTradingTime.OVN_Open):
                            case (SpecificTradingTime.OVN_Close):
                            case (SpecificTradingTime.OVN_IB_Open):
                            case (SpecificTradingTime.OVN_IB_Close):
                            case (SpecificTradingTime.OVN_FB_Open):
                            case (SpecificTradingTime.OVN_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            default:
                                throw new Exception("The specific trading time name doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        /// <summary>
        /// Method to convert the <see cref="GeneralTradingTime"/> to <see cref="Time"/>.
        /// </summary>
        /// <param name="tradingHourName"></param>
        /// <returns><see cref="TimeSpan"/> of the <see cref="GeneralTradingTime"/>.</returns>
        public static TimeSpan ToTimeSpan(this GeneralTradingTime tradingHourName)
        {
            switch (tradingHourName)
            {
                case (GeneralTradingTime.American_Open):
                    return new TimeSpan(8, 30, 0);
                case (GeneralTradingTime.American_Close):
                    return new TimeSpan(15, 0, 0);
                case (GeneralTradingTime.American_IB_Open):
                    return new TimeSpan(8, 30, 0);
                case (GeneralTradingTime.American_IB_Close):
                    return new TimeSpan(9, 30, 0);
                case (GeneralTradingTime.American_FB_Open):
                    return new TimeSpan(14, 30, 0);
                case (GeneralTradingTime.American_FB_Close):
                    return new TimeSpan(15, 0, 0);

                case (GeneralTradingTime.AmericanAndEuropean_Open):
                    return new TimeSpan(8, 30, 0);
                case (GeneralTradingTime.AmericanAndEuropean_Close):
                    return new TimeSpan(10, 30, 0);
                case (GeneralTradingTime.AmericanAndEuropean_IB_Open):
                    return new TimeSpan(8, 30, 0);
                case (GeneralTradingTime.AmericanAndEuropean_IB_Close):
                    return new TimeSpan(9, 30, 0);
                case (GeneralTradingTime.AmericanAndEuropean_FB_Open):
                    return new TimeSpan(10, 15, 0);
                case (GeneralTradingTime.AmericanAndEuropean_FB_Close):
                    return new TimeSpan(10, 30, 0);

                case (GeneralTradingTime.Asian_Open):
                    return new TimeSpan(9, 0, 0);
                case (GeneralTradingTime.Asian_Close):
                    return new TimeSpan(15, 0, 0);
                case (GeneralTradingTime.Asian_IB_Open):
                    return new TimeSpan(9, 0, 0);
                case (GeneralTradingTime.Asian_IB_Close):
                    return new TimeSpan(9, 15, 0);
                case (GeneralTradingTime.Asian_FB_Open):
                    return new TimeSpan(14, 45, 0);
                case (GeneralTradingTime.Asian_FB_Close):
                    return new TimeSpan(15, 0, 0);

                case (GeneralTradingTime.European_Open):
                    return new TimeSpan(8, 0, 0);
                case (GeneralTradingTime.European_Close):
                    return new TimeSpan(16, 30, 0);
                case (GeneralTradingTime.European_IB_Open):
                    return new TimeSpan(8, 0, 0);
                case (GeneralTradingTime.European_IB_Close):
                    return new TimeSpan(8, 15, 0);
                case (GeneralTradingTime.European_FB_Open):
                    return new TimeSpan(16, 15, 0);
                case (GeneralTradingTime.European_FB_Close):
                    return new TimeSpan(16, 30, 0);

                default:
                    throw new Exception("The traging hour name doesn't exists.");

            }
        }

        /// <summary>
        /// Method to convert the <see cref="SpecificTradingTime"/> to <see cref="Time"/>.
        /// </summary>
        /// <param name="tradingHourName"></param>
        /// <returns><see cref="TimeSpan"/> of the <see cref="SpecificTradingTime"/>.</returns>
        public static TimeSpan ToTimeSpan(this SpecificTradingTime tradingHourName, InstrumentCode code = InstrumentCode.Default)
        {
            switch (code)
            {
                case (InstrumentCode.Default):
                    {
                        switch (tradingHourName)
                        {
                            case (SpecificTradingTime.Electronic_Open):
                                return new TimeSpan(17, 0, 0);
                            case (SpecificTradingTime.Electronic_Close):
                                return new TimeSpan(16, 0, 0);
                            case (SpecificTradingTime.Electronic_IB_Open):
                                return new TimeSpan(17, 0, 0);
                            case (SpecificTradingTime.Electronic_IB_Close):
                                return new TimeSpan(17, 15, 0);
                            case (SpecificTradingTime.Electronic_FB_Open):
                                return new TimeSpan(15, 45, 0);
                            case (SpecificTradingTime.Electronic_FB_Close):
                                return new TimeSpan(16, 0, 0);

                            case (SpecificTradingTime.Regular_Open):
                            case (SpecificTradingTime.DAY_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.Regular_Close):
                            case (SpecificTradingTime.DAY_Close):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificTradingTime.Regular_IB_Open):
                            case (SpecificTradingTime.DAY_IB_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.Regular_IB_Close):
                            case (SpecificTradingTime.DAY_IB_Close):
                                return new TimeSpan(9, 30, 0);
                            case (SpecificTradingTime.Regular_FB_Open):
                            case (SpecificTradingTime.DAY_FB_Open):
                                return new TimeSpan(14, 30, 0);
                            case (SpecificTradingTime.Regular_FB_Close):
                            case (SpecificTradingTime.DAY_FB_Close):
                                return new TimeSpan(15, 0, 0);


                            case (SpecificTradingTime.OVN_Open):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificTradingTime.OVN_Close):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.OVN_IB_Open):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificTradingTime.OVN_IB_Close):
                                return new TimeSpan(15, 15, 0);
                            case (SpecificTradingTime.OVN_FB_Open):
                                return new TimeSpan(14, 45, 0);
                            case (SpecificTradingTime.OVN_FB_Close):
                                return new TimeSpan(15, 0, 0);

                            default:
                                throw new Exception("The specific trading time name doesn't exists.");

                        }
                    }

                case (InstrumentCode.MES):
                    {
                        switch (tradingHourName)
                        {
                            case (SpecificTradingTime.Electronic_Open):
                                return new TimeSpan(17, 0, 0);
                            case (SpecificTradingTime.Electronic_Close):
                                return new TimeSpan(16, 0, 0);
                            case (SpecificTradingTime.Electronic_IB_Open):
                                return new TimeSpan(17, 0, 0);
                            case (SpecificTradingTime.Electronic_IB_Close):
                                return new TimeSpan(17, 15, 0);
                            case (SpecificTradingTime.Electronic_FB_Open):
                                return new TimeSpan(15, 45, 0);
                            case (SpecificTradingTime.Electronic_FB_Close):
                                return new TimeSpan(16, 0, 0);

                            case (SpecificTradingTime.Regular_Open):
                            case (SpecificTradingTime.DAY_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.Regular_Close):
                            case (SpecificTradingTime.DAY_Close):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificTradingTime.Regular_IB_Open):
                            case (SpecificTradingTime.DAY_IB_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.Regular_IB_Close):
                            case (SpecificTradingTime.DAY_IB_Close):
                                return new TimeSpan(9, 30, 0);
                            case (SpecificTradingTime.Regular_FB_Open):
                            case (SpecificTradingTime.DAY_FB_Open):
                                return new TimeSpan(14, 30, 0);
                            case (SpecificTradingTime.Regular_FB_Close):
                            case (SpecificTradingTime.DAY_FB_Close):
                                return new TimeSpan(15, 0, 0);


                            case (SpecificTradingTime.OVN_Open):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificTradingTime.OVN_Close):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.OVN_IB_Open):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificTradingTime.OVN_IB_Close):
                                return new TimeSpan(15, 15, 0);
                            case (SpecificTradingTime.OVN_FB_Open):
                                return new TimeSpan(14, 45, 0);
                            case (SpecificTradingTime.OVN_FB_Close):
                                return new TimeSpan(15, 0, 0);

                            default:
                                throw new Exception("The specific trading time name doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Returns <see cref="TradingTime"/> by general trading hour type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns><see cref="TradingTime"/>.</returns>
        public static TradingTime ToTradingHour(this GeneralTradingTime type)
        {
            return TradingTime.CreateTradingHourByType(type);
        }

        /// <summary>
        /// Returns <see cref="TradingTime"/> by specific trading hour type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns><see cref="TradingTime"/>.</returns>
        public static TradingTime ToTradingHour(this SpecificTradingTime type, InstrumentCode code = InstrumentCode.Default)
        {
            return TradingTime.CreateTradingHourByType(type, code);
        }

    }
}
