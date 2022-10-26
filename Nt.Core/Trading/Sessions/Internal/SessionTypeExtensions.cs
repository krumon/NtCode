using System;
using System.Linq;

namespace Nt.Core.Trading
{

    /// <summary>
    /// <see cref="SessionCode"/> enum helper methods.
    /// </summary>
    internal static class SessionTypeExtensions
    {

        /// <summary>
        /// Returns the unique code of the <see cref="SessionCode"/>.
        /// </summary>
        /// <param name="tradingSessionType">The specific trading session.</param>
        /// <returns>String that represents the unique code of the <see cref="SessionCode"/>.</returns>
        /// <exception cref="Exception">The <see cref="SessionCode"/> doesn´t exists.</exception>
        public static string ToCode(this SessionCode tradingSessionType)
        {
            switch (tradingSessionType)
            {
                // SESSIONS
                case SessionCode.Electronic:
                    return "EL";
                case SessionCode.Regular:
                    return "RG";
                case SessionCode.OVN:
                    return "OV";
                case SessionCode.American:
                    return "AM";
                case SessionCode.AmericanAndEuropean:
                    return "AE";
                case SessionCode.Asian:
                    return "AS";
                case SessionCode.European:
                    return "EU";
                case SessionCode.American_RS:
                    return "AM-RS";
                case SessionCode.Asian_RS:
                    return "AS-RS";
                case SessionCode.American_RS_EXT:
                    return "AM-RS-EXT";
                case SessionCode.American_RS_EOD:
                    return "AM-RS-EOD";
                case SessionCode.American_RS_NWD:
                    return "AM-RS-NWD";

                default:
                    throw new Exception("The specific trading session doesn't exists.");
            }
        }

        /// <summary>
        /// Returns the description of the <see cref="SessionCode"/>.
        /// </summary>
        /// <param name="tradingSessionType">The specific trading session.</param>
        /// <returns>String that represents the description of the <see cref="SessionCode"/>.</returns>
        /// <exception cref="Exception">The <see cref="SessionCode"/> doesn´t exists.</exception>
        public static string ToDescription(this SessionCode tradingSessionType)
        {
            switch (tradingSessionType)
            {
                // SESSIONS
                case SessionCode.Electronic:
                    return "Electronic TradingSession.";
                case SessionCode.Regular:
                    return "Regular TradingSession.";
                case SessionCode.OVN:
                    return "Overnight TradingSession.";
                case SessionCode.American:
                    return "American TradingSession.";
                case SessionCode.AmericanAndEuropean:
                    return "American and European TradingSession.";
                case SessionCode.Asian:
                    return "Asian TradingSession.";
                case SessionCode.European:
                    return "Asian TradingSession.";
                case SessionCode.American_RS:
                    return "American Residual TradingSession.";
                case SessionCode.Asian_RS:
                    return "Asian Residual TradingSession.";
                case SessionCode.American_RS_EXT:
                    return "American Residual Extra time TradingSession.";
                case SessionCode.American_RS_EOD:
                    return "American Residual End Of Day TradingSession.";
                case SessionCode.American_RS_NWD:
                    return "American Residual New Day TradingSession.";

                default:
                    return "TradingSession Custom";
            }
        }

        /// <summary>
        /// Returns the name of the <see cref="SessionCode"/>.
        /// </summary>
        /// <param name="tradingSessionType">The specific trading session.</param>
        /// <returns>String that represents the name of the <see cref="SessionCode"/>.</returns>
        /// <exception cref="Exception">The <see cref="SessionCode"/> doesn´t exists.</exception>
        public static string ToName(this SessionCode tradingSessionType)
        {
            switch (tradingSessionType)
            {
                // SESSIONS
                case SessionCode.Electronic:
                    return "TradingSession - Electronic";
                case SessionCode.Regular:
                    return "TradingSession - Regular";
                case SessionCode.OVN:
                    return "TradingSession - Overnight";
                case SessionCode.American:
                    return "TradingSession - American";
                case SessionCode.AmericanAndEuropean:
                    return "TradingSession - AmericanAndEuropean";
                case SessionCode.Asian:
                    return "TradingSession - Asian";
                case SessionCode.European:
                    return "TradingSession - European";
                case SessionCode.American_RS:
                    return "TradingSession - AmericanResidual";
                case SessionCode.Asian_RS:
                    return "TradingSession - AsianResidual";
                case SessionCode.American_RS_EXT:
                    return "TradingSession - AmericanResidualExtratime";
                case SessionCode.American_RS_EOD:
                    return "TradingSession - AmericanResidialEndOfDay";
                case SessionCode.American_RS_NWD:
                    return "TradingSession - AmericanResidualNewDay";

                default:
                    return "TradingSession - Custom";
            }
        }

        /// <summary>
        /// Converts the <see cref="SessionCode"/> to initial <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="tradingSessionType"></param>
        /// <returns>Initial <see cref="TradingTime"/> of the <see cref="SessionCode"/>.</returns>
        public static TradingTime ToBeginSessionTime(this SessionCode tradingSessionType, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int offset = 0)
        {
            switch (instrumentCode)
            {
                case (TradingInstrumentCode.Default):
                case (TradingInstrumentCode.MES):
                {

                    switch (tradingSessionType)
                    {
                        case (SessionCode.Electronic):
                            return TradingTimeType.Electronic_Open.ToSessionTime(instrumentCode,offset);
                        case (SessionCode.Regular):
                            return TradingTimeType.Regular_Open.ToSessionTime(instrumentCode, offset);
                        case (SessionCode.OVN):
                            return TradingTimeType.OVN_Open.ToSessionTime(instrumentCode, offset);

                        case (SessionCode.American):
                            return TradingTimeType.American_Open.ToSessionTime(instrumentCode, offset);
                        case (SessionCode.AmericanAndEuropean):
                            return TradingTimeType.AmericanAndEuropean_Open.ToSessionTime(instrumentCode, offset);
                        case (SessionCode.Asian):
                            return TradingTimeType.Asian_Open.ToSessionTime(instrumentCode, offset);
                        case (SessionCode.European):
                            return TradingTimeType.European_Open.ToSessionTime(instrumentCode, offset);
                        case (SessionCode.American_RS):
                            return TradingTimeType.American_RS_Open.ToSessionTime(instrumentCode, offset);
                        case (SessionCode.Asian_RS):
                            return TradingTimeType.Asian_RS_Open.ToSessionTime(instrumentCode, offset);
                        case (SessionCode.American_RS_EXT):
                            return TradingTimeType.American_RS_EXT_Open.ToSessionTime(instrumentCode, offset);
                        case (SessionCode.American_RS_EOD):
                            return TradingTimeType.American_RS_EOD_Open.ToSessionTime(instrumentCode, offset);
                        case (SessionCode.American_RS_NWD):
                            return TradingTimeType.American_RS_NWD_Open.ToSessionTime(instrumentCode, offset);

                        default:
                            throw new Exception("The trading session doesn't exists.");
                    }
                }
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="SessionCode"/> to final <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="tradingSessionType"></param>
        /// <returns>Final <see cref="TradingTime"/> of the <see cref="SessionCode"/>.</returns>
        public static TradingTime ToEndSessionTime(this SessionCode tradingSessionType, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int offset = 0)
        {
            switch (instrumentCode)
            {
                case (TradingInstrumentCode.Default):
                case (TradingInstrumentCode.MES):
                    {

                        switch (tradingSessionType)
                        {
                            case (SessionCode.Electronic):
                                return TradingTimeType.Electronic_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.Regular):
                                return TradingTimeType.Regular_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.OVN):
                                return TradingTimeType.OVN_Close.ToSessionTime(instrumentCode, offset);

                            case (SessionCode.American):
                                return TradingTimeType.American_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.AmericanAndEuropean):
                                return TradingTimeType.AmericanAndEuropean_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.Asian):
                                return TradingTimeType.Asian_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.European):
                                return TradingTimeType.European_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.American_RS):
                                return TradingTimeType.American_RS_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.Asian_RS):
                                return TradingTimeType.Asian_RS_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.American_RS_EXT):
                                return TradingTimeType.American_RS_EXT_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.American_RS_EOD):
                                return TradingTimeType.American_RS_EOD_Close.ToSessionTime(instrumentCode, offset);
                            case (SessionCode.American_RS_NWD):
                                return TradingTimeType.American_RS_NWD_Close.ToSessionTime(instrumentCode, offset);

                            default:
                                throw new Exception("The trading session doesn't exists.");
                        }
                    }
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        public static TradingSession ToSessionHours(this SessionCode type, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int balanceMinutes = 0)
        {
            return TradingSession.CreateTradingSessionByType(type,instrumentCode,balanceMinutes);
        }

        public static SessionCode[] ToArray(this SessionCode type)
        {
            return Enum.GetValues(typeof(SessionCode)).Cast<SessionCode>().ToArray();
        }

    }
}
