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

        public void CreateDefaultSessions(InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            // Main Sessions
            Add(GetRegularSession());
            Add(GetOvernightSession());
            // Regular Sessions
            Sessions[0].Add(GetAmericanAndEuropeanSession());
            Sessions[0].Add(GetAmericanSession());
            // Overnight Sessions
            Sessions[1].Add(GetAmericanResidualSession());
            Sessions[1].Add(GetAsianSession());
            Sessions[1].Add(GetAsianResidualSession());
            Sessions[1].Add(GetEuropeanSession());
            // Minor Sessions
            Sessions[1].Sessions[0].Add(GetAmericanResidualExtraTimeSession());
            Sessions[1].Sessions[0].Add(GetAmericanResidualEndOfDaySession());
            Sessions[1].Sessions[0].Add(GetAmericanResidualNewDaySession());
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
