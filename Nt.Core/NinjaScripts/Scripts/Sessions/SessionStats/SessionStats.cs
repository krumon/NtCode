using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    // TODO: Cambiar los parámetros genéricos
    public class SessionStats : BaseSession<SessionStats, SessionStatsOptions>
    {

        #region Private members

        private double rangeSum;
        private double rangeCount;

        #endregion

        #region Public properties

        public double RangeAvg => rangeSum / rangeCount;
        public double Range1Dv { get; set; }
        public double Range2Dv { get; set; }

        #endregion

        #region Constructors

        public SessionStats()
        {

        }

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

        public override void OnBarUpdate()
        {
            base.OnBarUpdate();
            rangeCount++;
            rangeSum += ninjascript.High[0] - ninjascript.Low[0];
        }

        #endregion


    }
}
