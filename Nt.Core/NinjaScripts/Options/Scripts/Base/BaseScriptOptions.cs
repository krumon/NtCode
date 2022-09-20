﻿using NinjaTrader.NinjaScript;
using System.Xml.Linq;

namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public class BaseScriptOptions<TOptions> : BaseNinjascriptOptions<TOptions>, IScriptOptions<TOptions>
        where TOptions : BaseScriptOptions<TOptions>, new()
    {

        #region Private members / Default values

        #endregion

        #region Properties
        
        // Declare Properties that not use in indicators and strategies. This properties haven't been hieralchical.

        #endregion

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public override void CopyTo(TOptions options)
        {
            // Copy parent properties
            base.CopyTo(options);

        }

        /// <summary>
        /// Copy options to ninjatrader properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public void CopyTo(NinjaScriptBase ninjascript)
        {
            CopyToNinjascript(ninjascript);
        }

        #endregion

    }

    /// <summary>
    /// The script options
    /// </summary>
    public class ScriptOptions : BaseScriptOptions<ScriptOptions>, IScriptOptions
    {
        public void CopyTo(IScriptOptions options)
        {
            base.CopyTo((ScriptOptions)options);
        }
    }

}
