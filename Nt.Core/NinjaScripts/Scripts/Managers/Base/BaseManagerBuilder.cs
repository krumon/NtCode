using NinjaTrader.NinjaScript;
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

        public override INinjascript Build()
        {
            return Builder();
        }

        public override INinjascript Build(NinjaScriptBase ninjascript)
        {
            return Builder(ninjascript);
        }
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

        #region Private methods

        private TManagerScript Builder(NinjaScriptBase ninjascript = null)
        {
            // Create the script
            TManagerScript script = Activator.CreateInstance<TManagerScript>(); // new TScript();

            // Configure options
            script.Scripts = scripts;

            if (ninjascript != null)
                // Set the default properties or the default actions of the session
                script.SetDefault(ninjascript);

            // Return the script.
            return script;
        }

        #endregion

    }

}
