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
                case SpecificSessionHours.Residual:
                    return "RS";
                
                case SpecificSessionHours.AmericanAndEuropean_IB:
                    return "AE-IB";
                case SpecificSessionHours.AmericanAndEuropean_BB:
                    return "AE-BB";
                case SpecificSessionHours.AmericanAndEuropean_FB:
                    return "AE-FB";

                case SpecificSessionHours.American_IB:
                    return "AM-IB";
                case SpecificSessionHours.American_BB:
                    return "AM-BB";
                case SpecificSessionHours.American_FB:
                    return "AM-FB";

                case SpecificSessionHours.Residual_AET:
                    return "RS-AET";
                case SpecificSessionHours.Residual_EOD:
                    return "RS-EOD";
                case SpecificSessionHours.Residual_NDB:
                    return "RS-NDB";

                case SpecificSessionHours.Asian_IB:
                    return "AS-IB";
                case SpecificSessionHours.Asian_BB:
                    return "AS-BB";
                case SpecificSessionHours.Asian_FB:
                    return "AS-FB";

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
                case SpecificSessionHours.Electronic:
                    return "Electronic session.";
                case SpecificSessionHours.Regular:
                    return "Regular session.";
                case SpecificSessionHours.OVN:
                    return "Overnight session.";
                case SpecificSessionHours.American:
                    return "American session.";
                case SpecificSessionHours.AmericanAndEuropean:
                    return "American and European session.";
                case SpecificSessionHours.Asian:
                    return "Asian session.";
                case SpecificSessionHours.Residual:
                    return "Residual session.";
                
                case SpecificSessionHours.AmericanAndEuropean_IB:
                    return "Initial balance of the American and European session.";
                case SpecificSessionHours.AmericanAndEuropean_BB:
                    return "American and European session between balances.";
                case SpecificSessionHours.AmericanAndEuropean_FB:
                    return "Final balance of the American and European session.";

                case SpecificSessionHours.American_IB:
                    return "Initial balance of the American session.";
                case SpecificSessionHours.American_BB:
                    return "American session between balances.";
                case SpecificSessionHours.American_FB:
                    return "Final balance of the American session.";

                case SpecificSessionHours.Residual_AET:
                    return "Extra time of the American session.";
                case SpecificSessionHours.Residual_EOD:
                    return "End of day session.";
                case SpecificSessionHours.Residual_NDB:
                    return "Initial balance of the new day session.";

                case SpecificSessionHours.Asian_IB:
                    return "Initial balance of the Asian session.";
                case SpecificSessionHours.Asian_BB:
                    return "Asian session between balances.";
                case SpecificSessionHours.Asian_FB:
                    return "Final balance of the Asian session.";

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

            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificSessionHours)
                        {

                            case (SpecificSessionHours.Electronic):
                                return SpecificSessionTime.Electronic_Open.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.Regular):
                                return SpecificSessionTime.Regular_Open.ToSessionTime(instrumentCode);
                            case (SpecificSessionHours.OVN):
                                return SpecificSessionTime.OVN_Open.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.American):
                                return SpecificSessionTime.American_Open.ToSessionTime();
                            case (SpecificSessionHours.American_IB):
                                return SpecificSessionTime.American_IB_Open.ToSessionTime();
                            case (SpecificSessionHours.American_BB):
                                return SpecificSessionTime.American_BB_Open.ToSessionTime();
                            case (SpecificSessionHours.American_FB):
                                return SpecificSessionTime.American_FB_Open.ToSessionTime();

                            case (SpecificSessionHours.AmericanAndEuropean):
                                return SpecificSessionTime.AmericanAndEuropean_Open.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_IB):
                                return SpecificSessionTime.AmericanAndEuropean_IB_Open.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_BB):
                                return SpecificSessionTime.AmericanAndEuropean_BB_Open.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_FB):
                                return SpecificSessionTime.AmericanAndEuropean_FB_Open.ToSessionTime();

                            case (SpecificSessionHours.Asian):
                                return SpecificSessionTime.Asian_Open.ToSessionTime();
                            case (SpecificSessionHours.Asian_IB):
                                return SpecificSessionTime.Asian_IB_Open.ToSessionTime();
                            case (SpecificSessionHours.Asian_BB):
                                return SpecificSessionTime.Asian_BB_Open.ToSessionTime();
                            case (SpecificSessionHours.Asian_FB):
                                return SpecificSessionTime.Asian_FB_Open.ToSessionTime();

                            case (SpecificSessionHours.European):
                                return SpecificSessionTime.European_Open.ToSessionTime();
                            case (SpecificSessionHours.European_IB):
                                return SpecificSessionTime.European_IB_Open.ToSessionTime();
                            case (SpecificSessionHours.European_BB):
                                return SpecificSessionTime.European_BB_Open.ToSessionTime();
                            case (SpecificSessionHours.European_FB):
                                return SpecificSessionTime.European_FB_Open.ToSessionTime();

                            case (SpecificSessionHours.Residual):
                                return SpecificSessionTime.Residual_Open.ToSessionTime();
                            case (SpecificSessionHours.Residual_AET):
                                return SpecificSessionTime.Residual_AET_Open.ToSessionTime();
                            case (SpecificSessionHours.Residual_EOD):
                                return SpecificSessionTime.Residual_EOD_Open.ToSessionTime();
                            case (SpecificSessionHours.Residual_NDB):
                                return SpecificSessionTime.Residual_NDB_Open.ToSessionTime();

                            default:
                                throw new Exception("The specific session hours doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="SpecificSessionHours"/> to final <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="specificSessionHours"></param>
        /// <returns>Final <see cref="SessionTime"/> of the <see cref="SpecificSessionHours"/>.</returns>
        public static SessionTime ToEndSessionTime(this SpecificSessionHours specificSessionHours, InstrumentCode instrumentCode = InstrumentCode.Default)
        {

            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {
                        switch (specificSessionHours)
                        {

                            case (SpecificSessionHours.Electronic):
                                return SpecificSessionTime.Electronic_Close.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.Regular):
                                return SpecificSessionTime.Regular_Close.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.OVN):
                                return SpecificSessionTime.OVN_Close.ToSessionTime(instrumentCode);

                            case (SpecificSessionHours.American):
                                return SpecificSessionTime.American_Close.ToSessionTime();
                            case (SpecificSessionHours.American_IB):
                                return SpecificSessionTime.American_IB_Close.ToSessionTime();
                            case (SpecificSessionHours.American_BB):
                                return SpecificSessionTime.American_BB_Close.ToSessionTime();
                            case (SpecificSessionHours.American_FB):
                                return SpecificSessionTime.American_FB_Close.ToSessionTime();

                            case (SpecificSessionHours.AmericanAndEuropean):
                                return SpecificSessionTime.AmericanAndEuropean_Close.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_IB):
                                return SpecificSessionTime.AmericanAndEuropean_IB_Close.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_BB):
                                return SpecificSessionTime.AmericanAndEuropean_BB_Close.ToSessionTime();
                            case (SpecificSessionHours.AmericanAndEuropean_FB):
                                return SpecificSessionTime.AmericanAndEuropean_FB_Close.ToSessionTime();

                            case (SpecificSessionHours.Asian):
                                return SpecificSessionTime.Asian_Close.ToSessionTime();
                            case (SpecificSessionHours.Asian_IB):
                                return SpecificSessionTime.Asian_IB_Close.ToSessionTime();
                            case (SpecificSessionHours.Asian_BB):
                                return SpecificSessionTime.Asian_BB_Close.ToSessionTime();
                            case (SpecificSessionHours.Asian_FB):
                                return SpecificSessionTime.Asian_FB_Close.ToSessionTime();

                            case (SpecificSessionHours.European):
                                return SpecificSessionTime.European_Close.ToSessionTime();
                            case (SpecificSessionHours.European_IB):
                                return SpecificSessionTime.European_IB_Close.ToSessionTime();
                            case (SpecificSessionHours.European_BB):
                                return SpecificSessionTime.European_BB_Close.ToSessionTime();
                            case (SpecificSessionHours.European_FB):
                                return SpecificSessionTime.European_FB_Close.ToSessionTime();

                            case (SpecificSessionHours.Residual):
                                return SpecificSessionTime.Residual_Close.ToSessionTime();
                            case (SpecificSessionHours.Residual_AET):
                                return SpecificSessionTime.Residual_AET_Close.ToSessionTime();
                            case (SpecificSessionHours.Residual_EOD):
                                return SpecificSessionTime.Residual_EOD_Close.ToSessionTime();
                            case (SpecificSessionHours.Residual_NDB):
                                return SpecificSessionTime.Residual_NDB_Close.ToSessionTime();

                            default:
                                throw new Exception("The specific session hours doesn't exists.");

                        }
                    }

                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

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
