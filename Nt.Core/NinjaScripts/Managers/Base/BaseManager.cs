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
        //protected List<INinjascript> scripts;

        #endregion

        #region Public properties

        /// <summary>
        /// Nijascripts collection
        /// </summary>
        public List<INinjascript> Scripts { get;protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseManager{TManagerScript, TManagerOptions, TManagerBuilder}"/> default instance.
        /// </summary>
        protected BaseManager() : base()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Create a ninjascript default builder.
        /// </summary>
        /// <returns>Default instance of <see cref="IBuilder"/>.</returns>
        public override IBuilder CreateBuilder()
        {
            return (IBuilder)CreateManagerBuilder(Configuration, Scripts);
        }

        /// <summary>
        /// Create a ninjascript default builder.
        /// </summary>
        /// <returns>Default instance of <see cref="IBuilder"/>.</returns>
        public override TManagerBuilder CreateDefaultBuilder()
        {
            return CreateManagerBuilder(Configuration,Scripts);
        }


        /// <summary>
        /// Gets script to the scripts collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <returns></returns>
        public INinjascript Get<T>()
            where T : INinjascript
        {
            if (Scripts != null)
                foreach (var script in Scripts)
                    if (typeof(T) == script.GetType())
                        return script;
            
            return null;
        }

        ///// <summary>
        ///// Gets script to the scripts collection.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="script"></param>
        ///// <returns></returns>
        //public INinjascript Get<T>(T script)
        //    where T : INinjascript, new()
        //{
        //    if (scripts != null)
        //        if (scripts.Contains(script))
        //            return scripts[scripts.IndexOf(script)];
            
        //    return null;
        //}

        /// <summary>
        /// Sets the ninjascripts collection from <see cref="BaseManagerBuilder"/> class.
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="methodName"></param>
        public void SetScripts(List<INinjascript> scripts, [CallerMemberName] string methodName = null)
        {
            if (methodName == nameof(IManagerBuilder.Build) || methodName == "OnBuild")
                this.Scripts = scripts;
        }

        #endregion

    }

}
