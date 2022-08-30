using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the user sessions collection.
    /// </summary>
    public class UserSessions
    {
        #region Private members

        private readonly GeneralSessionsConfigure generalSessionConfigure;

        /// <summary>
        /// The session hours structure core.
        /// </summary>
        private UserSession currentSession;

        /// <summary>
        /// <see cref="UserSession"/> sorted collection.
        /// </summary>
        private List<UserSession> sessions; 
        
        
        public bool HasSessions => sessions != null && sessions.Count > 0;

        /// <summary>
        /// The number of main sessions stored.
        /// </summary>
        public int Count => HasSessions ? sessions.Count : 0;

        #endregion

        #region Constructors

        public UserSessions()
        {
            this.generalSessionConfigure = new GeneralSessionsDefaultConfigure();
        }

        public UserSessions(GeneralSessionsConfigure configure)
        {
            if (configure == null)
                this.generalSessionConfigure = new GeneralSessionsDefaultConfigure();
            else
                this.generalSessionConfigure = configure;
        }

        #endregion

        #region Handler methods

        /// <summary>
        /// Changed any object or property when the session changed.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnUserSessionChanged(SessionChangedEventArgs e)
        {
            currentSession = new UserSession(generalSessionConfigure);
            currentSession.UpdateUserSessions(e);
            if (sessions == null)
                sessions = new List<UserSession>();
            sessions.Add(currentSession);
            currentSession.N = Count;
        }

        #endregion

        #region Public methods

        public override string ToString()
        {
            return HasSessions ? sessions[Count - 1].ToString() : "Sessions list is empty.";
        }

        #endregion
    }


}
