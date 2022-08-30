using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents all user trading sessions.
    /// </summary>
    public class TradingSessions
    {
        #region Private members

        /// <summary>
        /// Represents the generic sessions configuration.
        /// </summary>
        private GenericSessionsConfigure genericSessionsConfigure;

        /// <summary>
        /// Represents the custom sessions configuration.
        /// </summary>
        private CustomSessionsConfigure customSessionsConfigure;

        /// <summary>
        /// Represents the current sessions.
        /// </summary>
        private Sessions currentSessions;

        /// <summary>
        /// Sessions store.
        /// </summary>
        private List<Sessions> sessions;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets true if any sessions are stored.
        /// </summary>
        public bool HasSessions => sessions != null && sessions.Count > 0;

        /// <summary>
        /// Gets the total sessions stored.
        /// </summary>
        public int Count => HasSessions ? sessions.Count : 0;

        /// <summary>
        /// Indicates if <see cref="TradingSessions"/> include generic sessions.
        /// </summary>
        public bool IncludeGenericSessions { get; set; }

        /// <summary>
        /// Indicates if <see cref="TradingSessions"/> include custom sessions.
        /// </summary>
        public bool IncludeCustomSessions { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of <see cref="TradingSessions"/> object.
        /// The session has been configured by default.
        /// </summary>
        public TradingSessions() //: this(null, null)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="TradingSessions"/> object with generic and custom configure objects.
        /// </summary>
        /// <param name="genericConfigure">The generic sessions configure.</param>
        /// <param name="customConfigure">The custom sessions configure.</param>
        //public TradingSessions(GenericSessionsConfigure genericConfigure, CustomSessionsConfigure customConfigure)
        //{
        //    if (IncludeGenericSessions)
        //        if (genericConfigure == null)
        //            this.genericSessionsConfigure = new GenericSessionsDefaultConfigure();
        //        else
        //            this.genericSessionsConfigure = genericConfigure;

        //    if (IncludeCustomSessions)
        //        if (customConfigure == null)
        //            this.customSessionsConfigure = new CustomSessionsDefaultConfigure();
        //        else
        //            this.customSessionsConfigure = customConfigure;
        //}

        /// <summary>
        /// Create a new instance of <see cref="TradingSessions"/> object with generic and custom configure objects.
        /// </summary>
        /// <param name="configure">Specific sessions configure.</param>
        //public TradingSessions(ISessionsConfigure configure)
        //{
        //    if (IncludeGenericSessions && configure == null)
        //    {
        //        genericSessionsConfigure = new GenericSessionsDefaultConfigure();
        //        return;
        //    }
        //    else if (IncludeGenericSessions && configure is GenericSessionsConfigure genericConfigure)
        //    {
        //        genericSessionsConfigure = genericConfigure;
        //        return;
        //    }
        //    else if (IncludeCustomSessions && configure == null)
        //    {
        //        customSessionsConfigure = new CustomSessionsDefaultConfigure();
        //        return;
        //    }
        //    else if (IncludeCustomSessions && configure is CustomSessionsConfigure customConfigure)
        //    {
        //        customSessionsConfigure = customConfigure;
        //        return;
        //    }
        //}

        /// <summary>
        /// Create a new instance of <see cref="TradingSessions"/> object with generic configure object.
        /// </summary>
        /// <param name="genericConfigure">The generic sessions configure.</param>
        //public TradingSessions(GenericSessionsConfigure genericConfigure)
        //{
        //    if (IncludeGenericSessions)
        //        if (genericConfigure == null)
        //            this.genericSessionsConfigure = new GenericSessionsDefaultConfigure();
        //        else
        //            this.genericSessionsConfigure = genericConfigure;

        //}

        /// <summary>
        /// Create a new instance of <see cref="TradingSessions"/> object with custom Configure object.
        /// </summary>
        /// <param name="customConfigure">The custom sessions configure.</param>
        //public TradingSessions(CustomSessionsConfigure customConfigure)
        //{
        //    if (IncludeCustomSessions)
        //        if (customConfigure == null)
        //            this.customSessionsConfigure = new CustomSessionsDefaultConfigure();
        //        else
        //            this.customSessionsConfigure = customConfigure;
        //}

        #endregion

        #region Handler methods

        /// <summary>
        /// Changed any object or property when the session changed.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnTradingSessionsChanged(SessionChangedEventArgs e)
        {
            currentSessions = new Sessions(); // genericSessionsConfigure, customSessionsConfigure);
            currentSessions.Init(e);
            currentSessions.N = Count;
            if (sessions == null)
                sessions = new List<Sessions>();
            sessions.Add(currentSessions);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add configure to the trading sessions.
        /// The configure can be added only when the configure doesn't exists.
        /// </summary>
        /// <param name="configure"></param>
        /// <exception cref="Exception"></exception>
        public void AddConfigure(ISessionsConfigure configure)
        {
            if (configure is GenericSessionsConfigure genericConfigure)
                if (genericSessionsConfigure != null)
                    throw new Exception("The generic sessions configure exists. The configure can not be rewriter.");
                else
                {
                    genericSessionsConfigure = genericConfigure;
                    IncludeGenericSessions = true;
                    return;
                }

            if (configure is CustomSessionsConfigure customConfigure)
                if (customSessionsConfigure != null)
                    throw new Exception("The custom sessions configure exists. The configure can not be rewriter.");
                else
                {
                    customSessionsConfigure = customConfigure;
                    IncludeCustomSessions = true;
                    return;
                }
        }

        /// <summary>
        /// Represent a string with the last session stored.
        /// </summary>
        /// <returns>String of the last session stored.</returns>
        public override string ToString()
        {
            return HasSessions ? sessions[Count - 1].ToString() : "Sessions list is empty.";
        }

        #endregion

    }


}
