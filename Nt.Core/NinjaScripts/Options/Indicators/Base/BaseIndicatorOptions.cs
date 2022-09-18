using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The indicator options
    /// </summary>
    public abstract class BaseIndicatorOptions<TOptions> : BaseNinjascriptOptions<TOptions>
        where TOptions : BaseIndicatorOptions<TOptions>, new()
    {

        #region Private members / Default value

        /// <summary>
        /// Indicates if the data is overlay in the graphics.
        /// </summary>
        private bool isOverlay = false;

        /// <summary>
        /// Indicates if the ninjascript display in the data box.
        /// </summary>
        private bool displayInDataBox = true;

        /// <summary>
        /// Represents the scale justification of the chart.
        /// </summary>
        private ScaleJustification scaleJustification = ScaleJustification.Right;

        //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
        //See Help Guide for additional information.
        /// <summary>
        /// Indicates if the ninjascript is suspended when the ninjascript is inactive.
        /// </summary>
        private bool isSuspendedWhileInactive = true;

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
        /// Indicates if the data is overlay in the graphics.
        /// </summary>
        public bool IsOverlay { get { return isOverlay; } set { isOverlay = value; } }

        /// <summary>
        /// Indicates if the ninjascript display in the data box.
        /// </summary>
        public bool DisplayInDataBox { get { return displayInDataBox; } set { displayInDataBox = value; } }

        /// <summary>
        /// Represents the scale justification of the chart.
        /// </summary>
        public ScaleJustification ScaleJustification { get { return scaleJustification; } set { scaleJustification = value; } }

        //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
        //See Help Guide for additional information.
        /// <summary>
        /// Indicates if the ninjascript is suspended when the ninjascript is inactive.
        /// </summary>
        public bool IsSuspendedWhileInactive { get { return isSuspendedWhileInactive; } set { isSuspendedWhileInactive = value; } }

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

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public override void CopyTo(TOptions options)
        {
            // Sets the parent options values.
            base.CopyTo(options);

            options.IsOverlay = IsOverlay;
            options.DisplayInDataBox = DisplayInDataBox;
            options.ScaleJustification = ScaleJustification;
            options.IsSuspendedWhileInactive = IsSuspendedWhileInactive;
            options.DrawOnPricePanel = DrawOnPricePanel;
            options.DrawHorizontalGridLines = DrawHorizontalGridLines;
            options.DrawVerticalGridLines = DrawVerticalGridLines;
            options.PaintPriceMarkers = PaintPriceMarkers;
        }

        /// <summary>
        /// Copy options to ninjatrader indicator properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public void CopyTo(IndicatorBase ninjascript)
        {
            // Ninjascript properties
            ninjascript.Name = Name;
            ninjascript.Calculate = Calculate;
            ninjascript.BarsRequiredToPlot = BarsRequiredToPlot;
            // Indicator properties
            ninjascript.IsOverlay = IsOverlay;
            ninjascript.DisplayInDataBox = DisplayInDataBox;
            ninjascript.ScaleJustification = ScaleJustification;
            ninjascript.DrawHorizontalGridLines = DrawHorizontalGridLines;
            ninjascript.DrawVerticalGridLines = DrawVerticalGridLines;
            ninjascript.DrawOnPricePanel = DrawOnPricePanel;
            ninjascript.PaintPriceMarkers = PaintPriceMarkers;
        }

        #endregion

    }
}
