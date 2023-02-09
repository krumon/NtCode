using NinjaTrader.NinjaScript;
using System;

namespace Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// Represents properties and methods of <see cref="NinjaScriptBase"/>.
    /// </summary>
    public interface INinjascript
    {
        /// <summary>
        /// Gets the ninjascript instance.
        /// </summary>
        NinjaScriptBase Instance { get; }

        /// <summary>
        /// Gets the <see cref="NinjaScript.State"/> of the ninjascript object.
        /// </summary>
        State State { get; }

        /// <summary>
        /// Gets the delegate that print in the ninjatrader output window.
        /// </summary>
        Action<object> Print { get; }

    }
}
