using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents all user trading sessionHoursList.
    /// </summary>
    public class TradingSessions
    {
        #region Private members

        /// <summary>
        /// Represents the generic sessionHoursList configuration.
        /// </summary>
        //private GenericSessionsConfigure genericSessionsConfigure;

        /// <summary>
        /// Represents the custom sessionHoursList configuration.
        /// </summary>
        //private CustomSessionsConfigure customSessionsConfigure;

        /// <summary>
        /// Represents the current sessionHoursList.
        /// </summary>
        //private SessionHours currentSessions;

        /// <summary>
        /// SessionHours store.
        /// </summary>
        //private List<SessionHours> sessions;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets true if any sessionHoursList are stored.
        /// </summary>
        //public bool HasSessionHours => sessions != null && sessions.Count > 0;

        /// <summary>
        /// Gets the total sessionHoursList stored.
        /// </summary>
        //public int Count => HasSessionHours ? sessions.Count : 0;

        /// <summary>
        /// Indicates if <see cref="TradingSessions"/> include generic sessionHoursList.
        /// </summary>
        //public bool IncludeGenericSessions { get; set; }

        /// <summary>
        /// Indicates if <see cref="TradingSessions"/> include custom sessionHoursList.
        /// </summary>
        //public bool IncludeCustomSessions { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of <see cref="TradingSessions"/> object.
        /// The session has been sessionHoursListIsConfigured by default.
        /// </summary>
        //public TradingSessions() //: this(null, null)
        //{
        //}

        /// <summary>
        /// Create a new instance of <see cref="TradingSessions"/> object with generic and custom configure objects.
        /// </summary>
        /// <param name="genericConfigure">The generic sessionHoursList configure.</param>
        /// <param name="customConfigure">The custom sessionHoursList configure.</param>
        //public GenericSessions(GenericSessionsConfigure genericConfigure, CustomSessionsConfigure customConfigure)
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
        /// <param name="configure">Specific sessionHoursList configure.</param>
        //public GenericSessions(ISessionsConfigure configure)
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
        /// <param name="genericConfigure">The generic sessionHoursList configure.</param>
        //public GenericSessions(GenericSessionsConfigure genericConfigure)
        //{
        //    if (IncludeGenericSessions)
        //        if (genericConfigure == null)
        //            this.genericSessionsConfigure = new GenericSessionsDefaultConfigure();
        //        else
        //            this.genericSessionsConfigure = genericConfigure;

        //}

        /// <summary>
        /// Create a new instance of <see cref="TradingSessions"/> object with custom ConfigureSessionHoursList object.
        /// </summary>
        /// <param name="customConfigure">The custom sessionHoursList configure.</param>
        //public GenericSessions(CustomSessionsConfigure customConfigure)
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
        //public virtual void OnTradingSessionsChanged(SessionChangedEventArgs e)
        //{
        //    currentSessions = new SessionHours(); // genericSessionsConfigure, customSessionsConfigure);
        //    currentSessions.AddValues(e);
        //    currentSessions.N = Count;
        //    if (sessions == null)
        //        sessions = new List<SessionHours>();
        //    sessions.Add(currentSessions);
        //}

        #endregion

        #region Public methods

        /// <summary>
        /// Add configure to the trading sessionHoursList.
        /// The configure can be added only when the configure doesn't exists.
        /// </summary>
        /// <param name="configure"></param>
        /// <exception cref="Exception"></exception>
        //public void AddConfigure(ISessionsConfigure configure)
        //{
        //    if (configure is GenericSessionsConfigure genericConfigure)
        //        if (genericSessionsConfigure != null)
        //            throw new Exception("The generic sessionHoursList configure exists. The configure can not be rewriter.");
        //        else
        //        {
        //            genericSessionsConfigure = genericConfigure;
        //            IncludeGenericSessions = true;
        //            return;
        //        }

        //    if (configure is CustomSessionsConfigure customConfigure)
        //        if (customSessionsConfigure != null)
        //            throw new Exception("The custom sessionHoursList configure exists. The configure can not be rewriter.");
        //        else
        //        {
        //            customSessionsConfigure = customConfigure;
        //            IncludeCustomSessions = true;
        //            return;
        //        }
        //}

        /// <summary>
        /// Represent a string with the last session stored.
        /// </summary>
        /// <returns>String of the last session stored.</returns>
        //public override string ToString()
        //{
        //    return HasSessionHours ? sessions[Count - 1].ToString() : "SessionHours list is empty.";
        //}

        #endregion

    }


}
