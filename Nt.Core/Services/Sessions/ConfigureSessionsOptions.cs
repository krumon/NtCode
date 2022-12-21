using Nt.Core.Options;
using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Configure the <see cref="SessionsOptions"/> with delegate passed by parameter.
    /// </summary>
    public class ConfigureSessionsOptions : ConfigureOptions<SessionsOptions>
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="action">Delegate for configure the <see cref="SessionsOptions"/>.</param>
        public ConfigureSessionsOptions(Action<SessionsOptions> action) : base(action)
        {
        }
    }
}
