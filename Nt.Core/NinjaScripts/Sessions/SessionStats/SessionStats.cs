using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    // TODO: Cambiar los parámetros genéricos
    public class SessionStats : BaseSession<SessionStats, SessionStatsOptions,SessionStatsBuilder>
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

        /// <summary>
        /// Creates <see cref="SessionStats"/> default instance.
        /// </summary>
        protected SessionStats() : base()
        {
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
