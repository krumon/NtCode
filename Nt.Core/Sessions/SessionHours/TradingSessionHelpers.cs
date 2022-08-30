using System;
using System.Linq;

namespace Nt.Core
{

    /// <summary>
    /// <see cref="TradingSession"/> enum helper methods.
    /// </summary>
    public static class TradingSessionHelpers
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
                    return "EU";
                case TradingSession.American_RS:
                    return "AM-RS";
                case TradingSession.Asian_RS:
                    return "AS-RS";
                case TradingSession.American_RS_EXT:
                    return "AM-RS-EXT";
                case TradingSession.American_RS_EOD:
                    return "AM-RS-EOD";
                case TradingSession.American_RS_NWD:
                    return "AM-RS-NWD";

                default:
                    throw new Exception("The specific trading session doesn't exists.");
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
                    return "Electronic SessionHours.";
                case TradingSession.Regular:
                    return "Regular SessionHours.";
                case TradingSession.OVN:
                    return "Overnight SessionHours.";
                case TradingSession.American:
                    return "American SessionHours.";
                case TradingSession.AmericanAndEuropean:
                    return "American and European SessionHours.";
                case TradingSession.Asian:
                    return "Asian SessionHours.";
                case TradingSession.European:
                    return "Asian SessionHours.";
                case TradingSession.American_RS:
                    return "American Residual SessionHours.";
                case TradingSession.Asian_RS:
                    return "Asian Residual SessionHours.";
                case TradingSession.American_RS_EXT:
                    return "American Residual Extra time SessionHours.";
                case TradingSession.American_RS_EOD:
                    return "American Residual End Of Day SessionHours.";
                case TradingSession.American_RS_NWD:
                    return "American Residual New Day SessionHours.";

                default:
                    throw new Exception("The specific trading doesn't exists.");
            }
        }

        /// <summary>
        /// Converts the <see cref="TradingSession"/> to initial <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="tradingSession"></param>
        /// <returns>Initial <see cref="SessionTime"/> of the <see cref="TradingSession"/>.</returns>
        public static SessionTime ToBeginSessionTime(this TradingSession tradingSession, InstrumentCode instrumentCode = InstrumentCode.Default, int offset = 0)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                {

                    switch (tradingSession)
                    {
                        case (TradingSession.Electronic):
                            return TradingTime.Electronic_Open.ToSessionTime(instrumentCode,offset);
                        case (TradingSession.Regular):
                            return TradingTime.Regular_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSession.OVN):
                            return TradingTime.OVN_Open.ToSessionTime(instrumentCode, offset);

                        case (TradingSession.American):
                            return TradingTime.American_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSession.AmericanAndEuropean):
                            return TradingTime.AmericanAndEuropean_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSession.Asian):
                            return TradingTime.Asian_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSession.European):
                            return TradingTime.European_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSession.American_RS):
                            return TradingTime.American_RS_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSession.Asian_RS):
                            return TradingTime.Asian_RS_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSession.American_RS_EXT):
                            return TradingTime.American_RS_EXT_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSession.American_RS_EOD):
                            return TradingTime.American_RS_EOD_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSession.American_RS_NWD):
                            return TradingTime.American_RS_NWD_Open.ToSessionTime(instrumentCode, offset);

                        default:
                            throw new Exception("The trading session doesn't exists.");
                    }
                }
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingSession"/> to final <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="tradingSession"></param>
        /// <returns>Final <see cref="SessionTime"/> of the <see cref="TradingSession"/>.</returns>
        public static SessionTime ToEndSessionTime(this TradingSession tradingSession, InstrumentCode instrumentCode = InstrumentCode.Default, int offset = 0)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    {

                        switch (tradingSession)
                        {
                            case (TradingSession.Electronic):
                                return TradingTime.Electronic_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.Regular):
                                return TradingTime.Regular_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.OVN):
                                return TradingTime.OVN_Close.ToSessionTime(instrumentCode, offset);

                            case (TradingSession.American):
                                return TradingTime.American_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.AmericanAndEuropean):
                                return TradingTime.AmericanAndEuropean_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.Asian):
                                return TradingTime.Asian_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.European):
                                return TradingTime.European_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.American_RS):
                                return TradingTime.American_RS_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.Asian_RS):
                                return TradingTime.Asian_RS_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.American_RS_EXT):
                                return TradingTime.American_RS_EXT_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.American_RS_EOD):
                                return TradingTime.American_RS_EOD_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSession.American_RS_NWD):
                                return TradingTime.American_RS_NWD_Close.ToSessionTime(instrumentCode, offset);

                            default:
                                throw new Exception("The trading session doesn't exists.");
                        }
                    }
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        public static SessionHours ToSessionHours(this TradingSession type, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            return SessionHours.CreateSessionHoursByType(type,instrumentCode,balanceMinutes);
        }

        public static TradingSession[] ToArray(this TradingSession type)
        {
            return Enum.GetValues(typeof(TradingSession)).Cast<TradingSession>().ToArray();
        }

    }
}
