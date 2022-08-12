namespace Nt.Core
{
    /// <summary>
    /// Base class for any session in ninjatrader.
    /// </summary>
    public class NsSession
    {

        #region Fields

        protected NsSessionHours sessionHours;

        #endregion

        #region Properties

        public NsSessionHours SessionHours
        {
            get { return sessionHours; }
        }

        #endregion

        #region Constructors

        public NsSession()
        {

            //sessionHours = KrSessionHours
            //    .CreateSessionHours(TradingSession.Electronic)
            //    .AddDefaultSessions();
            //sessionHours.AddDefaultSessions();

        }

        #endregion

        #region Public methods


        #endregion

        #region Private methods


        #endregion

        #region ToString Methods



        #endregion
    }
}
