using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System.Collections.Generic;

namespace Nt.Core
{

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options.</typeparam>
    public abstract class BaseNinjascriptsManager<TScript, TOptions, TManagerScript, TManagerOptions> : BaseNinjascript<TScript,TOptions>
        where TScript : BaseNinjascript<TScript, TOptions>, new()
        where TOptions : BaseNinjascriptOptions<TOptions>, new()
        where TManagerScript : BaseNinjascriptsManager<TScript, TOptions, TManagerScript, TManagerOptions>, new()
        where TManagerOptions : BaseNinjascriptsManagerOptions<TManagerOptions>, new()
    {
        #region Protected members

        /// <summary>
        /// Nijascripts collection
        /// </summary>
        protected List<TScript> scripts;

        #endregion

        #region Public properties


        #endregion

        #region State changed methods

        /// <summary>
        /// AddValues the <see cref="BaseNinjascript"/>.
        /// </summary>
        /// <param name="ninjascript">The parent ninjascript.</param>
        /// <param name="bars">The chart bars object.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Call parent method
            base.Load(ninjascript, bars);
        }

        #endregion

        #region Configure methods


        #endregion

        #region Builder methods


        #endregion

        #region Public methods

        /// <summary>
        /// Add new script to the scripts collection
        /// </summary>
        /// <param name="script"></param>
        public void Add<T>(T script)
            where T : TScript, new()
        {
            if (scripts == null)
                scripts = new List<TScript>();

            scripts.Add(script);
        }

        /// <summary>
        /// Remove script to the scripts collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        public void Remove<T>(T script)
            where T : TScript, new()
        {
            if (scripts == null)
                return;
            scripts.Remove(script);
        }

        /// <summary>
        /// Gets script to the scripts collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <returns></returns>
        public TScript Get<T>(T script)
            where T : TScript, new()
        {
            if (scripts == null)
                if (scripts.Contains(script))
                    return scripts[scripts.IndexOf(script)];
            
            return null;
        }

        #endregion

    }

}
