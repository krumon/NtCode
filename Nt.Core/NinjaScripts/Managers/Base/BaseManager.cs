using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Nt.Core
{

    /// <summary>
    /// Base class for any ninjascripts manager.
    /// </summary>
    /// <typeparam name="TManagerScript">The ninjascripts manager.</typeparam>
    /// <typeparam name="TManagerOptions">The ninjascripts manager options.</typeparam>
    public abstract class BaseManager<TManagerScript, TManagerOptions,TManagerBuilder> : BaseNinjascript<TManagerScript, TManagerOptions,TManagerBuilder>, IManager
        where TManagerScript : BaseManager<TManagerScript, TManagerOptions, TManagerBuilder>, IManager
        where TManagerOptions : BaseManagerOptions<TManagerOptions>, IManagerOptions
        where TManagerBuilder : BaseManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IManagerBuilder
    {

        #region Private members

        /// <summary>
        /// Nijascripts collection
        /// </summary>
        protected List<INinjascript> scripts;

        #endregion

        #region Public methods

        ///// <summary>
        ///// Add new script to the scripts collection
        ///// </summary>
        ///// <param name="script"></param>
        //public void Add<T>(T script)
        //    where T : INinjascript
        //{
        //    if (Scripts == null)
        //        Scripts = new List<INinjascript>();

        //    Scripts.Add(script);
        //}

        ///// <summary>
        ///// Remove script to the scripts collection.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="script"></param>
        //public void Remove<T>(T script)
        //    where T : INinjascript, new()
        //{
        //    if (Scripts == null)
        //        return;
        //    Scripts.Remove(script);
        //}

        /// <summary>
        /// Gets script to the scripts collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <returns></returns>
        public INinjascript Get<T>(T script)
            where T : INinjascript, new()
        {
            if (scripts != null)
                if (scripts.Contains(script))
                    return scripts[scripts.IndexOf(script)];
            
            return null;
        }

        /// <summary>
        /// Sets the ninjascripts collection from <see cref="BaseManagerBuilder"/> class.
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="methodName"></param>
        public void SetScripts(List<INinjascript> scripts, [CallerMemberName] string methodName = null)
        {
            if (methodName == nameof(IManagerBuilder.Build))
                this.scripts = scripts;
        }

        #endregion

    }

}
