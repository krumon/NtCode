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
        /// Converts the <see cref="BalanceSession"/> to <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="sessionBalance"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static TradingSession ToTradingSession(this BalanceSession sessionBalance)
        {
            switch (sessionBalance)
            {
                case (BalanceSession.AmericanAndEuropean_BB):
                case (BalanceSession.AmericanAndEuropean_FB):
                case (BalanceSession.AmericanAndEuropean_IB):
                    return TradingSession.AmericanAndEuropean;

                case (BalanceSession.American_BB):
                case (BalanceSession.American_FB):
                case (BalanceSession.American_IB):
                    return TradingSession.American;

                case (BalanceSession.European_BB):
                case (BalanceSession.European_FB):
                case (BalanceSession.European_IB):
                    return TradingSession.European;

                case (BalanceSession.Asian_BB):
                case (BalanceSession.Asian_FB):
                case (BalanceSession.Asian_IB):
                    return TradingSession.Asian;

                case (BalanceSession.Regular_BB):
                case (BalanceSession.Regular_FB):
                case (BalanceSession.Regular_IB):
                    return TradingSession.Regular;

                case (BalanceSession.OVN_BB):
                case (BalanceSession.OVN_FB):
                case (BalanceSession.OVN_IB):
                    return TradingSession.OVN;

                case (BalanceSession.Asian_RS_BB):
                case (BalanceSession.Asian_RS_FB):
                case (BalanceSession.Asian_RS_IB):
                    return TradingSession.Asian_RS;

                case (BalanceSession.American_RS_BB):
                case (BalanceSession.American_RS_FB):
                case (BalanceSession.American_RS_IB):
                    return TradingSession.American_RS;

                case (BalanceSession.American_RS_NWD_BB):
                case (BalanceSession.American_RS_NWD_FB):
                case (BalanceSession.American_RS_NWD_IB):
                    return TradingSession.American_RS_NWD;

                default:
                    throw new Exception("The Session Balance doesn´t exist.");
            }
        }

        /// <summary>
        /// Returns true if the <see cref="BalanceSession"/> is a initial balance.
        /// </summary>
        /// <param name="sessionBalance"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool IsInitialBalance(this BalanceSession sessionBalance)
        {
            switch (sessionBalance)
            {
                case (BalanceSession.AmericanAndEuropean_IB):
                case (BalanceSession.American_IB):
                case (BalanceSession.European_IB):
                case (BalanceSession.Asian_IB):
                case (BalanceSession.Regular_IB):
                case (BalanceSession.OVN_IB):
                case (BalanceSession.Asian_RS_IB):
                case (BalanceSession.American_RS_IB):
                case (BalanceSession.American_RS_NWD_IB):
                    return true;

                case (BalanceSession.AmericanAndEuropean_BB):
                case (BalanceSession.American_BB):
                case (BalanceSession.European_BB):
                case (BalanceSession.Asian_BB):
                case (BalanceSession.Regular_BB):
                case (BalanceSession.OVN_BB):
                case (BalanceSession.Asian_RS_BB):
                case (BalanceSession.American_RS_BB):
                case (BalanceSession.American_RS_NWD_BB):
                    return false;

                case (BalanceSession.AmericanAndEuropean_FB):
                case (BalanceSession.American_FB):
                case (BalanceSession.European_FB):
                case (BalanceSession.Asian_FB):
                case (BalanceSession.Regular_FB):
                case (BalanceSession.OVN_FB):
                case (BalanceSession.Asian_RS_FB):
                case (BalanceSession.American_RS_FB):
                case (BalanceSession.American_RS_NWD_FB):
                    return false;

                default:
                    throw new Exception("The Session Balance doesn´t exist.");
            }
        }

        /// <summary>
        /// Returns true if the <see cref="BalanceSession"/> is between balances.
        /// </summary>
        /// <param name="sessionBalance"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool IsBetweenBalances(this BalanceSession sessionBalance)
        {
            switch (sessionBalance)
            {
                case (BalanceSession.AmericanAndEuropean_IB):
                case (BalanceSession.American_IB):
                case (BalanceSession.European_IB):
                case (BalanceSession.Asian_IB):
                case (BalanceSession.Regular_IB):
                case (BalanceSession.OVN_IB):
                case (BalanceSession.Asian_RS_IB):
                case (BalanceSession.American_RS_IB):
                case (BalanceSession.American_RS_NWD_IB):
                    return false;

                case (BalanceSession.AmericanAndEuropean_BB):
                case (BalanceSession.American_BB):
                case (BalanceSession.European_BB):
                case (BalanceSession.Asian_BB):
                case (BalanceSession.Regular_BB):
                case (BalanceSession.OVN_BB):
                case (BalanceSession.Asian_RS_BB):
                case (BalanceSession.American_RS_BB):
                case (BalanceSession.American_RS_NWD_BB):
                    return true;

                case (BalanceSession.AmericanAndEuropean_FB):
                case (BalanceSession.American_FB):
                case (BalanceSession.European_FB):
                case (BalanceSession.Asian_FB):
                case (BalanceSession.Regular_FB):
                case (BalanceSession.OVN_FB):
                case (BalanceSession.Asian_RS_FB):
                case (BalanceSession.American_RS_FB):
                case (BalanceSession.American_RS_NWD_FB):
                    return false;

                default:
                    throw new Exception("The Session Balance doesn´t exist.");
            }
        }

        /// <summary>
        /// Returns true if the <see cref="BalanceSession"/> is a final balance.
        /// </summary>
        /// <param name="sessionBalance"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool IsFinalBalance(this BalanceSession sessionBalance)
        {
            switch (sessionBalance)
            {
                case (BalanceSession.AmericanAndEuropean_IB):
                case (BalanceSession.American_IB):
                case (BalanceSession.European_IB):
                case (BalanceSession.Asian_IB):
                case (BalanceSession.Regular_IB):
                case (BalanceSession.OVN_IB):
                case (BalanceSession.Asian_RS_IB):
                case (BalanceSession.American_RS_IB):
                case (BalanceSession.American_RS_NWD_IB):
                    return false;

                case (BalanceSession.AmericanAndEuropean_BB):
                case (BalanceSession.American_BB):
                case (BalanceSession.European_BB):
                case (BalanceSession.Asian_BB):
                case (BalanceSession.Regular_BB):
                case (BalanceSession.OVN_BB):
                case (BalanceSession.Asian_RS_BB):
                case (BalanceSession.American_RS_BB):
                case (BalanceSession.American_RS_NWD_BB):
                    return false;

                case (BalanceSession.AmericanAndEuropean_FB):
                case (BalanceSession.American_FB):
                case (BalanceSession.European_FB):
                case (BalanceSession.Asian_FB):
                case (BalanceSession.Regular_FB):
                case (BalanceSession.OVN_FB):
                case (BalanceSession.Asian_RS_FB):
                case (BalanceSession.American_RS_FB):
                case (BalanceSession.American_RS_NWD_FB):
                    return true;

                default:
                    throw new Exception("The Session Balance doesn´t exist.");
            }
        }

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
        /// Returns the unique code of the <see cref="BalanceSession"/>.
        /// </summary>
        /// <param name="tradingBalance">The specific trading balance.</param>
        /// <returns>String that represents the unique code of the <see cref="BalanceSession"/>.</returns>
        /// <exception cref="Exception">The <see cref="BalanceSession"/> doesn´t exists.</exception>
        public static string ToCode(this BalanceSession tradingBalance)
        {
            switch (tradingBalance)
            {
                // BALANCES
                case BalanceSession.Regular_IB:
                    return "RG-IB";
                case BalanceSession.Regular_BB:
                    return "RG-BB";
                case BalanceSession.Regular_FB:
                    return "RG-FB";

                case BalanceSession.OVN_IB:
                    return "OV-IB";
                case BalanceSession.OVN_BB:
                    return "OV-BB";
                case BalanceSession.OVN_FB:
                    return "OV-FB";

                case BalanceSession.American_IB:
                    return "AM-IB";
                case BalanceSession.American_BB:
                    return "AM-BB";
                case BalanceSession.American_FB:
                    return "AM-FB";

                case BalanceSession.AmericanAndEuropean_IB:
                    return "AE-IB";
                case BalanceSession.AmericanAndEuropean_BB:
                    return "AE-BB";
                case BalanceSession.AmericanAndEuropean_FB:
                    return "AE-FB";

                case BalanceSession.Asian_IB:
                    return "AS-IB";
                case BalanceSession.Asian_BB:
                    return "AS-BB";
                case BalanceSession.Asian_FB:
                    return "AS-FB";

                case BalanceSession.European_IB:
                    return "EU-IB";
                case BalanceSession.European_BB:
                    return "EU-BB";
                case BalanceSession.European_FB:
                    return "EU-FB";

                case BalanceSession.American_RS_IB:
                    return "AM-RS-IB";
                case BalanceSession.American_RS_BB:
                    return "AM-RS-BB";
                case BalanceSession.American_RS_FB:
                    return "AM-RS-FB";

                case BalanceSession.Asian_RS_IB:
                    return "AS-RS-IB";
                case BalanceSession.Asian_RS_BB:
                    return "AS-RS-BB";
                case BalanceSession.Asian_RS_FB:
                    return "AS-RS-FB";

                case BalanceSession.American_RS_NWD_IB:
                    return "AM-RS-NWD-IB";
                case BalanceSession.American_RS_NWD_BB:
                    return "AM-RS-NWD-BB";
                case BalanceSession.American_RS_NWD_FB:
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
        /// Returns the description of the <see cref="BalanceSession"/>.
        /// </summary>
        /// <param name="tradingBalance">The specific trading balance.</param>
        /// <returns>String that represents the description of the <see cref="BalanceSession"/>.</returns>
        /// <exception cref="Exception">The <see cref="BalanceSession"/> doesn´t exists.</exception>
        public static string ToDescription(this BalanceSession tradingBalance)
        {
            switch (tradingBalance)
            {
                // BALANCES
                case BalanceSession.Regular_IB:
                    return "Regular Session Initial Balance.";
                case BalanceSession.Regular_BB:
                    return "Regular Session Between Balances.";
                case BalanceSession.Regular_FB:
                    return "Regular Session Final Balance.";

                case BalanceSession.OVN_IB:
                    return "Overnight Session Initial Balance.";
                case BalanceSession.OVN_BB:
                    return "Overnight Session Between Balances.";
                case BalanceSession.OVN_FB:
                    return "Overnight Session Final Balance.";

                case BalanceSession.American_IB:
                    return "American Session Initial Balance.";
                case BalanceSession.American_BB:
                    return "American Session Between Balances.";
                case BalanceSession.American_FB:
                    return "American Session Final Balance.";

                case BalanceSession.AmericanAndEuropean_IB:
                    return "American and European Session Initial Balance.";
                case BalanceSession.AmericanAndEuropean_BB:
                    return "American and European Session Between Balances.";
                case BalanceSession.AmericanAndEuropean_FB:
                    return "American and European Session Final Balance.";

                case BalanceSession.Asian_IB:
                    return "Asian Initial Balance.";
                case BalanceSession.Asian_BB:
                    return "Asian Between Balances.";
                case BalanceSession.Asian_FB:
                    return "Asian Final Balance.";

                case BalanceSession.European_IB:
                    return "European Initial Balance.";
                case BalanceSession.European_BB:
                    return "European Between Balances.";
                case BalanceSession.European_FB:
                    return "European Final Balance.";

                case BalanceSession.American_RS_IB:
                    return "American Residual Initial Balance.";
                case BalanceSession.American_RS_BB:
                    return "American Residual Between Balances.";
                case BalanceSession.American_RS_FB:
                    return "American Residual Initial Balance";

                case BalanceSession.Asian_RS_IB:
                    return "Asian Residual Initial Balance.";
                case BalanceSession.Asian_RS_BB:
                    return "Asian Residual Between Balances.";
                case BalanceSession.Asian_RS_FB:
                    return "Asian Residual Final Balance.";

                case BalanceSession.American_RS_NWD_IB:
                    return "American Residual New Day Initial Balance.";
                case BalanceSession.American_RS_NWD_BB:
                    return "American Residual New Day Between Balances.";
                case BalanceSession.American_RS_NWD_FB:
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

        /// <summary>
        /// Converts the <see cref="BalanceSession"/> to initial <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="tradingSession">The type of the trading session.</param>
        /// <param name="balanceMinutes">The minutes of the balance.</param>
        /// <returns>Initial <see cref="SessionTime"/> of the <see cref="BalanceSession"/>.</returns>
        public static SessionTime ToBeginSessionTime(this BalanceSession sessionBalance, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            if (sessionBalance.IsInitialBalance())
                return sessionBalance.ToTradingSession().ToBeginSessionTime(instrumentCode, 0);
            if (sessionBalance.IsBetweenBalances())
                return sessionBalance.ToTradingSession().ToBeginSessionTime(instrumentCode, balanceMinutes);
            if (sessionBalance.IsFinalBalance())
                return sessionBalance.ToTradingSession().ToEndSessionTime(instrumentCode, -balanceMinutes);

            throw new Exception("Error in ToBeginSessionTime Method.");
        }

        /// <summary>
        /// Converts the <see cref="BalanceSession"/> to initial <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="tradingSession">The type of the trading session.</param>
        /// <param name="balanceMinutes">The minutes of the balance.</param>
        /// <returns>Initial <see cref="SessionTime"/> of the <see cref="BalanceSession"/>.</returns>
        public static SessionTime ToEndSessionTime(this BalanceSession sessionBalance, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            if (sessionBalance.IsInitialBalance())
                return sessionBalance.ToTradingSession().ToBeginSessionTime(instrumentCode, balanceMinutes);
            if (sessionBalance.IsBetweenBalances())
                return sessionBalance.ToTradingSession().ToEndSessionTime(instrumentCode, -balanceMinutes);
            if (sessionBalance.IsFinalBalance())
                return sessionBalance.ToTradingSession().ToEndSessionTime(instrumentCode, 0);

            throw new Exception("Error in ToBeginSessionTime Method.");

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

        public static SessionHours ToSessionHours(this TradingSession type)
        {
            return SessionHours.CreateSessionHoursByType(type);
        }

        public static SessionHours ToInitialBalance(this BalanceSession sessionBalance, int balanceMinutes, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            SessionTime initialTime = sessionBalance.ToTradingSession().ToBeginSessionTime(instrumentCode);
            SessionTime finalTime = sessionBalance.ToTradingSession().ToBeginSessionTime(instrumentCode,balanceMinutes);
            //return SessionHours.CreateSessionHoursByType(type);
            return null;
        }

        public static TradingSession[] ToArray(this TradingSession type)
        {
            return Enum.GetValues(typeof(TradingSession)).Cast<TradingSession>().ToArray();
        }
    }
}
