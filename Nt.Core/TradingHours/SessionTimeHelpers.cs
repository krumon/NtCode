using System;

namespace NtCore
{

    /// <summary>
    /// Helper methods of trading hours classes.
    /// </summary>
    public static class SessionTimeHelpers
    {

        /// <summary>
        /// Converts the <see cref="SpecificSessionTime"/> to unique code.
        /// </summary>
        /// <param name="specificSessionTime">The specific session time.</param>
        /// <returns></returns>
        public static string ToCode(this SpecificSessionTime specificSessionTime)
        {
            switch (specificSessionTime)
            {
                case SpecificSessionTime.Electronic_Open:
                    return "EL-O";
                case SpecificSessionTime.Electronic_Close:
                    return "EL-C";
                case SpecificSessionTime.Regular_Open:
                    return "RG-O";
                case SpecificSessionTime.Regular_Close:
                    return "RG-C";
                case SpecificSessionTime.OVN_Open:
                    return "OV-O";
                case SpecificSessionTime.OVN_Close:
                    return "OV-C";
                case SpecificSessionTime.American_Open:
                    return "AM-O";
                case SpecificSessionTime.American_Close:
                    return "AM-C";
                case SpecificSessionTime.AmericanAndEuropean_Open:
                    return "AE-O";
                case SpecificSessionTime.AmericanAndEuropean_Close:
                    return "AE-C";
                case SpecificSessionTime.Asian_Open:
                    return "AS-O";
                case SpecificSessionTime.Asian_Close:
                    return "AS-C";
                case SpecificSessionTime.Residual_Open:
                    return "RS-O";
                case SpecificSessionTime.Residual_Close:
                    return "RS-C";

                case SpecificSessionTime.AmericanAndEuropean_IB_Open:
                    return "AE-IBO";
                case SpecificSessionTime.AmericanAndEuropean_IB_Close:
                    return "AE-IBC";
                case SpecificSessionTime.AmericanAndEuropean_BB_Open:
                    return "AE-BBO";
                case SpecificSessionTime.AmericanAndEuropean_BB_Close:
                    return "AE-BBC";
                case SpecificSessionTime.AmericanAndEuropean_FB_Open:
                    return "AE-FBO";
                case SpecificSessionTime.AmericanAndEuropean_FB_Close:
                    return "AE-FBC";

                case SpecificSessionTime.American_IB_Open:
                    return "AM-IBO";
                case SpecificSessionTime.American_IB_Close:
                    return "AM-IBC";
                case SpecificSessionTime.American_BB_Open:
                    return "AM-BBO";
                case SpecificSessionTime.American_BB_Close:
                    return "AM-BBC";
                case SpecificSessionTime.American_FB_Open:
                    return "AM-FBO";
                case SpecificSessionTime.American_FB_Close:
                    return "AM-FBC";

                case SpecificSessionTime.Residual_AET_Open:
                    return "RS-AET-O";
                case SpecificSessionTime.Residual_AET_Close:
                    return "RS-AET-C";
                case SpecificSessionTime.Residual_EOD_Open:
                    return "RS-EOD-O";
                case SpecificSessionTime.Residual_EOD_Close:
                    return "RS-EOD-C";
                case SpecificSessionTime.Residual_NDB_Open:
                    return "RS-NDB-O";
                case SpecificSessionTime.Residual_NDB_Close:
                    return "RS-NDB-C";

                case SpecificSessionTime.Asian_IB_Open:
                    return "AS-IBO";
                case SpecificSessionTime.Asian_IB_Close:
                    return "AS-IBC";
                case SpecificSessionTime.Asian_BB_Open:
                    return "AS-BBO";
                case SpecificSessionTime.Asian_BB_Close:
                    return "AS-BBC";
                case SpecificSessionTime.Asian_FB_Open:
                    return "AS-FBO";
                case SpecificSessionTime.Asian_FB_Close:
                    return "AS-FBC";

                default:
                    throw new Exception("The specific session time doesn't exists.");
            }
        }

        /// <summary>
        /// Converts the <see cref="SpecificSessionTime"/> to description.
        /// </summary>
        /// <param name="specificSessionTime">The specific session time.</param>
        /// <returns></returns>
        public static string ToDescription(this SpecificSessionTime specificSessionTime)
        {
            switch (specificSessionTime)
            {
                case SpecificSessionTime.Electronic_Open:
                    return "Electronic session open time.";
                case SpecificSessionTime.Electronic_Close:
                    return "Electronic session close time.";
                case SpecificSessionTime.Regular_Open:
                    return "Regular session open time.";
                case SpecificSessionTime.Regular_Close:
                    return "Regular session close time.";
                case SpecificSessionTime.OVN_Open:
                    return "Overnight session open time.";
                case SpecificSessionTime.OVN_Close:
                    return "Overnight session close time.";
                case SpecificSessionTime.American_Open:
                    return "American session open time.";
                case SpecificSessionTime.American_Close:
                    return "American session close time.";
                case SpecificSessionTime.AmericanAndEuropean_Open:
                    return "American and European session open time.";
                case SpecificSessionTime.AmericanAndEuropean_Close:
                    return "American and European session close time.";
                case SpecificSessionTime.Asian_Open:
                    return "Asian session open time.";
                case SpecificSessionTime.Asian_Close:
                    return "Asian session close time.";
                case SpecificSessionTime.Residual_Open:
                    return "Residual session open time.";
                case SpecificSessionTime.Residual_Close:
                    return "Residual session close time.";

                case SpecificSessionTime.AmericanAndEuropean_IB_Open:
                    return "Open time of American and European initial balance.";
                case SpecificSessionTime.AmericanAndEuropean_IB_Close:
                    return "Close time of American and European initial balance.";
                case SpecificSessionTime.AmericanAndEuropean_BB_Open:
                    return "Open time between balance of American and European session.";
                case SpecificSessionTime.AmericanAndEuropean_BB_Close:
                    return "Close time between balance of American and European session.";
                case SpecificSessionTime.AmericanAndEuropean_FB_Open:
                    return "Open time of American and European final balance.";
                case SpecificSessionTime.AmericanAndEuropean_FB_Close:
                    return "Close time of American and European final balance.";

                case SpecificSessionTime.American_IB_Open:
                    return "Open time of American initial balance.";
                case SpecificSessionTime.American_IB_Close:
                    return "Close time of American initial balance.";
                case SpecificSessionTime.American_BB_Open:
                    return "Open time between balance of American session.";
                case SpecificSessionTime.American_BB_Close:
                    return "Close time between balance of American session.";
                case SpecificSessionTime.American_FB_Open:
                    return "Open time of American final balance.";
                case SpecificSessionTime.American_FB_Close:
                    return "Close time of American final balance.";

                case SpecificSessionTime.Residual_AET_Open:
                    return "Open time of American extra time.";
                case SpecificSessionTime.Residual_AET_Close:
                    return "Close time of American extra time.";
                case SpecificSessionTime.Residual_EOD_Open:
                    return "Open time of end of day session.";
                case SpecificSessionTime.Residual_EOD_Close:
                    return "Close time of end of day session.";
                case SpecificSessionTime.Residual_NDB_Open:
                    return "Open time of new day balance session.";
                case SpecificSessionTime.Residual_NDB_Close:
                    return "Close time of new day balance session.";

                case SpecificSessionTime.Asian_IB_Open:
                    return "Open time of Asian initial balance.";
                case SpecificSessionTime.Asian_IB_Close:
                    return "Close time of Asian initial balance.";
                case SpecificSessionTime.Asian_BB_Open:
                    return "Open time between balance of Asian session.";
                case SpecificSessionTime.Asian_BB_Close:
                    return "Close time between balance of Asian session.";
                case SpecificSessionTime.Asian_FB_Open:
                    return "Open time of Asian final balance.";
                case SpecificSessionTime.Asian_FB_Close:
                    return "Close time of Asian final balance.";

                default:
                    throw new Exception("The specific session time doesn't exists.");
            }
        }

        /// <summary>
        /// Converts the <see cref="SpecificSessionTime"/> to <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="specificSessionTime"></param>
        /// <returns><see cref="TimeZoneInfo"/> of the <see cref="SpecificSessionTime"/>.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this SpecificSessionTime specificSessionTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificSessionTime)
                        {
                            case (SpecificSessionTime.Electronic_Open):
                            case (SpecificSessionTime.Electronic_Close):
                            case (SpecificSessionTime.Regular_Open):
                            case (SpecificSessionTime.Regular_Close):
                            case (SpecificSessionTime.OVN_Open):
                            case (SpecificSessionTime.OVN_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            case (SpecificSessionTime.Residual_Open):
                            case (SpecificSessionTime.Residual_Close):
                            case (SpecificSessionTime.Residual_AET_Open):
                            case (SpecificSessionTime.Residual_AET_Close):
                            case (SpecificSessionTime.Residual_EOD_Open):
                            case (SpecificSessionTime.Residual_EOD_Close):
                            case (SpecificSessionTime.Residual_NDB_Open):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            case (SpecificSessionTime.Residual_NDB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

                            case (SpecificSessionTime.American_Open):
                            case (SpecificSessionTime.American_Close):
                            case (SpecificSessionTime.American_IB_Open):
                            case (SpecificSessionTime.American_IB_Close):
                            case (SpecificSessionTime.American_BB_Open):
                            case (SpecificSessionTime.American_BB_Close):
                            case (SpecificSessionTime.American_FB_Open):
                            case (SpecificSessionTime.American_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            case (SpecificSessionTime.AmericanAndEuropean_Open):
                            case (SpecificSessionTime.AmericanAndEuropean_IB_Open):
                            case (SpecificSessionTime.AmericanAndEuropean_IB_Close):
                            case (SpecificSessionTime.AmericanAndEuropean_BB_Open):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            case (SpecificSessionTime.AmericanAndEuropean_Close):
                            case (SpecificSessionTime.AmericanAndEuropean_BB_Close):
                            case (SpecificSessionTime.AmericanAndEuropean_FB_Open):
                            case (SpecificSessionTime.AmericanAndEuropean_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                            case (SpecificSessionTime.Asian_Open):
                            case (SpecificSessionTime.Asian_Close):
                            case (SpecificSessionTime.Asian_IB_Open):
                            case (SpecificSessionTime.Asian_IB_Close):
                            case (SpecificSessionTime.Asian_BB_Open):
                            case (SpecificSessionTime.Asian_BB_Close):
                            case (SpecificSessionTime.Asian_FB_Open):
                            case (SpecificSessionTime.Asian_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

                            case (SpecificSessionTime.European_Open):
                            case (SpecificSessionTime.European_Close):
                            case (SpecificSessionTime.European_IB_Open):
                            case (SpecificSessionTime.European_IB_Close):
                            case (SpecificSessionTime.European_BB_Open):
                            case (SpecificSessionTime.European_BB_Close):
                            case (SpecificSessionTime.European_FB_Open):
                            case (SpecificSessionTime.European_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                            case (SpecificSessionTime.Custom):
                                throw new Exception("the convert is not possible, the session time is a custom value.");

                            default:
                                throw new Exception("The specific session time doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        /// <summary>
        /// Converts the <see cref="SpecificSessionTime"/> to <see cref="Time"/>.
        /// </summary>
        /// <param name="specificSessionTime"></param>
        /// <returns><see cref="TimeSpan"/> of the <see cref="SpecificSessionTime"/>.</returns>
        public static TimeSpan ToTimeSpan(this SpecificSessionTime specificSessionTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificSessionTime)
                        {

                            case (SpecificSessionTime.Electronic_Open):
                                return new TimeSpan(17, 0, 0);
                            case (SpecificSessionTime.Residual_NDB_Open):
                                return new TimeSpan(17, 0, 0);
                            case (SpecificSessionTime.Residual_NDB_Close):
                                return SpecificSessionTime.Asian_Open.ToTimeSpan();
                            case (SpecificSessionTime.Asian_Open):
                                return new TimeSpan(9, 0, 0);
                            case (SpecificSessionTime.Asian_IB_Open):
                                return new TimeSpan(9, 0, 0);
                            case (SpecificSessionTime.Asian_IB_Close):
                                return new TimeSpan(9, 15, 0);
                            case (SpecificSessionTime.Asian_BB_Open):
                                return SpecificSessionTime.Asian_IB_Close.ToTimeSpan();
                            case (SpecificSessionTime.Asian_BB_Close):
                                return SpecificSessionTime.Asian_FB_Open.ToTimeSpan();
                            case (SpecificSessionTime.Asian_FB_Open):
                                return new TimeSpan(14, 45, 0);
                            case (SpecificSessionTime.Asian_FB_Close):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificSessionTime.Asian_Close):
                                return new TimeSpan(15, 0, 0);



                            case (SpecificSessionTime.Electronic_Close):
                                return new TimeSpan(16, 0, 0);

                            case (SpecificSessionTime.Regular_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificSessionTime.Regular_Close):
                                return new TimeSpan(15, 0, 0);

                            case (SpecificSessionTime.OVN_Open):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificSessionTime.OVN_Close):
                                return new TimeSpan(8, 30, 0);

                            case (SpecificSessionTime.Residual_Open):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificSessionTime.Residual_Close):
                                return SpecificSessionTime.Asian_Open.ToTimeSpan();
                            case (SpecificSessionTime.Residual_AET_Open):
                                return new TimeSpan(15, 0, 0); ;
                            case (SpecificSessionTime.Residual_AET_Close):
                                return new TimeSpan(15, 15, 0);
                            case (SpecificSessionTime.Residual_EOD_Open):
                                return new TimeSpan(15, 30, 0);
                            case (SpecificSessionTime.Residual_EOD_Close):
                                return new TimeSpan(16, 0, 0);

                            case (SpecificSessionTime.American_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificSessionTime.American_Close):
                                return new TimeSpan(15, 0, 0);
                            case (SpecificSessionTime.American_IB_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificSessionTime.American_IB_Close):
                                return new TimeSpan(9, 30, 0);
                            case (SpecificSessionTime.American_BB_Open):
                                return SpecificSessionTime.American_IB_Close.ToTimeSpan();
                            case (SpecificSessionTime.American_BB_Close):
                                return SpecificSessionTime.American_FB_Open.ToTimeSpan();
                            case (SpecificSessionTime.American_FB_Open):
                                return new TimeSpan(14, 30, 0);
                            case (SpecificSessionTime.American_FB_Close):
                                return new TimeSpan(15, 0, 0);

                            case (SpecificSessionTime.AmericanAndEuropean_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificSessionTime.AmericanAndEuropean_Close):
                                return new TimeSpan(10, 30, 0);
                            case (SpecificSessionTime.AmericanAndEuropean_IB_Open):
                                return new TimeSpan(8, 30, 0);
                            case (SpecificSessionTime.AmericanAndEuropean_IB_Close):
                                return new TimeSpan(9, 30, 0);
                            case (SpecificSessionTime.AmericanAndEuropean_BB_Open):
                                return SpecificSessionTime.AmericanAndEuropean_IB_Close.ToTimeSpan();
                            case (SpecificSessionTime.AmericanAndEuropean_BB_Close):
                                return SpecificSessionTime.AmericanAndEuropean_FB_Open.ToTimeSpan();
                            case (SpecificSessionTime.AmericanAndEuropean_FB_Open):
                                return new TimeSpan(10, 15, 0);
                            case (SpecificSessionTime.AmericanAndEuropean_FB_Close):
                                return new TimeSpan(10, 30, 0);

                            case (SpecificSessionTime.European_Open):
                                return new TimeSpan(8, 0, 0);
                            case (SpecificSessionTime.European_Close):
                                return new TimeSpan(16, 30, 0);
                            case (SpecificSessionTime.European_IB_Open):
                                return new TimeSpan(8, 0, 0);
                            case (SpecificSessionTime.European_IB_Close):
                                return new TimeSpan(8, 15, 0);
                            case (SpecificSessionTime.European_BB_Open):
                                return SpecificSessionTime.European_IB_Close.ToTimeSpan();
                            case (SpecificSessionTime.European_BB_Close):
                                return SpecificSessionTime.European_FB_Open.ToTimeSpan();
                            case (SpecificSessionTime.European_FB_Open):
                                return new TimeSpan(16, 15, 0);
                            case (SpecificSessionTime.European_FB_Close):
                                return new TimeSpan(16, 30, 0);

                            case (SpecificSessionTime.Custom):
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
        /// Returns <see cref="SessionTime"/> by <see cref="SpecificSessionTime"/>.
        /// </summary>
        /// <param name="specificSessionTime"></param>
        /// <returns><see cref="SessionTime"/>.</returns>
        public static SessionTime ToSessionTime(this SpecificSessionTime specificSessionTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            return SessionTime.CreateSessionTimeByType(specificSessionTime, instrumentCode);
        }
    }
}
