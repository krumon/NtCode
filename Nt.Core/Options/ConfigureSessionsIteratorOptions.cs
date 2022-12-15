using Nt.Core.Services;
using System;

namespace Nt.Core.Options
{
    /// <summary>
    /// Configure the <see cref="SessionsIteratorOptions"/> with delegate passed by parameter.
    /// </summary>
    public class ConfigureSessionsIteratorOptions : BaseOptions<SessionsIteratorOptions>
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
