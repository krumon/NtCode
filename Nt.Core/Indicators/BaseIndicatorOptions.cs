namespace Nt.Core
{
    /// <summary>
    /// Indicator configure options.
    /// </summary>
    public class BaseIndicatorOptions
    {

        #region Private members / Default value

		/// <summary>
		/// Indicates if the ninjascript draw in the price panel.
		/// </summary>
		private bool drawOnPricePanel = true;

		/// <summary>
		/// Indicates if the ninjscript draw the horizontal grid lines.
		/// </summary>
		private bool drawHorizontalGridLines = true;

        /// <summary>
        /// Indicates if the ninjscript draw the vertical grid lines.
        /// </summary>
        private bool drawVerticalGridLines = true;

        /// <summary>
        /// Indicates if the ninjscript paint the price markers.
        /// </summary>
        private bool paintPriceMarkers = true;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates if the ninjascript draw in the price panel.
        /// </summary>
        public bool DrawOnPricePanel { get { return drawOnPricePanel; } set { drawOnPricePanel = value; } }

        /// <summary>
        /// Indicates if the ninjscript draw the horizontal grid lines.
        /// </summary>
        public bool DrawHorizontalGridLines { get { return drawHorizontalGridLines; } set { drawHorizontalGridLines = value; } }

        /// <summary>
        /// Indicates if the ninjscript draw the vertical grid lines.
        /// </summary>
        public bool DrawVerticalGridLines { get { return drawVerticalGridLines; } set { drawVerticalGridLines = value; } }

        /// <summary>
        /// Indicates if the ninjscript paint the price markers.
        /// </summary>
        public bool PaintPriceMarkers { get { return paintPriceMarkers; } set { paintPriceMarkers = value; } }

        #endregion

    }
}
