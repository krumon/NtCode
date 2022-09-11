namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript indicator.
    /// </summary>
    public abstract class NtIndicator : NtScript
    {

        #region Public properties

        /// <summary>
        /// Indicates if the ninjascript draw in the price panel.
        /// </summary>
        public bool DrawOnPricePanel { get; private set; }

        /// <summary>
        /// Indicates if the ninjscript draw the horizontal grid lines.
        /// </summary>
        public bool DrawHorizontalGridLines { get; private set; }

        /// <summary>
        /// Indicates if the ninjscript draw the vertical grid lines.
        /// </summary>
        public bool DrawVerticalGridLines { get; private set; }

        /// <summary>
        /// Indicates if the ninjscript paint the price markers.
        /// </summary>
        public bool PaintPriceMarkers { get; private set; }

        #endregion

    }
}
