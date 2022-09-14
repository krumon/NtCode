using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// <see cref="SessionsManager"/> builder.
    /// </summary>
    public class SessionsManagerBuilder
    {
        /// <summary>
        /// Store the <see cref="ScriptProperties"/>.
        /// </summary>
        private ScriptProperties scriptProperties;

        /// <summary>
        /// Store the <see cref="SessionFiltersOptions"/>.
        /// </summary>
        private SessionFiltersOptions sessionFiltersOptions;

        /// <summary>
        /// Store the <see cref="SessionHoursListOptions"/>.
        /// </summary>
        private SessionHoursListOptions sessionHoursListOptions;

        /// <summary>
        /// Indicates when the client use <see cref="SessionFilters"/>.
        /// </summary>
        bool useSessionFilters;

        /// <summary>
        /// Indicates when the client use <see cref="SessionHoursList"/>.
        /// </summary>
        bool useSessionHoursList;

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

            if (sessionFiltersOptions != null || useSessionFilters)
                sessionsManager.ConfigureSession(sessionFiltersOptions);

            if (sessionHoursListOptions != null || useSessionHoursList)
                sessionsManager.ConfigureSession(sessionHoursListOptions);

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
            if (sessionFiltersOptions == null && options != null) 
                sessionFiltersOptions = new SessionFiltersOptions();

            // Add custom properties
            options?.Invoke(sessionFiltersOptions);

            // Update the flag
            useSessionFilters = true;

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
            if (sessionHoursListOptions == null && options != null)
                sessionHoursListOptions = new SessionHoursListOptions();

            // Add custom properties
            options?.Invoke(sessionHoursListOptions);

            // Update the flag
            useSessionHoursList = true;

            // return the builder
            return this;
        }

        #endregion

    }
}
