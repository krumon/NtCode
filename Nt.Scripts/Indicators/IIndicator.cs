﻿namespace Nt.Scripts.Indicators
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

    /// <summary>
    /// A generic interface for indicators where the indicator name is derived from the specified
    /// TIndicatorName type name.
    /// </summary>
    /// <typeparam name="TIndicatorName">The type whose name is used for the indicator name.</typeparam>
    public interface IIndicator<out TIndicatorName> : IIndicator
    {
    }

}
