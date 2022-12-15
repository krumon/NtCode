using Nt.Core.Services;
using System;

namespace Nt.Core.Options
{
    /// <summary>
    /// Configure the <see cref="SessionsOptions"/> with delegate passed by parameter.
    /// </summary>
    public class ConfigureSessionsOptions : BaseOptions<SessionsOptions>
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="action">Delegate for configure the <see cref="SessionsOption"/>.</param>
        public ConfigureSessionsOptions(Action<SessionsOptions> action) : base(action)
        {
        }
    }
}
