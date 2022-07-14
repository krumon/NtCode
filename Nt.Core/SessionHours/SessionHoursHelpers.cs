using System;
using System.Linq;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="SpecificSessionHours"/> enum.
    /// </summary>
    public static class SessionHoursHelpers
    {

        /// <summary>
        /// Returns the unique code of the <see cref="SpecificSessionHours"/>.
        /// </summary>
        /// <param name="specificSessionHours">The specific session hours.</param>
        /// <returns>String that represents the unique code of the <see cref="SessionHours"/>.</returns>
        /// <exception cref="Exception">The <see cref="SpecificSessionHours"/> doesn´t exists.</exception>
        public static string ToCode(this SpecificSessionHours specificSessionHours)
        {
            switch (specificSessionHours)
            {
                // SESSIONS
                case SpecificSessionHours.Electronic:
                    return "EL";
                case SpecificSessionHours.Regular:
                    return "RG";
                case SpecificSessionHours.OVN:
                    return "OV";
                case SpecificSessionHours.American:
                    return "AM";
                case SpecificSessionHours.AmericanAndEuropean:
                    return "AE";
                case SpecificSessionHours.Asian:
                    return "AS";
                case SpecificSessionHours.European:
                    return "AS";
                case SpecificSessionHours.American_RS:
                    return "AM-RS";
                case SpecificSessionHours.Asian_RS:
                    return "AS-RS";
                case SpecificSessionHours.American_RS_EXT:
                    return "RS-AET";
                case SpecificSessionHours.American_RS_EOD:
                    return "RS-EOD";
                case SpecificSessionHours.American_RS_NWD:
                    return "RS-NWD";

                // BALANCES
                case SpecificSessionHours.Regular_IB:
                    return "RG-IB";
                case SpecificSessionHours.Regular_BB:
                    return "RG-BB";
                case SpecificSessionHours.Regular_FB:
                    return "RG-FB";

                case SpecificSessionHours.OVN_IB:
                    return "OV-IB";
                case SpecificSessionHours.OVN_BB:
                    return "OV-BB";
                case SpecificSessionHours.OVN_FB:
                    return "OV-FB";

                case SpecificSessionHours.American_IB:
                    return "AM-IB";
                case SpecificSessionHours.American_BB:
                    return "AM-BB";
                case SpecificSessionHours.American_FB:
                    return "AM-FB";

                case SpecificSessionHours.AmericanAndEuropean_IB:
                    return "AE-IB";
                case SpecificSessionHours.AmericanAndEuropean_BB:
                    return "AE-BB";
                case SpecificSessionHours.AmericanAndEuropean_FB:
                    return "AE-FB";

                case SpecificSessionHours.Asian_IB:
                    return "AS-IB";
                case SpecificSessionHours.Asian_BB:
                    return "AS-BB";
                case SpecificSessionHours.Asian_FB:
                    return "AS-FB";

                case SpecificSessionHours.European_IB:
                    return "EU-IB";
                case SpecificSessionHours.European_BB:
                    return "EU-BB";
                case SpecificSessionHours.European_FB:
                    return "EU-FB";

                case SpecificSessionHours.American_RS_IB:
                    return "AM-RS-IB";
                case SpecificSessionHours.American_RS_BB:
                    return "AM-RS-BB";
                case SpecificSessionHours.American_RS_FB:
                    return "AM-RS-FB";

                case SpecificSessionHours.Asian_RS_IB:
                    return "AS-RS-IB";
                case SpecificSessionHours.Asian_RS_BB:
                    return "AS-RS-BB";
                case SpecificSessionHours.Asian_RS_FB:
                    return "AS-RS-FB";

                case SpecificSessionHours.American_RS_NWD_IB:
                    return "AM-RS-NWD-IB";
                case SpecificSessionHours.American_RS_NWD_BB:
                    return "AM-RS-NWD-BB";
                case SpecificSessionHours.American_RS_NWD_FB:
                    return "AM-RS-NWD-FB";

                default:
                    throw new Exception("The specific session hours doesn't exists.");
            }
        }

        /// <summary>
        /// Returns the description of the <see cref="SpecificSessionHours"/>.
        /// </summary>
        /// <param name="specificSessionHours">The specific session hours.</param>
        /// <returns>String that represents the description of the <see cref="SpecificSessionHours"/>.</returns>
        /// <exception cref="Exception">The <see cref="SpecificSessionHours"/> doesn´t exists.</exception>
        public static string ToDescription(this SpecificSessionHours specificSessionHours)
        {
            switch (specificSessionHours)
            {
                // SESSIONS
                case SpecificSessionHours.Electronic:
                    return "Electronic Session.";
                case SpecificSessionHours.Regular:
                    return "Regular Session.";
                case SpecificSessionHours.OVN:
                    return "Overnight Session.";
                case SpecificSessionHours.American:
                    return "American Session.";
                case SpecificSessionHours.AmericanAndEuropean:
                    return "American and European Session.";
                case SpecificSessionHours.Asian:
                    return "Asian Session.";
                case SpecificSessionHours.European:
                    return "Asian Session.";
                case SpecificSessionHours.American_RS:
                    return "American Residual Session.";
                case SpecificSessionHours.Asian_RS:
                    return "Asian Residual Session.";
                case SpecificSessionHours.American_RS_EXT:
                    return "American Residual Extra time Session.";
                case SpecificSessionHours.American_RS_EOD:
                    return "American Residual End Of Day Session.";
                case SpecificSessionHours.American_RS_NWD:
                    return "American Residual New Day Session.";

                // BALANCES
                case SpecificSessionHours.Regular_IB:
                    return "Regular Session Initial Balance.";
                case SpecificSessionHours.Regular_BB:
                    return "Regular Session Between Balances.";
                case SpecificSessionHours.Regular_FB:
                    return "Regular Session Final Balance.";

                case SpecificSessionHours.OVN_IB:
                    return "Overnight Session Initial Balance.";
                case SpecificSessionHours.OVN_BB:
                    return "Overnight Session Between Balances.";
                case SpecificSessionHours.OVN_FB:
                    return "Overnight Session Final Balance.";

                case SpecificSessionHours.American_IB:
                    return "American Session Initial Balance.";
                case SpecificSessionHours.American_BB:
                    return "American Session Between Balances.";
                case SpecificSessionHours.American_FB:
                    return "American Session Final Balance.";

                case SpecificSessionHours.AmericanAndEuropean_IB:
                    return "American and European Session Initial Balance.";
                case SpecificSessionHours.AmericanAndEuropean_BB:
                    return "American and European Session Between Balances.";
                case SpecificSessionHours.AmericanAndEuropean_FB:
                    return "American and European Session Final Balance.";

                case SpecificSessionHours.Asian_IB:
                    return "Asian Initial Balance.";
                case SpecificSessionHours.Asian_BB:
                    return "Asian Between Balances.";
                case SpecificSessionHours.Asian_FB:
                    return "Asian Final Balance.";

                case SpecificSessionHours.European_IB:
                    return "European Initial Balance.";
                case SpecificSessionHours.European_BB:
                    return "European Between Balances.";
                case SpecificSessionHours.European_FB:
                    return "European Final Balance.";

                case SpecificSessionHours.American_RS_IB:
                    return "American Residual Initial Balance.";
                case SpecificSessionHours.American_RS_BB:
                    return "American Residual Between Balances.";
                case SpecificSessionHours.American_RS_FB:
                    return "American Residual Initial Balance";

                case SpecificSessionHours.Asian_RS_IB:
                    return "Asian Residual Initial Balance.";
                case SpecificSessionHours.Asian_RS_BB:
                    return "Asian Residual Between Balances.";
                case SpecificSessionHours.Asian_RS_FB:
                    return "Asian Residual Final Balance.";

                case SpecificSessionHours.American_RS_NWD_IB:
                    return "American Residual New Day Initial Balance.";
                case SpecificSessionHours.American_RS_NWD_BB:
                    return "American Residual New Day Between Balances.";
                case SpecificSessionHours.American_RS_NWD_FB:
                    return "American Residual New Day Final Balance.";

                default:
                    throw new Exception("The specific session hours doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="SpecificSessionHours"/> to initial <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="specificSessionHours"></param>
        /// <returns>Initial <see cref="SessionTime"/> of the <see cref="SpecificSessionHours"/>.</returns>
        public static SessionTime ToBeginSessionTime(this SpecificSessionHours specificSessionHours, InstrumentCode instrumentCode = InstrumentCode.Default)
        {

            //switch (instrumentCode)
            //{
            //    case (InstrumentCode.Default):
            //    case (InstrumentCode.MES):
            //        {
            //            switch (specificSessionHours)
            //            {

            //                case (SpecificSessionHours.Electronic):
            //                    return SpecificSessionTime.Electronic_Open.ToSessionTime(instrumentCode);

            //                case (SpecificSessionHours.Regular):
            //                    return SpecificSessionTime.Regular_Open.ToSessionTime(instrumentCode);
            //                case (SpecificSessionHours.OVN):
            //                    return SpecificSessionTime.OVN_Open.ToSessionTime(instrumentCode);

            //                case (SpecificSessionHours.American):
            //                    return SpecificSessionTime.American_Open.ToSessionTime();
            //                case (SpecificSessionHours.American_IB):
            //                    return SpecificSessionTime.American_IB_Open.ToSessionTime();
            //                case (SpecificSessionHours.American_BB):
            //                    return SpecificSessionTime.American_BB_Open.ToSessionTime();
            //                case (SpecificSessionHours.American_FB):
            //                    return SpecificSessionTime.American_FB_Open.ToSessionTime();

            //                case (SpecificSessionHours.AmericanAndEuropean):
            //                    return SpecificSessionTime.AmericanAndEuropean_Open.ToSessionTime();
            //                case (SpecificSessionHours.AmericanAndEuropean_IB):
            //                    return SpecificSessionTime.AmericanAndEuropean_IB_Open.ToSessionTime();
            //                case (SpecificSessionHours.AmericanAndEuropean_BB):
            //                    return SpecificSessionTime.AmericanAndEuropean_BB_Open.ToSessionTime();
            //                case (SpecificSessionHours.AmericanAndEuropean_FB):
            //                    return SpecificSessionTime.AmericanAndEuropean_FB_Open.ToSessionTime();

            //                case (SpecificSessionHours.Asian):
            //                    return SpecificSessionTime.Asian_Open.ToSessionTime();
            //                case (SpecificSessionHours.Asian_IB):
            //                    return SpecificSessionTime.Asian_IB_Open.ToSessionTime();
            //                case (SpecificSessionHours.Asian_BB):
            //                    return SpecificSessionTime.Asian_BB_Open.ToSessionTime();
            //                case (SpecificSessionHours.Asian_FB):
            //                    return SpecificSessionTime.Asian_FB_Open.ToSessionTime();

            //                case (SpecificSessionHours.European):
            //                    return SpecificSessionTime.European_Open.ToSessionTime();
            //                case (SpecificSessionHours.European_IB):
            //                    return SpecificSessionTime.European_IB_Open.ToSessionTime();
            //                case (SpecificSessionHours.European_BB):
            //                    return SpecificSessionTime.European_BB_Open.ToSessionTime();
            //                case (SpecificSessionHours.European_FB):
            //                    return SpecificSessionTime.European_FB_Open.ToSessionTime();

            //                case (SpecificSessionHours.American_RS):
            //                    return SpecificSessionTime.American_RS_Open.ToSessionTime();
            //                case (SpecificSessionHours.American_RS_EXT):
            //                    return SpecificSessionTime.American_RS_EXT_Open.ToSessionTime();
            //                case (SpecificSessionHours.American_RS_EOD):
            //                    return SpecificSessionTime.American_RS_EOD_Open.ToSessionTime();
            //                case (SpecificSessionHours.American_RS_NWD_IB):
            //                    return SpecificSessionTime.American_RS_NWD_IB_Open.ToSessionTime();

            //                default:
            //                    throw new Exception("The specific session hours doesn't exists.");

            //            }
            //        }

            //    default:
            //        throw new Exception("The instrument code doesn't exists.");
            //}

            throw new Exception("The instrument code doesn't exists.");
        }

        /// <summary>
        /// Method to convert the <see cref="SpecificSessionHours"/> to final <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="specificSessionHours"></param>
        /// <returns>Final <see cref="SessionTime"/> of the <see cref="SpecificSessionHours"/>.</returns>
        public static SessionTime ToEndSessionTime(this SpecificSessionHours specificSessionHours, InstrumentCode instrumentCode = InstrumentCode.Default)
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

        public static SessionHours ToSessionHours(this SpecificSessionHours type)
        {
            return SessionHours.CreateSessionHoursByType(type);
        }

        public static SpecificSessionHours[] ToArray(this SpecificSessionHours type)
        {
            return Enum.GetValues(typeof(SpecificSessionHours)).Cast<SpecificSessionHours>().ToArray();
        }
    }
}
