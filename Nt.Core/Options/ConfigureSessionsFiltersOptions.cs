using Nt.Core.Services;
using System;

namespace Nt.Core.Options
{
    /// <summary>
    /// Configure the <see cref="SessionsFiltersOptions"/> with delegate passed by parameter.
    /// </summary>
    public class ConfigureSessionsFiltersOptions : BaseOptions<SessionsFiltersOptions>
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="action">Delegate for configure the <see cref="SessionsOption"/>.</param>
        public ConfigureSessionsFiltersOptions(Action<SessionsFiltersOptions> action) : base(action)
        {
        }
    }
}
