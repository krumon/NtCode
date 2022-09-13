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
        ScriptProperties scriptProperties;
        SessionFiltersOptions sessionFiltersOptions;
        SessionHoursListOptions sessionHoursListOptions;

        #region Public methods

        /// <summary>
        /// Method to build the <see cref="SessionsManager"/> object.
        /// </summary>
        /// <returns>The <see cref="SessionsManager"/> object created by <see cref="SessionsManagerBuilder"/>.</returns>
        public SessionsManager Build(NinjaScriptBase ninjascript)
        {
            var sessionsManager = new SessionsManager();

            if (scriptProperties != null)
                sessionsManager.ConfigureProperties(ninjascript, scriptProperties);

            if (sessionFiltersOptions != null)
                sessionsManager.ConfigureSession(sessionHoursListOptions);

            if (sessionHoursListOptions != null)
                sessionsManager.ConfigureSession(sessionFiltersOptions);

            return sessionsManager;
        }

        /// <summary>
        /// Configure the ninjascript properties.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public SessionsManagerBuilder ConfigureProperties(Action<ScriptProperties> properties)
        {
            // Create default properties
            if (scriptProperties == null)
                scriptProperties = new ScriptProperties();

            // Add custom properties
            properties?.Invoke(scriptProperties);

            // Return the builder
            return this;
        }

        /// <summary>
        /// Add <see cref="SessionFilters"/> funcionality.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManagerBuilder UseSessionFilters(Action<SessionFiltersOptions> options)
        {
            // Create default properties
            if (sessionFiltersOptions == null) 
                sessionFiltersOptions = new SessionFiltersOptions();

            // Add custom properties
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
            // Create default properties
            if (sessionHoursListOptions == null)
                sessionHoursListOptions = new SessionHoursListOptions();

            // Add custom properties
            options?.Invoke(sessionHoursListOptions);

            // return the builder
            return this;
        }

        #endregion

    }
}
