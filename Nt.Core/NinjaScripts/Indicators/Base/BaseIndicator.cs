using NinjaTrader.NinjaScript;

namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// Base class for any ninjascript indicator.
    /// </summary>
    public abstract class BaseIndicator<TScript, TOptions,TBuilder> : BaseNinjascript<TScript, TOptions, TBuilder>, IIndicator
        where TScript : BaseIndicator<TScript, TOptions,TBuilder>, IIndicator
        where TOptions : BaseIndicatorOptions<TOptions>, IIndicatorOptions
        where TBuilder : BaseIndicatorBuilder<TScript,TOptions,TBuilder>, IIndicatorBuilder
    {

        #region Constructors

        ///// <summary>
        ///// Creates <see cref="BaseIndicator{TScript, TOptions, TBuilder}"/> default instance.
        ///// </summary>
        //protected BaseIndicator() : base()
        //{
        //}

        #endregion

        #region Configure methods

        protected override void SetDefault(NinjaScriptBase ninjascript)
        {
            // Copy the ninjascript properties in the parent
            base.SetDefault(ninjascript);

            // Cast the element to IndicatorBase
            IndicatorBase indicator = (IndicatorBase)ninjascript;

            // Copy the indicator properties
            indicator.IsOverlay = configuration.IsOverlay;
            indicator.DisplayInDataBox = configuration.DisplayInDataBox;
            indicator.ScaleJustification = configuration.ScaleJustification;
            indicator.DrawHorizontalGridLines = configuration.DrawHorizontalGridLines;
            indicator.DrawVerticalGridLines = configuration.DrawVerticalGridLines;
            indicator.DrawOnPricePanel = configuration.DrawOnPricePanel;
            indicator.PaintPriceMarkers = configuration.PaintPriceMarkers;
        }


        #endregion
    }
}
