using NinjaTrader.NinjaScript;
using System;
using System.CodeDom;
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
