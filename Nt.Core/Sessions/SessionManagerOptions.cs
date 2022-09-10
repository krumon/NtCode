namespace Nt.Core
{
    /// <summary>
    /// Options to create <see cref="SessionsManager"/> object.
    /// </summary>
    public class SessionManagerOptions
    {
        #region Private members / Default values

        /// <summary>
        /// Max sessions to stored in <see cref="SessionsManager"/> object.
        /// </summary>
        private int maxSessionsToStored = 256;

        #endregion

        #region Public properties

        /// <summary>
        /// Max sessions to stored in <see cref="SessionsManager"/> object.
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
