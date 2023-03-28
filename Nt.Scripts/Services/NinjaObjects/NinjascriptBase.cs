using Kr.Core.Helpers;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents properties and methods of <see cref="NinjaTrader.NinjaScript.NinjaScriptBase"/>.
    /// </summary>
    public class NinjaScriptBase : INinjaScriptBase
    {
        private readonly NinjaTrader.NinjaScript.NinjaScriptBase _ninjascript;   

        /// <summary>
        /// Creates a new <see cref="NinjaScriptBase"/> instance.
        /// </summary>
        /// <param name="ninjascript">The ninjascript that is running in the ninjatrader plattform.</param>
        /// <exception cref="ArgumentNullException">The ninjascript cannot be null.</exception>
        public NinjaScriptBase(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            //Configure();
        }

        // TODO: Delete this constructor. Is only necesary for tests in console.
        internal NinjaScriptBase()
        {
            //Configure();
        }

        /// <summary>
        /// Gets the NinjaScript type name.
        /// </summary>
        public string TypeName => TypeNameHelper.GetTypeDisplayName(_ninjascript.GetType(),fullName: false, includeGenericParameters: false, nestedTypeDelimiter: '.');

        /// <summary>
        /// Gets the ninjascript instance.
        /// </summary>
        public NinjaTrader.NinjaScript.NinjaScriptBase Instance => _ninjascript;

        /// <summary>
        /// Gets the <see cref="NinjaScript.State"/> of the ninjascript object.
        /// </summary>
        public State State => _ninjascript?.State ?? State.Configure;

        /// <summary>
        /// Gets the delegate that print in the ninjatrader output window.
        /// </summary>
        public Action<object> Print 
        {
            get
            {
                if (_ninjascript != null)
                    return _ninjascript.Print;

                return Console.WriteLine;
            }
        }

        /// <summary>
        /// Gets methods thats clear the output window.
        /// </summary>
        public Action ClearOutputWindow
        {
            get
            {
                if (_ninjascript != null)
                    return _ninjascript.ClearOutputWindow;

                return Console.Clear;
            }
        }

        //protected virtual void Configure()
        //{
        //    Instance = _ninjascript;
        //}
    }
}
