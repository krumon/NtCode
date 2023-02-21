using NinjaTrader.NinjaScript;
using System;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents properties and methods of <see cref="NinjaScriptBase"/>.
    /// </summary>
    public class Ninjascript : INinjascript
    {
        private readonly NinjaScriptBase _ninjascript;   

        /// <summary>
        /// Creates a new <see cref="Ninjascript"/> instance.
        /// </summary>
        /// <param name="ninjascript">The ninjascript that is running in the ninjatrader plattform.</param>
        /// <exception cref="ArgumentNullException">The ninjascript cannot be null.</exception>
        public Ninjascript(NinjaScriptBase ninjascript)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
        }

        // TODO: Delete this constructor. Is only necesary for testing in console.
        public Ninjascript()
        {
        }

        /// <summary>
        /// Gets the ninjascript instance.
        /// </summary>
        public NinjaScriptBase Instance => _ninjascript;

        /// <summary>
        /// Gets the <see cref="NinjaScript.State"/> of the ninjascript object.
        /// </summary>
        public State State => _ninjascript.State;

        /// <summary>
        /// Gets the delegate that print in the ninjatrader output window.
        /// </summary>
        public Action<object> Print => _ninjascript.Print;

        /// <summary>
        /// Gets methods thats clear the output window.
        /// </summary>
        public Action ClearOutputWindow => _ninjascript.ClearOutputWindow;

    }
}
