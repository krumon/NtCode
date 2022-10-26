namespace Nt.Core.Trading.Internal
{
    /// <summary>
    /// Extension methods to create generic sessions.
    /// </summary>
    internal static class TradingSessionExtensions
    {
        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.Regular"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.Regular"/></returns>
        public static TradingSession CreateRegularSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.Regular);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.OVN"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.OVN"/></returns>
        public static TradingSession CreateOvernightSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.OVN);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.European"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.European"/></returns>
        public static TradingSession CreateEuropeanSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.European);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.Asian"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.Asian"/></returns>
        public static TradingSession CreateAsianSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.Asian);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.American"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.American"/></returns>
        public static TradingSession CreateAmericanSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.American);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.AmericanAndEuropean"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.AmericanAndEuropean"/></returns>
        public static TradingSession CreateAmericanAndEuropeanSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.AmericanAndEuropean);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.American_RS"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.American_RS"/></returns>
        public static TradingSession CreateAmericanResidualSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.American_RS);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.Asian_RS"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.Asian_RS"/></returns>
        public static TradingSession CreateAsianResidualSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.Asian_RS);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.American_RS_EXT"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.American_RS_EXT"/></returns>
        public static TradingSession CreateAmericanResidualExtraTimeSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.American_RS_EXT);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.American_RS_EOD"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.American_RS_EOD"/></returns>
        public static TradingSession CreateAmericanResidualEndOfDaySession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.American_RS_EOD);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="TradingSessionType.American_RS_NWD"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="TradingSessionType.American_RS_NWD"/></returns>
        public static TradingSession CreateAmericanResidualNewDaySession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(TradingSessionType.American_RS_NWD);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic american sessions types.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic american sessions types.</returns>
        public static TradingSessionCollection CreateAmericanSessions(this TradingSession _)
        {
            TradingSessionCollection tradingSessions = new TradingSessionCollection
            {
                TradingSessionType.AmericanAndEuropean,
                TradingSessionType.American
            };

            return tradingSessions;
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic american residual sessions types.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic american residual sessions types.</returns>
        public static TradingSessionCollection CreateAmericanResidualSessions(this TradingSession _)
        {
            TradingSessionCollection tradingSessions = new TradingSessionCollection
            {
                TradingSessionType.American_RS_EXT,
                TradingSessionType.American_RS_EOD,
                TradingSessionType.American_RS_NWD
            };

            return tradingSessions;
        }

    }
}
