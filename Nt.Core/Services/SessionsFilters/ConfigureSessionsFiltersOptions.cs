using Nt.Core.Options;
using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Configure the <see cref="SessionsFiltersOptions"/> with delegate passed by parameter.
    /// </summary>
    public class ConfigureSessionsFiltersOptions : ConfigureOptions<SessionsFiltersOptions>
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="action">Delegate for configure the <see cref="SessionsFiltersOptions"/>.</param>
        public ConfigureSessionsFiltersOptions(Action<SessionsFiltersOptions> action) : base(action)
        {
        }
    }
}
