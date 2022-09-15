using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript sessions
    /// </summary>
    public abstract class SessionBuilder<TSession, TOptions, TProperties>
        where TSession : BaseSession<TSession, TOptions, TProperties> , new()
        where TOptions : SessionOptions, new()
        where TProperties : SessionProperties, new()
    {
        #region Private members

        /// <summary>
        /// Store the session options and properties of session that uses.
        /// </summary>
        private Dictionary<Type, SessionConfigure<TOptions,TProperties>> configureDic = new Dictionary<Type, SessionConfigure<TOptions, TProperties>>();

        #endregion

        #region Protected members

        /// <summary>
        /// The <see cref="BaseSession{TSession, TOptions, TProperties}"/> options.
        /// </summary>
        protected TOptions options;

        /// <summary>
        /// The <see cref="BaseSession{TSession, TOptions, TProperties}"/> properties.
        /// </summary>
        protected TProperties properties;
        
        #endregion

        #region Public methods

        /// <summary>
        /// Method to build the <see cref="BaseSession{TSession, TOptions, TProperties}"/> object.
        /// </summary>
        /// <returns>The <see cref="BaseSession{TSession, TOptions, TProperties}"/> object created by <see cref="SessionBuilder{TSession, TOptions, TProperties}"/>.</returns>
        public TSession Build(NinjaScriptBase ninjascript)
        {
            // Create the session
            TSession session = new TSession();

            // Configure options and properties
            session.Configure(options, properties);

            // Set the default properties or the default actions of the session
            session.SetDefault(ninjascript);

            // Configure de "UseSessions"
            foreach (KeyValuePair<Type, SessionConfigure<TOptions, TProperties>> kvp in configureDic)
            {
                //var t = kvp.Key;
                //if  ((t).IsAssignableFrom(typeof(TSession)))
                //session.ConfigureUseSession(s,kvp.Value.Options, kvp.Value.Properties)
            }

            return session;
        }

        /// <summary>
        /// Configure the ninjascript properties.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public SessionBuilder<TSession, TOptions, TProperties> Configure(Action<TOptions, TProperties> config)
        {
            // Create default options
            if (options == null)
                options = new TOptions();

            // Create default properties
            if (properties == null)
                properties = new TProperties();

            // Add custom options and properties
            config?.Invoke(options,properties);

            // Return the builder
            return this;
        }

        /// <summary>
        /// Add <see cref="BaseSession{TSession, TOptions, TProperties}"/> funcionality.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public SessionBuilder<TSession,TOptions, TProperties> UseSession<T,P,O>(Action<O,P> action)
            where T : TSession, new()
            where O : TOptions, new()
            where P : TProperties, new()
        {
            if (action != null)
            {
                // Create variables of options and properties
                O op;
                P pp;

                if (configureDic.ContainsKey(typeof(T)))
                {
                    // Gets properties and options
                    op = (O)configureDic[typeof(T)].Options;
                    pp = (P)configureDic[typeof(T)].Properties;

                    // Set custom properties and options
                    action?.Invoke(op, pp);

                    // Create a new configure
                    var configure = new SessionConfigure<TOptions, TProperties>();

                    // Add properties and options to dictionary
                    configureDic.Add(typeof(T), configure);

                    configureDic[typeof(T)].Options = (O)op;
                    configureDic[typeof(T)].Properties = (P)pp;
                }
                else 
                { 
                    // Create default properties and options
                    op = new O();
                    pp = new P();

                    // Set custom properties and options
                    action?.Invoke(op, pp);

                    // Create a new configure
                    var configure = new SessionConfigure<TOptions, TProperties>();

                    // Add properties and options to dictionary
                    configureDic.Add(typeof(T), configure);

                    configureDic[typeof(T)].Options = (O)op;
                    configureDic[typeof(T)].Properties = (P)pp;
                }

            }

            // Return the builder
            return this;
        }

        #endregion

    }
}
