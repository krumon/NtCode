namespace Nt.Core.Trading.Internal
{
    /// <summary>
    /// Extension methods to create generic sessions.
    /// </summary>
    internal static class TradingSessionExtensions
    {
        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.Regular"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.Regular"/></returns>
        public static TradingSession CreateRegularSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.Regular);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.OVN"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.OVN"/></returns>
        public static TradingSession CreateOvernightSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.OVN);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.European"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.European"/></returns>
        public static TradingSession CreateEuropeanSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.European);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.Asian"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.Asian"/></returns>
        public static TradingSession CreateAsianSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.Asian);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.American"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.American"/></returns>
        public static TradingSession CreateAmericanSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.American);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.AmericanAndEuropean"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.AmericanAndEuropean"/></returns>
        public static TradingSession CreateAmericanAndEuropeanSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.AmericanAndEuropean);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.American_RS"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.American_RS"/></returns>
        public static TradingSession CreateAmericanResidualSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.American_RS);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.Asian_RS"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.Asian_RS"/></returns>
        public static TradingSession CreateAsianResidualSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.Asian_RS);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.American_RS_EXT"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.American_RS_EXT"/></returns>
        public static TradingSession CreateAmericanResidualExtraTimeSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.American_RS_EXT);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.American_RS_EOD"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.American_RS_EOD"/></returns>
        public static TradingSession CreateAmericanResidualEndOfDaySession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.American_RS_EOD);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionCode.American_RS_NWD"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionCode.American_RS_NWD"/></returns>
        public static TradingSession CreateAmericanResidualNewDaySession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionCode.American_RS_NWD);
        }

        ///// <summary>
        ///// Create a <see cref="TradingSession"/> instance with the generic american sessions types.
        ///// </summary>
        ///// <param name="_"></param>
        ///// <returns>An instance of <see cref="TradingSession"/> with the generic american sessions types.</returns>
        //public static SessionCollection CreateAmericanSessions(this TradingSession _)
        //{
        //    SessionCollection tradingSessions = new SessionCollection
        //    {
        //        SessionType.AmericanAndEuropean,
        //        SessionType.American
        //    };

        //    return tradingSessions;
        //}

        ///// <summary>
        ///// Create a <see cref="TradingSession"/> instance with the generic american residual sessions types.
        ///// </summary>
        ///// <param name="_"></param>
        ///// <returns>An instance of <see cref="TradingSession"/> with the generic american residual sessions types.</returns>
        //public static SessionCollection CreateAmericanResidualSessions(this TradingSession _)
        //{
        //    SessionCollection tradingSessions = new SessionCollection
        //    {
        //        SessionType.American_RS_EXT,
        //        SessionType.American_RS_EOD,
        //        SessionType.American_RS_NWD
        //    };

        //    return tradingSessions;
        //}

    }
}
