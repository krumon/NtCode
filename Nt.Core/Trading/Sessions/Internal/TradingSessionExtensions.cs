﻿namespace Nt.Core.Trading.Internal
{
    /// <summary>
    /// Extension methods to create generic sessions.
    /// </summary>
    internal static class TradingSessionExtensions
    {
        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.Regular"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.Regular"/></returns>
        public static TradingSession CreateRegularSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.Regular);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.OVN"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.OVN"/></returns>
        public static TradingSession CreateOvernightSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.OVN);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.European"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.European"/></returns>
        public static TradingSession CreateEuropeanSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.European);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.Asian"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.Asian"/></returns>
        public static TradingSession CreateAsianSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.Asian);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.American"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.American"/></returns>
        public static TradingSession CreateAmericanSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.American);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.AmericanAndEuropean"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.AmericanAndEuropean"/></returns>
        public static TradingSession CreateAmericanAndEuropeanSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.AmericanAndEuropean);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.American_RS"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.American_RS"/></returns>
        public static TradingSession CreateAmericanResidualSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.American_RS);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.Asian_RS"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.Asian_RS"/></returns>
        public static TradingSession CreateAsianResidualSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.Asian_RS);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.American_RS_EXT"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.American_RS_EXT"/></returns>
        public static TradingSession CreateAmericanResidualExtraTimeSession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.American_RS_EXT);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.American_RS_EOD"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.American_RS_EOD"/></returns>
        public static TradingSession CreateAmericanResidualEndOfDaySession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.American_RS_EOD);
        }

        /// <summary>
        /// Create a <see cref="TradingSession"/> instance with the generic type <see cref="SessionType.American_RS_NWD"/>.
        /// </summary>
        /// <param name="_"></param>
        /// <returns>An instance of <see cref="TradingSession"/> with the generic type <see cref="SessionType.American_RS_NWD"/></returns>
        public static TradingSession CreateAmericanResidualNewDaySession(this TradingSession _)
        {
            return TradingSession.CreateTradingSessionByType(SessionType.American_RS_NWD);
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
