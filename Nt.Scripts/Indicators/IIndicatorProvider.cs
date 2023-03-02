using Nt.Core.Hosting;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Represents a type that can create instances of <see cref="IIndicator"/>.
    /// </summary>
    public interface IIndicatorProvider : IDisposable
    {

        /// <summary>
        /// Creates a new <see cref="IIndicator"/> instance.
        /// </summary>
        /// <param name="indicatorName">The indicator name.</param>
        /// <returns>The instance of <see cref="IIndicator"/> that was created.</returns>
        IIndicator CreateLogger(string indicatorName);

    }
}
