using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript indicator.
    /// </summary>
    public abstract class BaseIndicator<TScript, TOptions> : BaseNinjascript<TScript, TOptions>
        where TScript : BaseIndicator<TScript, TOptions>, new()
        where TOptions : BaseIndicatorOptions<TOptions>, new()
    {

        #region Configure properties

        ///// <summary>
        ///// Indicates if the data is overlay in the graphics.
        ///// </summary>
        //public bool IsOverlay { get; private set; }

        ///// <summary>
        ///// Indicates if the ninjascript display in the data box.
        ///// </summary>
        //public bool DisplayInDataBox { get; private set; }

        ///// <summary>
        ///// Represents the scale justification of the chart.
        ///// </summary>
        //public ScaleJustification ScaleJustification { get; private set; }

        ////Disable this property if your indicator requires custom values that cumulate with each new market data event. 
        ////See Help Guide for additional information.
        ///// <summary>
        ///// Indicates if the ninjascript is suspended when the ninjascript is inactive.
        ///// </summary>
        //public bool IsSuspendedWhileInactive { get; private set; }

        ///// <summary>
        ///// Indicates if the ninjascript draw in the price panel.
        ///// </summary>
        //public bool DrawOnPricePanel { get; private set; }

        ///// <summary>
        ///// Indicates if the ninjscript draw the horizontal grid lines.
        ///// </summary>
        //public bool DrawHorizontalGridLines { get; private set; }

        ///// <summary>
        ///// Indicates if the ninjscript draw the vertical grid lines.
        ///// </summary>
        //public bool DrawVerticalGridLines { get; private set; }

        ///// <summary>
        ///// Indicates if the ninjscript paint the price markers.
        ///// </summary>
        //public bool PaintPriceMarkers { get; private set; }

        #endregion

        #region Protected methods

        //protected override void Mapper(TOptions options)
        //{
        //    base.Mapper(options);
        //}

        //protected override void Mapper(IndicatorProperties properties)
        //{
        //    // Mapper the parent properties
        //    base.Mapper(properties);

        //    IsOverlay = properties.IsOverlay;
        //    DisplayInDataBox = properties.DisplayInDataBox;
        //    ScaleJustification = properties.ScaleJustification;
        //    IsSuspendedWhileInactive = properties.IsSuspendedWhileInactive;

        //}

        /// <summary>
        /// Automapper from <see cref="BaseNinjascript"/> to <see cref="NinjaScriptBase"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        //protected override void SetNinjascriptProperties(NinjaScriptBase ninjascript)
        //{
        //    // Set the parent properties
        //    base.SetNinjascriptProperties(ninjascript);

        //    ninjascript.IsOverlay = IsOverlay;
        //    ninjascript.DisplayInDataBox = DisplayInDataBox;
        //    ninjascript.ScaleJustification = ScaleJustification;
            

        //}

        #endregion

    }
}
