using System;
using System.Collections.Generic;
using System.Reflection;
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
        public void Add (INinjascript script)
        {
            if(scripts == null)
                scripts = new List<INinjascript> ();
            scripts.Add(script);
        }

        #region Private members

        /// <summary>
        /// Nijascripts collection
        /// </summary>
        protected List<INinjascript> scripts;

        #endregion

        #region Public properties

        /// <summary>
        /// Nijascripts collection
        /// </summary>
        public List<INinjascript> Scripts
        {
            get 
            { 
                if (scripts == null)
                    return new List<INinjascript>();

                return scripts; 
            }
        }

        #endregion

        #region Constructors

        ///// <summary>
        ///// Creates <see cref="BaseManager{TManagerScript, TManagerOptions, TManagerBuilder}"/> default instance.
        ///// </summary>
        //protected BaseManager() : base()
        //{
        //}

        #endregion

        #region Public methods

        ///// <summary>
        ///// Creates ninjascript builder to construct the script.
        ///// </summary>
        ///// <typeparam name="Script">The ninhascript to construct.</typeparam>
        ///// <typeparam name="Builder">The ninjascript builder.</typeparam>
        ///// <returns>The ninjascript builder.</returns>
        ///// <exception cref="Exception">Any type is wrong.</exception>
        //public override Builder CreateBuilder<Script, Builder>()
        //{
        //    return (Builder)CreateBuilderInstance(Configuration, Scripts);
        //}

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
            if (methodName == "BaseManagerBuilder" || methodName == "Configure" || methodName == "Build" || methodName == "Add")
                this.scripts = scripts;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a new <see cref="IBuilder"/> instance.
        /// </summary>
        /// <returns>The constructor can not be invoke.</returns>
        protected IManagerBuilder CreateManagerBuilderInstance()
        {
            ConstructorInfo construct = typeof(TManagerBuilder).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(TManagerScript) }, null);
            if (construct != null)
                return (TManagerBuilder)construct.Invoke(new object[] { this });
            else
                throw new NullReferenceException();
        }


        #endregion

    }

}
