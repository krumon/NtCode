using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    public class SessionStats : BaseSession
    {

        #region Private members

        private NinjaScriptBase ninjascript;

        private double rangeSum;
        private double rangeCount;

        #endregion

        #region Public properties

        public double RangeAvg => rangeSum / rangeCount;
        public double Range1Dv { get; set; }
        public double Range2Dv { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create default instance of <see cref="SessionStats"/> class.
        /// </summary>
        /// <param name="ninjascript">The ninjascript that help to get the stats.</param>
        public SessionStats(NinjaScriptBase ninjascript)
        {
            this.ninjascript = ninjascript;
        }

        #endregion

        #region Public methods

        public void OnBarUpdate()
        {
            rangeCount++;
            rangeSum += ninjascript.High[0] - ninjascript.Low[0];
        }

        #endregion


    }
}
