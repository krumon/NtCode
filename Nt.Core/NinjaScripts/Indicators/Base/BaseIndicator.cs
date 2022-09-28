using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript indicator.
    /// </summary>
    public abstract class BaseIndicator<TScript, TOptions,TBuilder> : BaseNinjascript<TScript, TOptions, TBuilder>, IIndicator
        where TScript : BaseIndicator<TScript, TOptions,TBuilder>, IIndicator
        where TOptions : BaseIndicatorOptions<TOptions>, IIndicatorOptions
        where TBuilder : BaseIndicatorBuilder<TScript,TOptions,TBuilder>, IIndicatorBuilder
    {

        #region Configure methods

        public override void SetDefault(NinjaScriptBase ninjascript)
        {
            // Copy the ninjascript properties in the parent
            base.SetDefault(ninjascript);

            // Cast the element to IndicatorBase
            IndicatorBase indicator = (IndicatorBase)ninjascript;

            // Copy the indicator properties
            indicator.IsOverlay = Configuration.IsOverlay;
            indicator.DisplayInDataBox = Configuration.DisplayInDataBox;
            indicator.ScaleJustification = Configuration.ScaleJustification;
            indicator.DrawHorizontalGridLines = Configuration.DrawHorizontalGridLines;
            indicator.DrawVerticalGridLines = Configuration.DrawVerticalGridLines;
            indicator.DrawOnPricePanel = Configuration.DrawOnPricePanel;
            indicator.PaintPriceMarkers = Configuration.PaintPriceMarkers;
        }


        #endregion
    }
}
