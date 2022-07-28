namespace NtCore
{
    /// <summary>
    /// Base class for any session in ninjatrader.
    /// </summary>
    public class NtSession
    {

        #region Fields

        protected SessionHours sessionHours;

        #endregion

        #region Properties

        public SessionHours SessionHours
        {
            get { return sessionHours; }
        }

        #endregion

        #region Constructors

        public NtSession()
        {
            sessionHours = SessionHours.CreateMundialSessions();
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
