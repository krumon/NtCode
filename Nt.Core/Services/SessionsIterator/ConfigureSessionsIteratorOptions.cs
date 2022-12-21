using Nt.Core.Options;
using Nt.Core.Services;
using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Configure the <see cref="SessionsIteratorOptions"/> with delegate passed by parameter.
    /// </summary>
    public class ConfigureSessionsIteratorOptions : ConfigureOptions<SessionsIteratorOptions>
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="action">Delegate for configure the <see cref="SessionsIteratorOptions"/>.</param>
        public ConfigureSessionsIteratorOptions(Action<SessionsIteratorOptions> action) : base(action)
        {
        }
    }
}
