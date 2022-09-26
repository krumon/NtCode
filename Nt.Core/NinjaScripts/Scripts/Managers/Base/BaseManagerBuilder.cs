using NinjaTrader.NinjaScript;
using System;
using System.CodeDom;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascripts manager builders.
    /// </summary>
    public abstract class BaseManagerBuilder<TManagerScript, TManagerOptions, TManagerBuilder> : BaseBuilder<TManagerScript, TManagerOptions, TManagerBuilder>, IManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerScript : BaseManager<TManagerScript, TManagerOptions,TManagerBuilder>, new()
        where TManagerOptions : BaseManagerOptions<TManagerOptions>, new()
        where TManagerBuilder : BaseManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, new()
    {
        #region Private members

        private List<ISession> scripts = new List<ISession>();

        #endregion

        #region Public methods

        public TManagerBuilder Add<T>()
            where T : ISession, new()
        {
            ISession session = Activator.CreateInstance<T>();
            
            
            return (TManagerBuilder)this;
        }


        #endregion

    }

}
