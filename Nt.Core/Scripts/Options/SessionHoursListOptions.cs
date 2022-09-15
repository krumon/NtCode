using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Options to create <see cref="SessionHoursList"/> object.
    /// </summary>
    public class SessionHoursListOptions : SessionOptions<SessionHoursListOptions>
    {
        #region Private members / Default values

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

        #region Copy methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public override void CopyTo(SessionHoursListOptions options)
        {
            // Copy the parent options...
            base.CopyTo(options);

            // Copy the new options
            options.MaxSessionsToStored = MaxSessionsToStored;

        }

        /// <summary>
        /// Copy options to ninjatrader properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public override void CopyTo(NinjaScriptBase ninjascript)
        {
            // Copy the parent options
            base.CopyTo(ninjascript);
        }

        #endregion


    }
}
