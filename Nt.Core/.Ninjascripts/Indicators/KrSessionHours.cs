using System.Collections.Generic;

namespace NtCore
{
    /// <summary>
    /// Represents the SessionHours Indicator Core.
    /// </summary>
    public class KrSessionHours : SessionHoursStructure
    {

        #region Private members

        /// <summary>
        /// The current session.
        /// </summary>
        protected SessionHoursStructure currentSession;

        /// <summary>
        /// Session sorted list.
        /// </summary>
        protected List<SessionHoursStructure> sortedSessionList = new List<SessionHoursStructure>();

        #endregion

        #region Public properties

        /// <summary>
        /// The number of session stored.
        /// </summary>
        public int N => sortedSessionList.Count;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a default instance of <see cref="KrSessionHours"/>.
        /// </summary>
        public KrSessionHours()
        {
        }

        #endregion

        #region Public methods

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public virtual void OnBarUpdate()
        {
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public virtual void OnMarketData()
        {
        }

        #endregion

        #region Private methods


        #endregion

        #region ToString Methods

        /// <summary>
        /// Returns the string with the number of the session, the begin time and the end time.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Session {N}: Begin: {currentSession.SessionBegin.ToString()} | End: {currentSession.SessionEnd.ToString()}";
        }

        #endregion

    }


}
