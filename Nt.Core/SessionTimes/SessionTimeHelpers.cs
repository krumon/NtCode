using System;

namespace NtCore
{

    /// <summary>
    /// Helper methods of trading hours classes.
    /// </summary>
    public static class SessionTimeHelpers
    {

        /// <summary>
        /// Converts the <see cref="TradingTime"/> to unique code.
        /// </summary>
        /// <param name="tradingTime">The specific session time.</param>
        /// <returns></returns>
        public static string ToCode(this TradingTime tradingTime)
        {
            switch (tradingTime)
            {
                // MAIN SESSIONS
                case TradingTime.Electronic_Open:
                    return "EL-O";
                case TradingTime.Electronic_Close:
                    return "EL-C";
                //case SpecificSessionTime.Regular_Open:
                //    return "RG-O";
                //case SpecificSessionTime.Regular_Close:
                //    return "RG-C";
                //case SpecificSessionTime.OVN_Open:
                //    return "OV-O";
                //case SpecificSessionTime.OVN_Close:
                //    return "OV-C";

                // MAJOR SESSIONS
                case TradingTime.Asian_Open:
                    return "AS-O";
                case TradingTime.Asian_Close:
                    return "AS-C";
                case TradingTime.Asian_RS_Open:
                    return "AS-RS-O";
                case TradingTime.Asian_RS_Close:
                    return "AS-RS-C";
                case TradingTime.European_Open:
                    return "AM-O";
                case TradingTime.European_Close:
                    return "AM-C";
                case TradingTime.AmericanAndEuropean_Open:
                    return "AE-O";
                case TradingTime.AmericanAndEuropean_Close:
                    return "AE-C";
                case TradingTime.American_Open:
                    return "AM-O";
                case TradingTime.American_Close:
                    return "AM-C";
                case TradingTime.American_RS_Open:
                    return "AM-RS-O";
                case TradingTime.American_RS_Close:
                    return "AM-RS-C";

                // MINOR SESSIONS
                case TradingTime.American_RS_EXT_Open:
                    return "AM-RS-EXT-O";
                case TradingTime.American_RS_EXT_Close:
                    return "AM-RS-EXT-C";
                case TradingTime.American_RS_EOD_Open:
                    return "AM-RS-EOD-O";
                case TradingTime.American_RS_EOD_Close:
                    return "AM-RS-EOD-C";
                case TradingTime.American_RS_NWD_Open:
                    return "AM-RS-NWD-O";
                case TradingTime.American_RS_NWD_Close:
                    return "AM-RS-NWD-C";
                
                // BALANCES SESSIONS
                // -----------------

                // Asian balances
                //case SpecificSessionTime.Asian_IB_Open:
                //    return "AS-IB-O";
                //case SpecificSessionTime.Asian_IB_Close:
                //    return "AS-IB-C";
                //case SpecificSessionTime.Asian_BB_Open:
                //    return "AS-BB-O";
                //case SpecificSessionTime.Asian_BB_Close:
                //    return "AS-BB-C";
                //case SpecificSessionTime.Asian_FB_Open:
                //    return "AS-FB-O";
                //case SpecificSessionTime.Asian_FB_Close:
                //    return "AS-FB-C";

                //// Asian Residual balances
                //case SpecificSessionTime.Asian_RS_IB_Open:
                //    return "AS-RS-IB-O";
                //case SpecificSessionTime.Asian_RS_IB_Close:
                //    return "AS-RS-IB-C";
                //case SpecificSessionTime.Asian_RS_BB_Open:
                //    return "AS-RS-BB-O";
                //case SpecificSessionTime.Asian_RS_BB_Close:
                //    return "AS-RS-BB-C";
                //case SpecificSessionTime.Asian_RS_FB_Open:
                //    return "AS-RS-FB-O";
                //case SpecificSessionTime.Asian_RS_FB_Close:
                //    return "AS-RS-FB-C";

                //// American balances.
                //case SpecificSessionTime.European_IB_Open:
                //    return "EU-IB-O";     
                //case SpecificSessionTime.European_IB_Close:
                //    return "EU-IB-C";     
                //case SpecificSessionTime.European_BB_Open:
                //    return "EU-BB-O";     
                //case SpecificSessionTime.European_BB_Close:
                //    return "EU-BB-C";     
                //case SpecificSessionTime.European_FB_Open:
                //    return "EU-FB-O";     
                //case SpecificSessionTime.European_FB_Close:
                //    return "EU-FB-C";

                //// American and European balances.
                //case SpecificSessionTime.AmericanAndEuropean_IB_Open:
                //    return "AE-IB-O";
                //case SpecificSessionTime.AmericanAndEuropean_IB_Close:
                //    return "AE-IB-C";
                //case SpecificSessionTime.AmericanAndEuropean_BB_Open:
                //    return "AE-BB-O";
                //case SpecificSessionTime.AmericanAndEuropean_BB_Close:
                //    return "AE-BB-C";
                //case SpecificSessionTime.AmericanAndEuropean_FB_Open:
                //    return "AE-FB-O";
                //case SpecificSessionTime.AmericanAndEuropean_FB_Close:
                //    return "AE-FB-C";

                //// American balances.
                //case SpecificSessionTime.American_IB_Open:
                //    return "AM-IB-O";
                //case SpecificSessionTime.American_IB_Close:
                //    return "AM-IB-C";
                //case SpecificSessionTime.American_BB_Open:
                //    return "AM-BB-O";
                //case SpecificSessionTime.American_BB_Close:
                //    return "AM-BB-C";
                //case SpecificSessionTime.American_FB_Open:
                //    return "AM-FB-O";
                //case SpecificSessionTime.American_FB_Close:
                //    return "AM-FB-C";

                //// American Residual new day balances
                //case SpecificSessionTime.American_RS_NWD_IB_Open:
                //    return "AM-RS-NWD-IB-O";
                //case SpecificSessionTime.American_RS_NWD_IB_Close:
                //    return "AM-RS-NWD-IB-C";
                //case SpecificSessionTime.American_RS_NWD_BB_Open:
                //    return "AM-RS-NWD-BB-O";
                //case SpecificSessionTime.American_RS_NWD_BB_Close:
                //    return "AM-RS-NWD-BB-C";
                //case SpecificSessionTime.American_RS_NWD_FB_Open:
                //    return "AM-RS-NWD-FB-O";
                //case SpecificSessionTime.American_RS_NWD_FB_Close:
                //    return "AM-RS-NWD-FB-C";

                default:
                    throw new Exception("The trading time doesn't exists.");
            }
        }

        /// <summary>
        /// Converts the <see cref="TradingTime"/> to description.
        /// </summary>
        /// <param name="tradingTime">The specific session time.</param>
        /// <returns></returns>
        public static string ToDescription(this TradingTime tradingTime)
        {
            switch (tradingTime)
            {
                // MAIN SESSIONS
                case TradingTime.Electronic_Open:
                    return "Electronic session open time.";
                case TradingTime.Electronic_Close:
                    return "Electronic session close time.";
                //case SpecificSessionTime.Regular_Open:
                //    return "Regular session open time.";
                //case SpecificSessionTime.Regular_Close:
                //    return "Regular session close time.";
                //case SpecificSessionTime.OVN_Open:
                //    return "Overnight session open time.";
                //case SpecificSessionTime.OVN_Close:
                //    return "Overnight session close time.";

                // MAJOR SESSIONS
                case TradingTime.Asian_Open:
                    return "Asian session open time.";
                case TradingTime.Asian_Close:
                    return "Asian session close time.";
                case TradingTime.Asian_RS_Open:
                    return "Asian residual session open time.";
                case TradingTime.Asian_RS_Close:
                    return "Asian residual session close time.";
                case TradingTime.European_Open:
                    return "European session open time.";
                case TradingTime.European_Close:
                    return "European session close time.";
                case TradingTime.AmericanAndEuropean_Open:
                    return "American and European session open time.";
                case TradingTime.AmericanAndEuropean_Close:
                    return "American and European session close time.";
                case TradingTime.American_Open:
                    return "American session open time.";
                case TradingTime.American_Close:
                    return "American session close time.";
                case TradingTime.American_RS_Open:
                    return "American residual session open time.";
                case TradingTime.American_RS_Close:
                    return "American residual session close time.";

                // MINOR SESSIONS
                case TradingTime.American_RS_EXT_Open:
                    return "Open time of American extra time.";
                case TradingTime.American_RS_EXT_Close:
                    return "Close time of American extra time.";
                case TradingTime.American_RS_EOD_Open:
                    return "End of day open time.";
                case TradingTime.American_RS_EOD_Close:
                    return "End of day close time.";
                case TradingTime.American_RS_NWD_Open:
                    return "New day open time.";
                case TradingTime.American_RS_NWD_Close:
                    return "New day close time.";

                // BALANCES SESSIONS
                // -----------------

                //// Asian balances
                //case SpecificSessionTime.Asian_IB_Open:
                //    return "Open time of Asian initial balance.";
                //case SpecificSessionTime.Asian_IB_Close:
                //    return "Close time of Asian initial balance.";
                //case SpecificSessionTime.Asian_BB_Open:
                //    return "Open time between balance of Asian session.";
                //case SpecificSessionTime.Asian_BB_Close:
                //    return "Close time between balance of Asian session.";
                //case SpecificSessionTime.Asian_FB_Open:
                //    return "Open time of Asian final balance.";
                //case SpecificSessionTime.Asian_FB_Close:
                //    return "Close time of Asian final balance.";

                //// Asian Residual new day balances
                //case SpecificSessionTime.Asian_RS_IB_Open:
                //    return "Open time of Asian residual intial balance.";
                //case SpecificSessionTime.Asian_RS_IB_Close:
                //    return "Close time of Asian residual initial balance.";
                //case SpecificSessionTime.Asian_RS_BB_Open:
                //    return "Open time between balance of Asian residual session.";
                //case SpecificSessionTime.Asian_RS_BB_Close:
                //    return "Close time between balances of Asian residual session balance.";
                //case SpecificSessionTime.Asian_RS_FB_Open:
                //    return "Open time of Asian residual final balance.";
                //case SpecificSessionTime.Asian_RS_FB_Close:
                //    return "Close time of Asian residual final balance.";

                //// European balances
                //case SpecificSessionTime.European_IB_Open:
                //    return "Open time of European_IB_Open initial balance.";
                //case SpecificSessionTime.European_IB_Close:
                //    return "Close time of European_IB_Open initial balance.";
                //case SpecificSessionTime.European_BB_Open:
                //    return "Open time between balance of European session.";
                //case SpecificSessionTime.European_BB_Close:
                //    return "Close time between balance of European session.";
                //case SpecificSessionTime.European_FB_Open:
                //    return "Open time of European final balance.";
                //case SpecificSessionTime.European_FB_Close:
                //    return "Close time of European final balance.";

                //// American and European balances
                //case SpecificSessionTime.AmericanAndEuropean_IB_Open:
                //    return "Open time of American and European initial balance.";
                //case SpecificSessionTime.AmericanAndEuropean_IB_Close:
                //    return "Close time of American and European initial balance.";
                //case SpecificSessionTime.AmericanAndEuropean_BB_Open:
                //    return "Open time between balance of American and European session.";
                //case SpecificSessionTime.AmericanAndEuropean_BB_Close:
                //    return "Close time between balance of American and European session.";
                //case SpecificSessionTime.AmericanAndEuropean_FB_Open:
                //    return "Open time of American and European final balance.";
                //case SpecificSessionTime.AmericanAndEuropean_FB_Close:
                //    return "Close time of American and European final balance.";

                //// American balances
                //case SpecificSessionTime.American_IB_Open:
                //    return "Open time of American initial balance.";
                //case SpecificSessionTime.American_IB_Close:
                //    return "Close time of American initial balance.";
                //case SpecificSessionTime.American_BB_Open:
                //    return "Open time between balance of American session.";
                //case SpecificSessionTime.American_BB_Close:
                //    return "Close time between balance of American session.";
                //case SpecificSessionTime.American_FB_Open:
                //    return "Open time of American final balance.";
                //case SpecificSessionTime.American_FB_Close:
                //    return "Close time of American final balance.";

                //// American Residual new day balances
                //case SpecificSessionTime.American_RS_NWD_IB_Open:
                //    return "Open time of new day intial balance.";
                //case SpecificSessionTime.American_RS_NWD_IB_Close:
                //    return "Close time of new day initial balance.";
                //case SpecificSessionTime.American_RS_NWD_BB_Open:
                //    return "Open time between balance of new day session.";
                //case SpecificSessionTime.American_RS_NWD_BB_Close:
                //    return "Close time between balances of new day session balance.";
                //case SpecificSessionTime.American_RS_NWD_FB_Open:
                //    return "Open time of new day final balance.";
                //case SpecificSessionTime.American_RS_NWD_FB_Close:
                //    return "Close time of new day final balance.";

                default:
                    throw new Exception("The trading time doesn't exists.");
            }
        }

        /// <summary>
        /// Converts the <see cref="TradingTime"/> to <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="tradingTime"></param>
        /// <returns><see cref="TimeZoneInfo"/> of the <see cref="TradingTime"/>.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (tradingTime)
                        {

                            case (TradingTime.Electronic_Open):
                            case (TradingTime.Electronic_Close):
                            //case (SpecificSessionTime.Regular_Open):
                            //case (SpecificSessionTime.Regular_Close):
                            //case (SpecificSessionTime.OVN_Open):
                            //case (SpecificSessionTime.OVN_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            case (TradingTime.American_RS_Open):
                            case (TradingTime.American_RS_Close):
                            case (TradingTime.American_RS_EXT_Open):
                            case (TradingTime.American_RS_EXT_Close):
                            case (TradingTime.American_RS_EOD_Open):
                            case (TradingTime.American_RS_EOD_Close):
                            case (TradingTime.American_RS_NWD_Open):
                            case (TradingTime.American_RS_NWD_Close):
                            //case (SpecificSessionTime.American_RS_NWD_IB_Open):
                            //case (SpecificSessionTime.American_RS_NWD_IB_Close):
                            //case (SpecificSessionTime.American_RS_NWD_BB_Open):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            //case (SpecificSessionTime.American_RS_NWD_BB_Close):
                            //case (SpecificSessionTime.American_RS_NWD_FB_Open):
                            //case (SpecificSessionTime.American_RS_NWD_FB_Close):
                            //    return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

                            case (TradingTime.American_Open):
                            case (TradingTime.American_Close):
                            //case (SpecificSessionTime.American_IB_Open):
                            //case (SpecificSessionTime.American_IB_Close):
                            //case (SpecificSessionTime.American_BB_Open):
                            //case (SpecificSessionTime.American_BB_Close):
                            //case (SpecificSessionTime.American_FB_Open):
                            //case (SpecificSessionTime.American_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            case (TradingTime.AmericanAndEuropean_Open):
                            //case (SpecificSessionTime.AmericanAndEuropean_IB_Open):
                            //case (SpecificSessionTime.AmericanAndEuropean_IB_Close):
                            //case (SpecificSessionTime.AmericanAndEuropean_BB_Open):
                                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                            case (TradingTime.AmericanAndEuropean_Close):
                            //case (SpecificSessionTime.AmericanAndEuropean_BB_Close):
                            //case (SpecificSessionTime.AmericanAndEuropean_FB_Open):
                            //case (SpecificSessionTime.AmericanAndEuropean_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                            case (TradingTime.Asian_Open):
                            case (TradingTime.Asian_Close):
                            //case (SpecificSessionTime.Asian_IB_Open):
                            //case (SpecificSessionTime.Asian_IB_Close):
                            //case (SpecificSessionTime.Asian_BB_Open):
                            //case (SpecificSessionTime.Asian_BB_Close):
                            //case (SpecificSessionTime.Asian_FB_Open):
                            //case (SpecificSessionTime.Asian_FB_Close):
                            //case (SpecificSessionTime.Asian_RS_Open):
                            //case (SpecificSessionTime.Asian_RS_IB_Open):
                            //case (SpecificSessionTime.Asian_RS_IB_Close):
                            //case (SpecificSessionTime.Asian_RS_BB_Open):

                                return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

                            case (TradingTime.European_Open):
                            case (TradingTime.European_Close):
                            //case (SpecificSessionTime.European_IB_Open):
                            //case (SpecificSessionTime.European_IB_Close):
                            //case (SpecificSessionTime.European_BB_Open):
                            //case (SpecificSessionTime.European_BB_Close):
                            //case (SpecificSessionTime.European_FB_Open):
                            //case (SpecificSessionTime.European_FB_Close):
                            //case (SpecificSessionTime.Asian_RS_Close):
                            //case (SpecificSessionTime.Asian_RS_BB_Close):
                            //case (SpecificSessionTime.Asian_RS_FB_Open):
                            //case (SpecificSessionTime.Asian_RS_FB_Close):
                                return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                            case (TradingTime.Custom):
                                throw new Exception("the convert is not possible, the session time is a custom value.");

                            default:
                                throw new Exception("The trading time doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        /// <summary>
        /// Converts the <see cref="TradingTime"/> to <see cref="Time"/>.
        /// </summary>
        /// <param name="tradingTime"></param>
        /// <returns><see cref="TimeSpan"/> of the <see cref="TradingTime"/>.</returns>
        public static TimeSpan ToTime(this TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (tradingTime)
                        {
                            // Main hours of sessions
                            case (TradingTime.Electronic_Open):
                                return instrumentCode.ToMarketExchange().ToElectronicInitialTime();
                            case (TradingTime.Electronic_Close):
                                return instrumentCode.ToMarketExchange().ToElectronicFinalTime();
                            //case (SpecificSessionTime.Regular_Open):
                            //    return instrumentCode.ToMarketExchange().ToRegularInitialTime();
                            //case (SpecificSessionTime.Regular_Close):
                            //    return instrumentCode.ToMarketExchange().ToRegularFinalTime();

                            //// Overnight hours
                            //case (SpecificSessionTime.OVN_Open):
                            //    return instrumentCode.ToMarketExchange().ToRegularFinalTime() + TimeSpan.FromSeconds(1);
                            //case (SpecificSessionTime.OVN_Close):
                            //    return instrumentCode.ToMarketExchange().ToRegularInitialTime() + TimeSpan.FromSeconds(-1);

                            //
                            //MAJOR SESSIONS
                            //

                            // Hours of major sessions
                            case (TradingTime.Asian_Open):
                                return new TimeSpan(9, 0, 0);
                            case (TradingTime.Asian_Close):
                                return new TimeSpan(15, 0, 0);
                            case (TradingTime.European_Open):
                                return new TimeSpan(8, 0, 0);
                            case (TradingTime.European_Close):
                                return new TimeSpan(16, 30, 0);
                            case (TradingTime.AmericanAndEuropean_Open):
                                return new TimeSpan(8, 30, 0);
                            case (TradingTime.AmericanAndEuropean_Close):
                                return new TimeSpan(10, 30, 0);
                            case (TradingTime.American_Open):
                                return new TimeSpan(8, 30, 0);
                            case (TradingTime.American_Close):
                                return new TimeSpan(15, 0, 0);
                            case (TradingTime.American_RS_Open):
                                return new TimeSpan(15, 0, 0);
                            case (TradingTime.American_RS_Close):
                                return TradingTime.Asian_Open.ToTime();

                            //
                            // RESIDUAL SESSIONS
                            //

                            // AMERICAN RESIDUAL SESSION
                            // ASIAN RESIDUAL SESSION


                            //
                            // MINOR SESSIONS
                            //

                            // American Residual minor sessions.
                            case (TradingTime.American_RS_EXT_Open):
                                return TradingTime.American_Close.ToTime() + TimeSpan.FromSeconds(1);
                            case (TradingTime.American_RS_EXT_Close):
                                return TradingTime.American_Close.ToTime() + TimeSpan.FromMinutes(15);
                            case (TradingTime.American_RS_EOD_Open):
                                return TradingTime.American_RS_EXT_Close.ToTime() + TimeSpan.FromSeconds(1);
                            case (TradingTime.American_RS_EOD_Close):
                                return TradingTime.Electronic_Close.ToTime();
                            //case (SpecificSessionTime.American_RS_NWD_IB_Open):
                            //    return SpecificSessionTime.Electronic_Open.ToTimeSpan();
                            //case (SpecificSessionTime.American_RS_NWD_IB_Close):
                            //    return SpecificSessionTime.Electronic_Open.ToTimeSpan() + TimeSpan.FromMinutes(5);
                            //case (SpecificSessionTime.American_RS_NWD_BB_Open):
                            //    return SpecificSessionTime.Electronic_Open.ToTimeSpan() + TimeSpan.FromMinutes(5) + TimeSpan.FromSeconds(1);
                            //case (SpecificSessionTime.American_RS_NWD_BB_Close):
                            //    return SpecificSessionTime.Asian_Open.ToTimeSpan() + TimeSpan.FromMinutes(-5);
                            //case (SpecificSessionTime.American_RS_NWD_FB_Open):
                            //    return SpecificSessionTime.Asian_Open.ToTimeSpan() + TimeSpan.FromMinutes(-5) + TimeSpan.FromSeconds(1);
                            //case (SpecificSessionTime.American_RS_NWD_FB_Close):
                            //    return SpecificSessionTime.Asian_Open.ToTimeSpan();
                                                       
                            //// Asian minor sessions
                            //case (SpecificSessionTime.Asian_IB_Open):
                            //    return new TimeSpan(9, 0, 0);
                            //case (SpecificSessionTime.Asian_IB_Close):
                            //    return new TimeSpan(9, 15, 0);
                            //case (SpecificSessionTime.Asian_BB_Open):
                            //    return SpecificSessionTime.Asian_IB_Close.ToTimeSpan();
                            //case (SpecificSessionTime.Asian_BB_Close):
                            //    return SpecificSessionTime.Asian_FB_Open.ToTimeSpan();
                            //case (SpecificSessionTime.Asian_FB_Open):
                            //    return new TimeSpan(14, 45, 0);
                            //case (SpecificSessionTime.Asian_FB_Close):
                            //    return new TimeSpan(15, 0, 0);

                            //// American minor sessions
                            //case (SpecificSessionTime.American_IB_Open):
                            //    return new TimeSpan(8, 30, 0);
                            //case (SpecificSessionTime.American_IB_Close):
                            //    return new TimeSpan(9, 30, 0);
                            //case (SpecificSessionTime.American_BB_Open):
                            //    return SpecificSessionTime.American_IB_Close.ToTimeSpan();
                            //case (SpecificSessionTime.American_BB_Close):
                            //    return SpecificSessionTime.American_FB_Open.ToTimeSpan();
                            //case (SpecificSessionTime.American_FB_Open):
                            //    return new TimeSpan(14, 30, 0);
                            //case (SpecificSessionTime.American_FB_Close):
                            //    return new TimeSpan(15, 0, 0);

                            //// American and european minor sessions
                            //case (SpecificSessionTime.AmericanAndEuropean_IB_Open):
                            //    return new TimeSpan(8, 30, 0);
                            //case (SpecificSessionTime.AmericanAndEuropean_IB_Close):
                            //    return new TimeSpan(9, 30, 0);
                            //case (SpecificSessionTime.AmericanAndEuropean_BB_Open):
                            //    return SpecificSessionTime.AmericanAndEuropean_IB_Close.ToTimeSpan();
                            //case (SpecificSessionTime.AmericanAndEuropean_BB_Close):
                            //    return SpecificSessionTime.AmericanAndEuropean_FB_Open.ToTimeSpan();
                            //case (SpecificSessionTime.AmericanAndEuropean_FB_Open):
                            //    return new TimeSpan(10, 15, 0);
                            //case (SpecificSessionTime.AmericanAndEuropean_FB_Close):
                            //    return new TimeSpan(10, 30, 0);
                            
                            //// Eurepan minor sessions
                            //case (SpecificSessionTime.European_IB_Open):
                            //    return new TimeSpan(8, 0, 0);
                            //case (SpecificSessionTime.European_IB_Close):
                            //    return new TimeSpan(8, 15, 0);
                            //case (SpecificSessionTime.European_BB_Open):
                            //    return SpecificSessionTime.European_IB_Close.ToTimeSpan();
                            //case (SpecificSessionTime.European_BB_Close):
                            //    return SpecificSessionTime.European_FB_Open.ToTimeSpan();
                            //case (SpecificSessionTime.European_FB_Open):
                            //    return new TimeSpan(16, 15, 0);
                            //case (SpecificSessionTime.European_FB_Close):
                            //    return new TimeSpan(16, 30, 0);

                            case (TradingTime.Custom):
                                throw new Exception("the convert is not possible, the trading time is a custom value.");

                            default:
                                throw new Exception("The trading time doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Returns <see cref="SessionTime"/> by <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="tradingTime"></param>
        /// <returns><see cref="SessionTime"/>.</returns>
        public static SessionTime ToSessionTime(this TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            return SessionTime.CreateSessionTimeByType(tradingTime, instrumentCode);
        }

        ///// <summary>
        ///// Returns <see cref="SessionTime"/> by <see cref="TradingTime"/>.
        ///// </summary>
        ///// <param name="specificSessionTime"></param>
        ///// <returns><see cref="SessionTime"/>.</returns>
        //public static SessionTime ToSessionTime(this TradingTime specificSessionTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        //{
        //    return SessionTime.CreateSessionTimeByType(specificSessionTime, instrumentCode);
        //}
    }
}
