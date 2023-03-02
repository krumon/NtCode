namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Represents a type used to configure the indicators system and create instances of
    /// <see cref="IIndicator"/> from the registered <see cref="IIndicatorProvider"/>.
    /// </summary>
    public interface IIndicatorFactory
    {
        /// <summary>
        /// Creates a new <see cref="IIndicator"/> instance.
        /// </summary>
        /// <param name="indicatorName">The name of indicator.</param>
        /// <returns>The <see cref="IIndicator"/>.</returns>
        IIndicator CreateLogger(string indicatorName);

        /// <summary>
        /// Adds an <see cref="IIndicatorProvider"/> to the indicator system.
        /// </summary>
        /// <param name="provider">The <see cref="IIndicatorProvider"/>.</param>
        void AddProvider(IIndicatorProvider provider);

    }
}
