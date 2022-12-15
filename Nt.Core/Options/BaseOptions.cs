using Nt.Core.Services;
using System;

namespace Nt.Core.Options
{
    public abstract class BaseOptions<TOptions> : IOptions<TOptions>
        where TOptions : class
    {
        public Action<TOptions> Action { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="action">The action to register.</param>
        public BaseOptions(Action<TOptions> action)
        {
            Action = action;
        }

        public virtual void Configure(TOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            Action?.Invoke(options);

        }

    }
}
