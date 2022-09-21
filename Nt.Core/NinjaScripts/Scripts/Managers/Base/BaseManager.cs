using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System.Collections.Generic;

namespace Nt.Core
{

    /// <summary>
    /// Base class for any ninjascripts manager.
    /// </summary>
    /// <typeparam name="TManagerScript">The ninjascripts manager.</typeparam>
    /// <typeparam name="TManagerOptions">The ninjascripts manager options.</typeparam>
    public abstract class BaseManager<TManagerScript, TManagerOptions> : BaseNinjascript<TManagerScript, TManagerOptions>
        where TManagerScript : BaseManager<TManagerScript, TManagerOptions>, new()
        where TManagerOptions : BaseManagerOptions<TManagerOptions>, new()
    {
        #region Protected members

        /// <summary>
        /// Nijascripts collection
        /// </summary>
        protected List<INinjascript> scripts;

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

        #region Public methods

        /// <summary>
        /// Add new script to the scripts collection
        /// </summary>
        /// <param name="script"></param>
        public void Add<T>(T script)
            where T : INinjascript, new()
        {
            if (scripts == null)
                scripts = new List<INinjascript>();

            scripts.Add(script);
        }

        /// <summary>
        /// Remove script to the scripts collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        public void Remove<T>(T script)
            where T : INinjascript, new()
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
        public INinjascript Get<T>(T script)
            where T : INinjascript, new()
        {
            if (scripts == null)
                if (scripts.Contains(script))
                    return scripts[scripts.IndexOf(script)];
            
            return null;
        }

        #endregion

    }

}
