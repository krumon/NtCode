using System;
using System.Collections.Generic;

namespace NtCore
{
    /// <summary>
    /// Represents the SessionHours Indicator Core.
    /// </summary>
    public class KrSessionHours : SessionHours
    {

        #region Public properties

        public DateTime CurrentTime { get; set; } = DateTime.MinValue;

        public DateTime NextBeginDateTime { get; set; } = DateTime.MinValue;

        public DateTime NextEndDateTime { get; set; } = DateTime.MinValue;

        #endregion

        #region Constructor


        #endregion

        #region Instance methods

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="tradingSession">the <see cref="TradingSession"/> to create the <see cref="SessionHours"/> class.</param>
        /// <param name="instrumentCode">The unique code of the instrument.</param>
        /// <param name="balanceMinutes">The minutes of the balance session.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        public static KrSessionHours CreateSessionHours(TradingSession tradingSession, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            return new KrSessionHours
            {
                BeginSessionTime = tradingSession.ToBeginSessionTime(instrumentCode, balanceMinutes),
                EndSessionTime = tradingSession.ToEndSessionTime(instrumentCode, balanceMinutes),
            };
        }

        #endregion

        #region Public methods

        public KrSessionHours AddDefaultSessions(InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            // Main Sessions
            AddSession(TradingSession.Regular, instrumentCode, 0, 0);
            AddSession(TradingSession.OVN, instrumentCode, 0, 0);
            // Regular Sessions
            Sessions[0].AddSession(TradingSession.AmericanAndEuropean, instrumentCode, 0, 0);
            Sessions[0].AddSession(TradingSession.American, instrumentCode, 0, 0);
            // Overnight Sessions
            Sessions[1].AddSession(TradingSession.American_RS, instrumentCode, 0, 0);
            Sessions[1].AddSession(TradingSession.Asian, instrumentCode, 0, 0);
            Sessions[1].AddSession(TradingSession.Asian_RS, instrumentCode, 0, 0);
            Sessions[1].AddSession(TradingSession.European, instrumentCode, 0, 0);
            // Minor Sessions
            Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_EXT, instrumentCode, 0, 0);
            Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_EOD, instrumentCode, 0, 0);
            Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_NWD, instrumentCode, 0, 0);

            return this;
        }

        protected bool IsInSession(DateTime time)
        {
            CurrentTime = time;

            if (CurrentTime > NextBeginDateTime)
            {
                if (CurrentTime > NextEndDateTime)
                {
                    NextBeginDateTime = GetNextBeginDateTime(CurrentTime);
                    NextEndDateTime = GetNextEndDateTime(CurrentTime);
                    return true;
                }
            }
            return false;
        }

        #endregion

    }


}
