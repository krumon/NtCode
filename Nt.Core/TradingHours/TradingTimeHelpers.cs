using System;

namespace NtCore
{

    /// <summary>
    /// Helper methods of trading hours classes.
    /// </summary>
    public static class TradingTimeHelpers
    {

        public static string ToCode(this SpecificTradingTime specificTradingTime)
        {
            return specificTradingTime.ToString();
        }

        public static string ToDescription(this SpecificTradingTime specificTradingTime)
        {
            return specificTradingTime.ToString();
        }

        /// <summary>
        /// Method to convert the <see cref="SpecificTradingTime"/> to <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="specificTradingTime"></param>
        /// <returns><see cref="TimeZoneInfo"/> of the <see cref="SpecificTradingTime"/>.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this SpecificTradingTime specificTradingTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificTradingTime)
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

                            case (SpecificTradingTime.American_Open):
                            case (SpecificTradingTime.American_Close):
                            case (SpecificTradingTime.American_IB_Open):
                            case (SpecificTradingTime.American_IB_Close):
                            case (SpecificTradingTime.American_FB_Open):
                            case (SpecificTradingTime.American_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            case (SpecificTradingTime.AmericanAndEuropean_Open):
                            case (SpecificTradingTime.AmericanAndEuropean_Close):
                            case (SpecificTradingTime.AmericanAndEuropean_IB_Open):
                            case (SpecificTradingTime.AmericanAndEuropean_IB_Close):
                            case (SpecificTradingTime.AmericanAndEuropean_FB_Open):
                            case (SpecificTradingTime.AmericanAndEuropean_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            case (SpecificTradingTime.Asian_Open):
                            case (SpecificTradingTime.Asian_Close):
                            case (SpecificTradingTime.Asian_IB_Open):
                            case (SpecificTradingTime.Asian_IB_Close):
                            case (SpecificTradingTime.Asian_FB_Open):
                            case (SpecificTradingTime.Asian_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

                            case (SpecificTradingTime.European_Open):
                            case (SpecificTradingTime.European_Close):
                            case (SpecificTradingTime.European_IB_Open):
                            case (SpecificTradingTime.European_IB_Close):
                            case (SpecificTradingTime.European_FB_Open):
                            case (SpecificTradingTime.European_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                            case (SpecificTradingTime.Custom):
                                throw new Exception("the convert is not possible, the trading time is a custom value.");

                            default:
                                throw new Exception("The specific trading time name doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        /// <summary>
        /// Method to convert the <see cref="SpecificTradingTime"/> to <see cref="Time"/>.
        /// </summary>
        /// <param name="specificTradingTime"></param>
        /// <returns><see cref="TimeSpan"/> of the <see cref="SpecificTradingTime"/>.</returns>
        public static TimeSpan ToTimeSpan(this SpecificTradingTime specificTradingTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificTradingTime)
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

                            case (SpecificTradingTime.American_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.American_Close):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificTradingTime.American_IB_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.American_IB_Close):
                                return new TimeSpan(9, 30, 0);
                            case (SpecificTradingTime.American_FB_Open):
                                return new TimeSpan(14, 30, 0);
                            case (SpecificTradingTime.American_FB_Close):
                                return new TimeSpan(15, 0, 0);

                            case (SpecificTradingTime.AmericanAndEuropean_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.AmericanAndEuropean_Close):
                                return new TimeSpan(10, 30, 0);
                            case (SpecificTradingTime.AmericanAndEuropean_IB_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificTradingTime.AmericanAndEuropean_IB_Close):
                                return new TimeSpan(9, 30, 0);
                            case (SpecificTradingTime.AmericanAndEuropean_FB_Open):
                                return new TimeSpan(10, 15, 0);
                            case (SpecificTradingTime.AmericanAndEuropean_FB_Close):
                                return new TimeSpan(10, 30, 0);

                            case (SpecificTradingTime.Asian_Open):
                                return new TimeSpan(9, 0, 0);
                            case (SpecificTradingTime.Asian_Close):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificTradingTime.Asian_IB_Open):
                                return new TimeSpan(9, 0, 0);
                            case (SpecificTradingTime.Asian_IB_Close):
                                return new TimeSpan(9, 15, 0);
                            case (SpecificTradingTime.Asian_FB_Open):
                                return new TimeSpan(14, 45, 0);
                            case (SpecificTradingTime.Asian_FB_Close):
                                return new TimeSpan(15, 0, 0);

                            case (SpecificTradingTime.European_Open):
                                return new TimeSpan(8, 0, 0);
                            case (SpecificTradingTime.European_Close):
                                return new TimeSpan(16, 30, 0);
                            case (SpecificTradingTime.European_IB_Open):
                                return new TimeSpan(8, 0, 0);
                            case (SpecificTradingTime.European_IB_Close):
                                return new TimeSpan(8, 15, 0);
                            case (SpecificTradingTime.European_FB_Open):
                                return new TimeSpan(16, 15, 0);
                            case (SpecificTradingTime.European_FB_Close):
                                return new TimeSpan(16, 30, 0);

                            case (SpecificTradingTime.Custom):
                                throw new Exception("the convert is not possible, the trading time is a custom value.");

                            default:
                                throw new Exception("The specific trading time doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Returns <see cref="TradingTime"/> by specific trading hour type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns><see cref="TradingTime"/>.</returns>
        public static TradingTime ToTradingTime(this SpecificTradingTime type, InstrumentCode code = InstrumentCode.Default)
        {
            return TradingTime.CreateTradingTimeByType(type, code);
        }
    }
}
