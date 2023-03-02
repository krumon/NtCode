using Kr.Core.Helpers;
using System;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Delegates to a new <see cref="ILogger"/> instance using the full name of the given type, created by the
    /// provided <see cref="ILoggerFactory"/>.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class Indicator<T> : IIndicator<T>
    {
        private readonly IIndicator _indicator;

        /// <summary>
        /// Creates a new <see cref="Logger{T}"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public Indicator(IIndicatorFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _indicator = factory.CreateLogger(TypeNameHelper.GetTypeDisplayName(typeof(T), includeGenericParameters: false, nestedTypeDelimiter: '.'));
        }

        /// <inheritdoc />
        bool IIndicator.IsEnabled()
        {
            return _indicator.IsEnabled();
        }
    }
}
