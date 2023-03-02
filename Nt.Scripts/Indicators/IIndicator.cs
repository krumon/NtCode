namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Represents an indicator to be used in ninjatrader plattform. 
    /// </summary>
    public interface IIndicator
    {
        /// <summary>
        /// Checks if the indicator is enabled.
        /// </summary>
        /// <returns>true if enabled.</returns>
        bool IsEnabled();

    }
}
