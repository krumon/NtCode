using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// The base class to script builders
    /// </summary>
    public abstract class BaseManagerBuilder<TScript,TOptions> : BaseScriptBuilder<TScript,TOptions>
        where TScript : BaseNinjascriptsManager<TScript,TOptions>, new()
        where TOptions : BaseManagerOptions<TOptions>, new()
    {

        #region Protected members

        protected Dictionary<string,TOptions> scripts;

        #endregion

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <returns>The script object created by the builder.</returns>
        public override TScript Build(NinjaScriptBase ninjascript)
        {
            // Build the parent script
            return base.Build(ninjascript);
        }

        /// <summary>
        /// Add funcionality to the manager with any script.
        /// </summary>
        /// <typeparam name="Script">Script to add.</typeparam>
        /// <typeparam name="Options">Script options</typeparam>
        /// <param name="action">Delegate to execute any code into the Ninjatrader when you work in ninajtrader.</param>
        /// <returns></returns>
        public BaseManagerBuilder<TScript, TOptions> AddSession<Script,Options>(Action<Options> action)
            where Script : BaseScript<Script, Options>, new()
            where Options : BaseScriptOptions<Options>, new()
        {
            if (action != null)
            {
                // Create variables of options and type of script
                Options op = null;
                string script2string = (typeof(Script)).ToString();

                if (scripts.ContainsKey(script2string))
                {
                    // Gets the options
                    //op = scripts[script2string];

                    // Set custom properties and options
                    action?.Invoke(op);
                }
                else
                {
                    // Create default properties and options
                    op = new Options();

                    // Set custom properties and options
                    action?.Invoke(op);

                    // Add options to dictionary
                    //scripts.Add(script2string, op);
                    //scripts[script2string] = op;
                }

            }

            // Return the builder
            return this;
        }


        #endregion

    }
}
