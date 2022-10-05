using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Nt.Core.Ninjascript
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
        protected List<INinjascript> scripts = new List<INinjascript>();

        #endregion

        #region Public methods

        /// <summary>
        /// Add elements to the manager collection in the order selected.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="ordereventType">The event type reference for sorted the collection.</param>
        public void Add(INinjascript script, EventType fromEvent = EventType.Configure)
        {
            // Add the first element
            if (scripts.Count == 0)
            {
                scripts.Add(script);
                return;
            }

            for (int i = 0; i < scripts.Count; i++)
            {
                if (script.GetOrder(fromEvent) >= scripts[i].GetOrder(fromEvent))
                {
                    if (i == scripts.Count - 1)
                    {
                        scripts.Add(script);
                        break;
                    }
                    continue;
                }
                
                scripts.Insert(i, script);
                break;
            }
        }

        /// <summary>
        /// Gets script to the scripts collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <returns></returns>
        public INinjascript Get<T>(T script)
            where T : INinjascript
        {
            if (scripts != null)
                if (scripts.Contains(script))
                    return scripts[scripts.IndexOf(script)];

            return null;
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
            if (scripts != null)
                foreach (var script in scripts)
                    if (typeof(T) == script.GetType())
                        return script;
            return null;
        }

        /// <summary>
        /// Gets all <see cref="INinjascript"/> objects if have the same type.
        /// </summary>
        /// <typeparam name="T">The type of the objects to get.</typeparam>
        /// <returns>List of <see cref="T"/> objects or empty list if they don't exists.</returns>
        public List<INinjascript> GetAll<T>()
            where T : INinjascript => 
            scripts.FindAll(x => x.GetType() == typeof(T));

        /// <summary>
        /// Find the first element of the <see cref="T"/> type 
        /// </summary>
        /// <typeparam name="T">The type of the element to find.</typeparam>
        /// <returns>The element finded or the default element value.</returns>
        public T FirstOrDefault<T>()
            where T : INinjascript
        {
            if(scripts != null && scripts.Count > 0)
                foreach (var script in scripts)
                    if (typeof(T) == script.GetType())
                        return (T)script;
            return default;
        }

        /// <summary>
        /// Returns if the type of element exists in the <see cref="INinjascript"/> collection
        /// </summary>
        /// <typeparam name="T">The type to find in the collection.</typeparam>
        /// <returns>True, if the element exist, otherwise false.</returns>
        public bool Contains<T>()
        {
            if(scripts != null && scripts.Count > 0)
                foreach (var script in scripts)
                    if (typeof(T) == script.GetType())
                        return true;
            return false;
        }

        /// <summary>
        /// Returns the index of the element of a specific type in the <see cref="INinjascript"/> collection.
        /// </summary>
        /// <typeparam name="T">The type of the element to find.</typeparam>
        /// <returns>The index of the element or -1 if the type of element doesn't exist in the <see cref="INinjascript"/>collection.</returns>
        public int IndexOf<T>()
        {
            if(scripts != null && scripts.Count > 0)
                for (int i = 0; i < scripts.Count; i++)
                    if (typeof(T) == scripts[i].GetType())
                        return i;
            return -1;
        }

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

        /// <summary>
        /// Clear the <see cref="INinjascript"/> collection.
        /// </summary>
        public override void Dispose()
        {
            scripts.Clear();
        }

        #endregion

    }

}
