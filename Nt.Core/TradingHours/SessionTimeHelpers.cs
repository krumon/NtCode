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
        /// <param name="specificTradingTime">The specific session time.</param>
        /// <returns></returns>
        public static string ToCode(this SpecificSessionTime specificTradingTime)
        {
            // TODO: Make the SessionTime to unique code converter.
            return specificTradingTime.ToString();
        }

        /// <summary>
        /// Converts the <see cref="SpecificSessionTime"/> to description.
        /// </summary>
        /// <param name="specificTradingTime">The specific session time.</param>
        /// <returns></returns>
        public static string ToDescription(this SpecificSessionTime specificTradingTime)
        {
            // TODO: Make the SessionTime to description converter.
            return specificTradingTime.ToString();
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
                            case (SpecificSessionTime.Residual_NDB_Open):
                                return new TimeSpan(17, 0, 0);
                            case (SpecificSessionTime.Residual_NDB_Close):
                                return SpecificSessionTime.Asian_Open.ToTimeSpan();

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

                            case (SpecificSessionTime.Asian_Open):
                                return new TimeSpan(9, 0, 0);
                            case (SpecificSessionTime.Asian_Close):
                                return new TimeSpan(15, 0, 0);
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
