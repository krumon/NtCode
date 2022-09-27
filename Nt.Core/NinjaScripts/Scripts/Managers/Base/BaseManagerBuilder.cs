using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascripts manager builders.
    /// </summary>
    public abstract class BaseManagerBuilder<TManagerScript, TManagerOptions, TManagerBuilder> : BaseBuilder<TManagerScript, TManagerOptions, TManagerBuilder>, IManagerBuilder
        where TManagerScript : BaseManager<TManagerScript, TManagerOptions,TManagerBuilder>, IManager
        where TManagerOptions : BaseManagerOptions<TManagerOptions>, IManagerOptions
        where TManagerBuilder : BaseManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IManagerBuilder
    {
        #region Private members

        private List<INinjascript> scripts = new List<INinjascript>();

        #endregion

        #region Public methods

        public IManagerBuilder Add<Script, Options>(Action<Options> options)
            where Script : INinjascript
        {
            var script = Activator.CreateInstance<Script>().CreateBuilder().Configure<Script,Options>(options).Build();

            if (scripts == null)
                scripts = new List<INinjascript>();

            scripts.Add(script);

            return this;

        }


        public TManagerBuilder Add<T>()
            where T : ISession, new()
        {
            ISession session = Activator.CreateInstance<T>();
            
            
            return (TManagerBuilder)this;
        }


        #endregion

    }

}
