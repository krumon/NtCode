﻿using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    ///// <summary>
    ///// Represents the user sessionHoursList collection.
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
    //    private List<UserSession> sessionHoursList; 
        
        
    //    public bool HasSessionHours => sessionHoursList != null && sessionHoursList.Count > 0;

    //    /// <summary>
    //    /// The number of main sessionHoursList stored.
    //    /// </summary>
    //    public int Count => HasSessionHours ? sessionHoursList.Count : 0;

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
    //    public virtual void OnSessionHoursChanged(SessionChangedEventArgs e)
    //    {
    //        currentSessions = new UserSession(genericSessionsConfigure);
    //        currentSessions.AddValues(e);
    //        if (sessionHoursList == null)
    //            sessionHoursList = new List<UserSession>();
    //        sessionHoursList.Add(currentSessions);
    //        currentSessions.N = Count;
    //    }

    //    #endregion

    //    #region Public methods

    //    public override string ToString()
    //    {
    //        return HasSessionHours ? sessionHoursList[Count - 1].ToString() : "SessionHours list is empty.";
    //    }

    //    #endregion
    //}


}
