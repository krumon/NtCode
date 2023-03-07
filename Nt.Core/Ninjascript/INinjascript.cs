using System;

namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// Represents properties and methods of ninjascript instance in ninjatrader plattform.
    /// </summary>
    public interface INinjascript : 
        IConfigurable, 
        IDisposable, 
        IRecalculableOnBarUpdate, 
        IRecalculableOnEachTick
    {

        /// <summary>
        /// Gets the delegate that print in the ninjatrader output window.
        /// </summary>
        Action<object> Print { get; }

        /// <summary>
        /// Gets methods thats clear the output window.
        /// </summary>
        Action ClearOutputWindow { get; }

    }
}
