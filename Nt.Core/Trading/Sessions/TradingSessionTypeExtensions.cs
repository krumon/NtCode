using System;
using System.Linq;

namespace Nt.Core.Trading
{

    /// <summary>
    /// <see cref="TradingSessionType"/> enum helper methods.
    /// </summary>
    public static class TradingSessionTypeExtensions
    {

        /// <summary>
        /// Returns the unique code of the <see cref="TradingSessionType"/>.
        /// </summary>
        /// <param name="tradingSessionType">The specific trading session.</param>
        /// <returns>String that represents the unique code of the <see cref="TradingSessionType"/>.</returns>
        /// <exception cref="Exception">The <see cref="TradingSessionType"/> doesn´t exists.</exception>
        public static string ToCode(this TradingSessionType tradingSessionType)
        {
            switch (tradingSessionType)
            {
                // SESSIONS
                case TradingSessionType.Electronic:
                    return "EL";
                case TradingSessionType.Regular:
                    return "RG";
                case TradingSessionType.OVN:
                    return "OV";
                case TradingSessionType.American:
                    return "AM";
                case TradingSessionType.AmericanAndEuropean:
                    return "AE";
                case TradingSessionType.Asian:
                    return "AS";
                case TradingSessionType.European:
                    return "EU";
                case TradingSessionType.American_RS:
                    return "AM-RS";
                case TradingSessionType.Asian_RS:
                    return "AS-RS";
                case TradingSessionType.American_RS_EXT:
                    return "AM-RS-EXT";
                case TradingSessionType.American_RS_EOD:
                    return "AM-RS-EOD";
                case TradingSessionType.American_RS_NWD:
                    return "AM-RS-NWD";

                default:
                    throw new Exception("The specific trading session doesn't exists.");
            }
        }

        /// <summary>
        /// Returns the description of the <see cref="TradingSessionType"/>.
        /// </summary>
        /// <param name="tradingSessionType">The specific trading session.</param>
        /// <returns>String that represents the description of the <see cref="TradingSessionType"/>.</returns>
        /// <exception cref="Exception">The <see cref="TradingSessionType"/> doesn´t exists.</exception>
        public static string ToDescription(this TradingSessionType tradingSessionType)
        {
            switch (tradingSessionType)
            {
                // SESSIONS
                case TradingSessionType.Electronic:
                    return "Electronic TradingSessionInfo.";
                case TradingSessionType.Regular:
                    return "Regular TradingSessionInfo.";
                case TradingSessionType.OVN:
                    return "Overnight TradingSessionInfo.";
                case TradingSessionType.American:
                    return "American TradingSessionInfo.";
                case TradingSessionType.AmericanAndEuropean:
                    return "American and European TradingSessionInfo.";
                case TradingSessionType.Asian:
                    return "Asian TradingSessionInfo.";
                case TradingSessionType.European:
                    return "Asian TradingSessionInfo.";
                case TradingSessionType.American_RS:
                    return "American Residual TradingSessionInfo.";
                case TradingSessionType.Asian_RS:
                    return "Asian Residual TradingSessionInfo.";
                case TradingSessionType.American_RS_EXT:
                    return "American Residual Extra time TradingSessionInfo.";
                case TradingSessionType.American_RS_EOD:
                    return "American Residual End Of Day TradingSessionInfo.";
                case TradingSessionType.American_RS_NWD:
                    return "American Residual New Day TradingSessionInfo.";

                default:
                    throw new Exception("The specific trading doesn't exists.");
            }
        }

        /// <summary>
        /// Converts the <see cref="TradingSessionType"/> to initial <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="tradingSessionType"></param>
        /// <returns>Initial <see cref="TradingTime"/> of the <see cref="TradingSessionType"/>.</returns>
        public static TradingTime ToBeginSessionTime(this TradingSessionType tradingSessionType, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int offset = 0)
        {
            switch (instrumentCode)
            {
                case (TradingInstrumentCode.Default):
                case (TradingInstrumentCode.MES):
                {

                    switch (tradingSessionType)
                    {
                        case (TradingSessionType.Electronic):
                            return TradingTimeType.Electronic_Open.ToSessionTime(instrumentCode,offset);
                        case (TradingSessionType.Regular):
                            return TradingTimeType.Regular_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSessionType.OVN):
                            return TradingTimeType.OVN_Open.ToSessionTime(instrumentCode, offset);

                        case (TradingSessionType.American):
                            return TradingTimeType.American_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSessionType.AmericanAndEuropean):
                            return TradingTimeType.AmericanAndEuropean_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSessionType.Asian):
                            return TradingTimeType.Asian_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSessionType.European):
                            return TradingTimeType.European_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSessionType.American_RS):
                            return TradingTimeType.American_RS_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSessionType.Asian_RS):
                            return TradingTimeType.Asian_RS_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSessionType.American_RS_EXT):
                            return TradingTimeType.American_RS_EXT_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSessionType.American_RS_EOD):
                            return TradingTimeType.American_RS_EOD_Open.ToSessionTime(instrumentCode, offset);
                        case (TradingSessionType.American_RS_NWD):
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
        /// Method to convert the <see cref="TradingSessionType"/> to final <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="tradingSessionType"></param>
        /// <returns>Final <see cref="TradingTime"/> of the <see cref="TradingSessionType"/>.</returns>
        public static TradingTime ToEndSessionTime(this TradingSessionType tradingSessionType, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int offset = 0)
        {
            switch (instrumentCode)
            {
                case (TradingInstrumentCode.Default):
                case (TradingInstrumentCode.MES):
                    {

                        switch (tradingSessionType)
                        {
                            case (TradingSessionType.Electronic):
                                return TradingTimeType.Electronic_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.Regular):
                                return TradingTimeType.Regular_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.OVN):
                                return TradingTimeType.OVN_Close.ToSessionTime(instrumentCode, offset);

                            case (TradingSessionType.American):
                                return TradingTimeType.American_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.AmericanAndEuropean):
                                return TradingTimeType.AmericanAndEuropean_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.Asian):
                                return TradingTimeType.Asian_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.European):
                                return TradingTimeType.European_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.American_RS):
                                return TradingTimeType.American_RS_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.Asian_RS):
                                return TradingTimeType.Asian_RS_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.American_RS_EXT):
                                return TradingTimeType.American_RS_EXT_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.American_RS_EOD):
                                return TradingTimeType.American_RS_EOD_Close.ToSessionTime(instrumentCode, offset);
                            case (TradingSessionType.American_RS_NWD):
                                return TradingTimeType.American_RS_NWD_Close.ToSessionTime(instrumentCode, offset);

                            default:
                                throw new Exception("The trading session doesn't exists.");
                        }
                    }
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }

        }

        public static TradingSession ToSessionHours(this TradingSessionType type, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int balanceMinutes = 0)
        {
            return TradingSession.CreateTradingSessionByType(type,instrumentCode,balanceMinutes);
        }

        public static TradingSessionType[] ToArray(this TradingSessionType type)
        {
            return Enum.GetValues(typeof(TradingSessionType)).Cast<TradingSessionType>().ToArray();
        }

    }
}
