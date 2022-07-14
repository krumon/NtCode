using System;
using System.Linq;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="SessionHours"/> classes.
    /// </summary>
    public static class SessionHoursHelpers
    {

        /// <summary>
        /// Returns the unique code of the <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="tradingSession">The specific trading session.</param>
        /// <returns>String that represents the unique code of the <see cref="TradingSession"/>.</returns>
        /// <exception cref="Exception">The <see cref="TradingSession"/> doesn´t exists.</exception>
        public static string ToCode(this TradingSession tradingSession)
        {
            switch (tradingSession)
            {
                // SESSIONS
                case TradingSession.Electronic:
                    return "EL";
                case TradingSession.Regular:
                    return "RG";
                case TradingSession.OVN:
                    return "OV";
                case TradingSession.American:
                    return "AM";
                case TradingSession.AmericanAndEuropean:
                    return "AE";
                case TradingSession.Asian:
                    return "AS";
                case TradingSession.European:
                    return "AS";
                case TradingSession.American_RS:
                    return "AM-RS";
                case TradingSession.Asian_RS:
                    return "AS-RS";
                case TradingSession.American_RS_EXT:
                    return "RS-AET";
                case TradingSession.American_RS_EOD:
                    return "RS-EOD";
                case TradingSession.American_RS_NWD:
                    return "RS-NWD";

                default:
                    throw new Exception("The specific trading session doesn't exists.");
            }
        }

        /// <summary>
        /// Returns the unique code of the <see cref="TradingBalance"/>.
        /// </summary>
        /// <param name="tradingBalance">The specific trading balance.</param>
        /// <returns>String that represents the unique code of the <see cref="TradingBalance"/>.</returns>
        /// <exception cref="Exception">The <see cref="TradingBalance"/> doesn´t exists.</exception>
        public static string ToCode(this TradingBalance tradingBalance)
        {
            switch (tradingBalance)
            {
                // BALANCES
                case TradingBalance.Regular_IB:
                    return "RG-IB";
                case TradingBalance.Regular_BB:
                    return "RG-BB";
                case TradingBalance.Regular_FB:
                    return "RG-FB";

                case TradingBalance.OVN_IB:
                    return "OV-IB";
                case TradingBalance.OVN_BB:
                    return "OV-BB";
                case TradingBalance.OVN_FB:
                    return "OV-FB";

                case TradingBalance.American_IB:
                    return "AM-IB";
                case TradingBalance.American_BB:
                    return "AM-BB";
                case TradingBalance.American_FB:
                    return "AM-FB";

                case TradingBalance.AmericanAndEuropean_IB:
                    return "AE-IB";
                case TradingBalance.AmericanAndEuropean_BB:
                    return "AE-BB";
                case TradingBalance.AmericanAndEuropean_FB:
                    return "AE-FB";

                case TradingBalance.Asian_IB:
                    return "AS-IB";
                case TradingBalance.Asian_BB:
                    return "AS-BB";
                case TradingBalance.Asian_FB:
                    return "AS-FB";

                case TradingBalance.European_IB:
                    return "EU-IB";
                case TradingBalance.European_BB:
                    return "EU-BB";
                case TradingBalance.European_FB:
                    return "EU-FB";

                case TradingBalance.American_RS_IB:
                    return "AM-RS-IB";
                case TradingBalance.American_RS_BB:
                    return "AM-RS-BB";
                case TradingBalance.American_RS_FB:
                    return "AM-RS-FB";

                case TradingBalance.Asian_RS_IB:
                    return "AS-RS-IB";
                case TradingBalance.Asian_RS_BB:
                    return "AS-RS-BB";
                case TradingBalance.Asian_RS_FB:
                    return "AS-RS-FB";

                case TradingBalance.American_RS_NWD_IB:
                    return "AM-RS-NWD-IB";
                case TradingBalance.American_RS_NWD_BB:
                    return "AM-RS-NWD-BB";
                case TradingBalance.American_RS_NWD_FB:
                    return "AM-RS-NWD-FB";

                default:
                    throw new Exception("The specific trading balance doesn't exists.");
            }
        }

        /// <summary>
        /// Returns the description of the <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="tradingSession">The specific trading session.</param>
        /// <returns>String that represents the description of the <see cref="TradingSession"/>.</returns>
        /// <exception cref="Exception">The <see cref="TradingSession"/> doesn´t exists.</exception>
        public static string ToDescription(this TradingSession tradingSession)
        {
            switch (tradingSession)
            {
                // SESSIONS
                case TradingSession.Electronic:
                    return "Electronic Session.";
                case TradingSession.Regular:
                    return "Regular Session.";
                case TradingSession.OVN:
                    return "Overnight Session.";
                case TradingSession.American:
                    return "American Session.";
                case TradingSession.AmericanAndEuropean:
                    return "American and European Session.";
                case TradingSession.Asian:
                    return "Asian Session.";
                case TradingSession.European:
                    return "Asian Session.";
                case TradingSession.American_RS:
                    return "American Residual Session.";
                case TradingSession.Asian_RS:
                    return "Asian Residual Session.";
                case TradingSession.American_RS_EXT:
                    return "American Residual Extra time Session.";
                case TradingSession.American_RS_EOD:
                    return "American Residual End Of Day Session.";
                case TradingSession.American_RS_NWD:
                    return "American Residual New Day Session.";

                default:
                    throw new Exception("The specific trading doesn't exists.");
            }
        }

        /// <summary>
        /// Returns the description of the <see cref="TradingBalance"/>.
        /// </summary>
        /// <param name="tradingBalance">The specific trading balance.</param>
        /// <returns>String that represents the description of the <see cref="TradingBalance"/>.</returns>
        /// <exception cref="Exception">The <see cref="TradingBalance"/> doesn´t exists.</exception>
        public static string ToDescription(this TradingBalance tradingBalance)
        {
            switch (tradingBalance)
            {
                // BALANCES
                case TradingBalance.Regular_IB:
                    return "Regular Session Initial Balance.";
                case TradingBalance.Regular_BB:
                    return "Regular Session Between Balances.";
                case TradingBalance.Regular_FB:
                    return "Regular Session Final Balance.";

                case TradingBalance.OVN_IB:
                    return "Overnight Session Initial Balance.";
                case TradingBalance.OVN_BB:
                    return "Overnight Session Between Balances.";
                case TradingBalance.OVN_FB:
                    return "Overnight Session Final Balance.";

                case TradingBalance.American_IB:
                    return "American Session Initial Balance.";
                case TradingBalance.American_BB:
                    return "American Session Between Balances.";
                case TradingBalance.American_FB:
                    return "American Session Final Balance.";

                case TradingBalance.AmericanAndEuropean_IB:
                    return "American and European Session Initial Balance.";
                case TradingBalance.AmericanAndEuropean_BB:
                    return "American and European Session Between Balances.";
                case TradingBalance.AmericanAndEuropean_FB:
                    return "American and European Session Final Balance.";

                case TradingBalance.Asian_IB:
                    return "Asian Initial Balance.";
                case TradingBalance.Asian_BB:
                    return "Asian Between Balances.";
                case TradingBalance.Asian_FB:
                    return "Asian Final Balance.";

                case TradingBalance.European_IB:
                    return "European Initial Balance.";
                case TradingBalance.European_BB:
                    return "European Between Balances.";
                case TradingBalance.European_FB:
                    return "European Final Balance.";

                case TradingBalance.American_RS_IB:
                    return "American Residual Initial Balance.";
                case TradingBalance.American_RS_BB:
                    return "American Residual Between Balances.";
                case TradingBalance.American_RS_FB:
                    return "American Residual Initial Balance";

                case TradingBalance.Asian_RS_IB:
                    return "Asian Residual Initial Balance.";
                case TradingBalance.Asian_RS_BB:
                    return "Asian Residual Between Balances.";
                case TradingBalance.Asian_RS_FB:
                    return "Asian Residual Final Balance.";

                case TradingBalance.American_RS_NWD_IB:
                    return "American Residual New Day Initial Balance.";
                case TradingBalance.American_RS_NWD_BB:
                    return "American Residual New Day Between Balances.";
                case TradingBalance.American_RS_NWD_FB:
                    return "American Residual New Day Final Balance.";

                default:
                    throw new Exception("The specific session hours doesn't exists.");
            }
        }

        /// <summary>
        /// Converts the <see cref="TradingSession"/> to initial <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="tradingSession"></param>
        /// <returns>Initial <see cref="SessionTime"/> of the <see cref="TradingSession"/>.</returns>
        public static SessionTime ToBeginSessionTime(this TradingSession tradingSession, InstrumentCode instrumentCode = InstrumentCode.Default)
        {

            switch (tradingSession)
            {
                //case (TradingSession.Electronic):
                //    return SessionTime.CreateSessionTimeByType(tradingSession, tradingSession.ToTime ,instrumentCode.ToMarketExchange().ToElectronicInitialTime());

                //case (TradingSession.Regular):
                //    return TradingTime.Regular_Open.ToSessionTime(instrumentCode);
                //case (TradingSession.OVN):
                //    return TradingTime.OVN_Open.ToSessionTime(instrumentCode);

                case (TradingSession.American):
                    return TradingTime.American_Open.ToSessionTime();
                case (TradingSession.AmericanAndEuropean):
                    return TradingTime.AmericanAndEuropean_Open.ToSessionTime();
                case (TradingSession.Asian):
                    return TradingTime.Asian_Open.ToSessionTime();
                case (TradingSession.European):
                    return TradingTime.European_Open.ToSessionTime();
                case (TradingSession.American_RS):
                    return TradingTime.American_RS_Open.ToSessionTime();
                case (TradingSession.American_RS_EXT):
                    return TradingTime.American_RS_EXT_Open.ToSessionTime();
                case (TradingSession.American_RS_EOD):
                    return TradingTime.American_RS_EOD_Open.ToSessionTime();

                default:
                    throw new Exception("The specific session hours doesn't exists.");

            }

        }

        ///// <summary>
        ///// Converts the <see cref="TradingSession"/> to initial <see cref="SessionTime"/>.
        ///// </summary>
        ///// <param name="tradingSession"></param>
        ///// <returns>Initial <see cref="SessionTime"/> of the <see cref="TradingSession"/>.</returns>
        //public static SessionTime ToBeginSessionTime(this TradingSession tradingSession, InstrumentCode instrumentCode = InstrumentCode.Default)
        //{

        //    switch (tradingSession)
        //    {

        //        case (SpecificSessionHours.Electronic):
        //            return SpecificSessionTime.Electronic_Open.ToSessionTime(instrumentCode);

        //        case (SpecificSessionHours.Regular):
        //            return SpecificSessionTime.Regular_Open.ToSessionTime(instrumentCode);
        //        case (SpecificSessionHours.OVN):
        //            return SpecificSessionTime.OVN_Open.ToSessionTime(instrumentCode);

        //        case (SpecificSessionHours.American):
        //            return SpecificSessionTime.American_Open.ToSessionTime();
        //        case (SpecificSessionHours.American_IB):
        //            return SpecificSessionTime.American_IB_Open.ToSessionTime();
        //        case (SpecificSessionHours.American_BB):
        //            return SpecificSessionTime.American_BB_Open.ToSessionTime();
        //        case (SpecificSessionHours.American_FB):
        //            return SpecificSessionTime.American_FB_Open.ToSessionTime();

        //        case (SpecificSessionHours.AmericanAndEuropean):
        //            return SpecificSessionTime.AmericanAndEuropean_Open.ToSessionTime();
        //        case (SpecificSessionHours.AmericanAndEuropean_IB):
        //            return SpecificSessionTime.AmericanAndEuropean_IB_Open.ToSessionTime();
        //        case (SpecificSessionHours.AmericanAndEuropean_BB):
        //            return SpecificSessionTime.AmericanAndEuropean_BB_Open.ToSessionTime();
        //        case (SpecificSessionHours.AmericanAndEuropean_FB):
        //            return SpecificSessionTime.AmericanAndEuropean_FB_Open.ToSessionTime();

        //        case (SpecificSessionHours.Asian):
        //            return SpecificSessionTime.Asian_Open.ToSessionTime();
        //        case (SpecificSessionHours.Asian_IB):
        //            return SpecificSessionTime.Asian_IB_Open.ToSessionTime();
        //        case (SpecificSessionHours.Asian_BB):
        //            return SpecificSessionTime.Asian_BB_Open.ToSessionTime();
        //        case (SpecificSessionHours.Asian_FB):
        //            return SpecificSessionTime.Asian_FB_Open.ToSessionTime();

        //        case (SpecificSessionHours.European):
        //            return SpecificSessionTime.European_Open.ToSessionTime();
        //        case (SpecificSessionHours.European_IB):
        //            return SpecificSessionTime.European_IB_Open.ToSessionTime();
        //        case (SpecificSessionHours.European_BB):
        //            return SpecificSessionTime.European_BB_Open.ToSessionTime();
        //        case (SpecificSessionHours.European_FB):
        //            return SpecificSessionTime.European_FB_Open.ToSessionTime();

        //        case (SpecificSessionHours.American_RS):
        //            return SpecificSessionTime.American_RS_Open.ToSessionTime();
        //        case (SpecificSessionHours.American_RS_EXT):
        //            return SpecificSessionTime.American_RS_EXT_Open.ToSessionTime();
        //        case (SpecificSessionHours.American_RS_EOD):
        //            return SpecificSessionTime.American_RS_EOD_Open.ToSessionTime();
        //        case (SpecificSessionHours.American_RS_NWD_IB):
        //            return SpecificSessionTime.American_RS_NWD_IB_Open.ToSessionTime();

        //        default:
        //            throw new Exception("The specific session hours doesn't exists.");

        //    }

        //}

        /// <summary>
        /// Method to convert the <see cref="TradingSession"/> to final <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="specificSessionHours"></param>
        /// <returns>Final <see cref="SessionTime"/> of the <see cref="TradingSession"/>.</returns>
        public static SessionTime ToEndSessionTime(this TradingSession specificSessionHours, InstrumentCode instrumentCode = InstrumentCode.Default)
        {

            //switch (instrumentCode)
            //{
            //    case (InstrumentCode.Default):
            //    case (InstrumentCode.MES):
            //        {
            //            switch (specificSessionHours)
            //            {

            //                case (SpecificSessionHours.Electronic):
            //                    return SpecificSessionTime.Electronic_Close.ToSessionTime(instrumentCode);

            //                case (SpecificSessionHours.Regular):
            //                    return SpecificSessionTime.Regular_Close.ToSessionTime(instrumentCode);

            //                case (SpecificSessionHours.OVN):
            //                    return SpecificSessionTime.OVN_Close.ToSessionTime(instrumentCode);

            //                case (SpecificSessionHours.American):
            //                    return SpecificSessionTime.American_Close.ToSessionTime();
            //                case (SpecificSessionHours.American_IB):
            //                    return SpecificSessionTime.American_IB_Close.ToSessionTime();
            //                case (SpecificSessionHours.American_BB):
            //                    return SpecificSessionTime.American_BB_Close.ToSessionTime();
            //                case (SpecificSessionHours.American_FB):
            //                    return SpecificSessionTime.American_FB_Close.ToSessionTime();

            //                case (SpecificSessionHours.AmericanAndEuropean):
            //                    return SpecificSessionTime.AmericanAndEuropean_Close.ToSessionTime();
            //                case (SpecificSessionHours.AmericanAndEuropean_IB):
            //                    return SpecificSessionTime.AmericanAndEuropean_IB_Close.ToSessionTime();
            //                case (SpecificSessionHours.AmericanAndEuropean_BB):
            //                    return SpecificSessionTime.AmericanAndEuropean_BB_Close.ToSessionTime();
            //                case (SpecificSessionHours.AmericanAndEuropean_FB):
            //                    return SpecificSessionTime.AmericanAndEuropean_FB_Close.ToSessionTime();

            //                case (SpecificSessionHours.Asian):
            //                    return SpecificSessionTime.Asian_Close.ToSessionTime();
            //                case (SpecificSessionHours.Asian_IB):
            //                    return SpecificSessionTime.Asian_IB_Close.ToSessionTime();
            //                case (SpecificSessionHours.Asian_BB):
            //                    return SpecificSessionTime.Asian_BB_Close.ToSessionTime();
            //                case (SpecificSessionHours.Asian_FB):
            //                    return SpecificSessionTime.Asian_FB_Close.ToSessionTime();

            //                case (SpecificSessionHours.European):
            //                    return SpecificSessionTime.European_Close.ToSessionTime();
            //                case (SpecificSessionHours.European_IB):
            //                    return SpecificSessionTime.European_IB_Close.ToSessionTime();
            //                case (SpecificSessionHours.European_BB):
            //                    return SpecificSessionTime.European_BB_Close.ToSessionTime();
            //                case (SpecificSessionHours.European_FB):
            //                    return SpecificSessionTime.European_FB_Close.ToSessionTime();

            //                case (SpecificSessionHours.American_RS):
            //                    return SpecificSessionTime.American_RS_Close.ToSessionTime();
            //                case (SpecificSessionHours.American_RS_EXT):
            //                    return SpecificSessionTime.American_RS_EXT_Close.ToSessionTime();
            //                case (SpecificSessionHours.American_RS_EOD):
            //                    return SpecificSessionTime.American_RS_EOD_Close.ToSessionTime();
            //                case (SpecificSessionHours.American_RS_NWD_IB):
            //                    return SpecificSessionTime.American_RS_NWD_IB_Close.ToSessionTime();

            //                default:
            //                    throw new Exception("The specific session hours doesn't exists.");

            //            }
            //        }

            //    default:
            //        throw new Exception("The instrument code doesn't exists.");
            //}

            throw new Exception("The instrument code doesn't exists.");

        }

        public static SessionHours ToSessionHours(this TradingSession type)
        {
            return SessionHours.CreateSessionHoursByType(type);
        }

        public static TradingSession[] ToArray(this TradingSession type)
        {
            return Enum.GetValues(typeof(TradingSession)).Cast<TradingSession>().ToArray();
        }
    }
}
