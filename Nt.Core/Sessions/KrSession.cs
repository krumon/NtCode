namespace NtCore
{
    /// <summary>
    /// Base class for any session in ninjatrader.
    /// </summary>
    public class KrSession
    {

        #region Fields

        protected KrSessionHours sessionHours;

        #endregion

        #region Properties

        public KrSessionHours SessionHours
        {
            get { return sessionHours; }
        }

        #endregion

        #region Constructors

        public KrSession()
        {

            sessionHours = KrSessionHours
                .CreateSessionHours(TradingSession.Electronic)
                .AddDefaultSessions();
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
