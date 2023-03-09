using NinjaTrader.NinjaScript;
using System;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents properties and methods of <see cref="NinjaScriptBase"/>.
    /// </summary>
    public class NinjascriptBase : INinjascriptBase
    {
        private readonly NinjaScriptBase _ninjascript;   

        /// <summary>
        /// Creates a new <see cref="NinjascriptBase"/> instance.
        /// </summary>
        /// <param name="ninjascript">The ninjascript that is running in the ninjatrader plattform.</param>
        /// <exception cref="ArgumentNullException">The ninjascript cannot be null.</exception>
        public NinjascriptBase(NinjaScriptBase ninjascript)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            Configure();
        }

        // TODO: Delete this constructor. Is only necesary for tests in console.
        internal NinjascriptBase()
        {
            Configure();
        }

        /// <summary>
        /// Gets the ninjascript instance.
        /// </summary>
        public NinjaScriptBase Instance { get; protected set; }

        /// <summary>
        /// Gets the <see cref="NinjaScript.State"/> of the ninjascript object.
        /// </summary>
        public State State { get; protected set; }

        /// <summary>
        /// Gets the delegate that print in the ninjatrader output window.
        /// </summary>
        public Action<object> Print { get; protected set; }

        /// <summary>
        /// Gets methods thats clear the output window.
        /// </summary>
        public Action ClearOutputWindow { get; protected set; }

        protected virtual void Configure()
        {
            Instance = _ninjascript;
            State = _ninjascript.State;
            Print = _ninjascript.Print;
            ClearOutputWindow = _ninjascript.ClearOutputWindow;
        }
    }
}
