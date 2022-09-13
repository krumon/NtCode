using NinjaTrader.NinjaScript;
using Rules1;
using System;

namespace Nt.Core
{
    /// <summary>
    /// <see cref="SessionsManager"/> builder.
    /// </summary>
    public class SessionsManagerBuilder
    {
        NtScriptOptions ntScriptOptions;
        SessionFiltersOptions sessionFiltersOptions;
        SessionHoursListOptions sessionHoursListOptions;

        #region Public methods

        /// <summary>
        /// Method to build the <see cref="SessionsManager"/> object.
        /// </summary>
        /// <returns>The <see cref="SessionsManager"/> object created by <see cref="SessionsManagerBuilder"/>.</returns>
        public SessionsManager Build()
        {
            var sessionsManager = new SessionsManager()
                .ConfigureNtScripts<SessionsManager>((options) =>
                {
                    options.Calculate = Calculate.OnBarClose;
                    options.Description = "My master indicator.";
                    options.Name = "KrMasTerSession";
                });

            if (sessionFiltersOptions != null)
                sessionsManager.ConfigureSession(sessionHoursListOptions);

            if (sessionHoursListOptions != null)
                sessionsManager.ConfigureSession(sessionFiltersOptions);

            return sessionsManager;
        }

        /// <summary>
        /// Add <see cref="SessionFilters"/> funcionality.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManagerBuilder UseSessionFilters(Action<SessionFiltersOptions> options)
        {
            // Create default options
            if (sessionFiltersOptions == null) 
                sessionFiltersOptions = new SessionFiltersOptions();

            // Add custom options
            options?.Invoke(sessionFiltersOptions);

            // Return the builder
            return this;
        }

        /// <summary>
        /// Add <see cref="SessionHoursList"/> funcionality.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManagerBuilder UseSessionHoursList(Action<SessionHoursListOptions> options)
        {
            // Create default options
            if (sessionHoursListOptions == null)
                sessionHoursListOptions = new SessionHoursListOptions();

            // Add custom options
            options?.Invoke(sessionHoursListOptions);

            // return the builder
            return this;
        }

        #endregion

    }
}
