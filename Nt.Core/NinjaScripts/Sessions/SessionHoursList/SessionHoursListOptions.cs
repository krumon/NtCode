using NinjaTrader.NinjaScript;

namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// Options to create <see cref="SessionHoursList"/> object.
    /// </summary>
    public class SessionHoursListOptions : BaseSessionOptions<SessionHoursListOptions>, ISessionHoursListOptions
    {

        #region Default values

        /// <summary>
        /// Max sessions to stored in <see cref="SessionHours"/> collection.
        /// </summary>
        private int maxSessionsToStored = 256;

        #endregion

        #region Public properties

        /// <summary>
        /// Max sessions to stored in <see cref="SessionHours"/> collection.
        /// </summary>
        public int MaxSessionsToStored
        {
            get => maxSessionsToStored;
            set
            {
                // Make sure value changed
                if (maxSessionsToStored == value)
                    return;

                if (value < 2)
                    // Store the last and actual session
                    maxSessionsToStored = 2;

                else if (value > int.MaxValue)
                    maxSessionsToStored = int.MaxValue;

                else
                    maxSessionsToStored = value;

            }
        }

        #endregion

    }
}
