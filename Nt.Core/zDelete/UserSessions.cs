using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    ///// <summary>
    ///// Represents the user children collection.
    ///// </summary>
    //public class UserSessions
    //{
    //    #region Private members

    //    private readonly GenericSessionsConfigure genericSessionsConfigure;

    //    /// <summary>
    //    /// The session hours structure core.
    //    /// </summary>
    //    private UserSession currentSessions;

    //    /// <summary>
    //    /// <see cref="UserSession"/> sorted collection.
    //    /// </summary>
    //    private List<UserSession> children; 
        
        
    //    public bool HasSessions => children != null && children.Count > 0;

    //    /// <summary>
    //    /// The number of main children stored.
    //    /// </summary>
    //    public int Count => HasSessions ? children.Count : 0;

    //    #endregion

    //    #region Constructors

    //    public UserSessions()
    //    {
    //        this.genericSessionsConfigure = new GenericSessionsDefaultConfigure();
    //    }

    //    public UserSessions(GenericSessionsConfigure configure)
    //    {
    //        if (configure == null)
    //            this.genericSessionsConfigure = new GenericSessionsDefaultConfigure();
    //        else
    //            this.genericSessionsConfigure = configure;
    //    }

    //    #endregion

    //    #region Handler methods

    //    /// <summary>
    //    /// Changed any object or property when the session changed.
    //    /// </summary>
    //    /// <param name="e"></param>
    //    public virtual void OnTradingSessionsChanged(SessionChangedEventArgs e)
    //    {
    //        currentSessions = new UserSession(genericSessionsConfigure);
    //        currentSessions.Init(e);
    //        if (children == null)
    //            children = new List<UserSession>();
    //        children.Add(currentSessions);
    //        currentSessions.N = Count;
    //    }

    //    #endregion

    //    #region Public methods

    //    public override string ToString()
    //    {
    //        return HasSessions ? children[Count - 1].ToString() : "Sessions list is empty.";
    //    }

    //    #endregion
    //}


}
