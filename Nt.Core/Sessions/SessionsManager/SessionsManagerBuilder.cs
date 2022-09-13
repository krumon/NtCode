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
        SessionFiltersOptions sessionFiltersOptions;

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
                })
                .ConfigureSession<SessionHoursList, SessionHoursListOptions>((options) =>
                {
                    options.MaxSessionsToStored = 100;
                })
                .ConfigureSession<SessionFilters, SessionFiltersOptions>(sessionFiltersOptions)
                .ConfigureSession<SessionFilters, SessionFiltersOptions>((filters) =>
                {
                    filters.UseDateFilters(
                        finalYear: 2022,
                        finalMonth: 9,
                        finalDay: 5
                        );
                });

            return sessionsManager;
        }

        /// <summary>
        /// Add <see cref="SessionFilters"/> funcionality.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManagerBuilder AddSessionFilters(Action<SessionFiltersOptions> options)
        {
            sessionFiltersOptions = new SessionFiltersOptions();
            options?.Invoke(sessionFiltersOptions);
            return this;
        }

        #endregion

    }
}
