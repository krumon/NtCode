using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using System;
using System.Xml.Linq;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    public abstract class NtScript : NtElement
    {
        #region Protected members

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        protected NinjaScriptBase ninjascript;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        protected Bars bars;

        #endregion

        #region Configure properties

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        public string Description {get; private set;}

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        public Calculate Calculate { get; private set; }

        /// <summary>
        /// Indicates if the data is overlay in the graphics.
        /// </summary>
        public bool IsOverlay { get; private set; }

        /// <summary>
        /// Indicates if the ninjascript display in the data box.
        /// </summary>
        public bool DisplayInDataBox { get; private set; }

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

        /// <summary>
        /// Represents the scale justification of the chart.
        /// </summary>
        public ScaleJustification ScaleJustification { get; private set; }

        //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
        //See Help Guide for additional information.
        /// <summary>
        /// Indicates if the ninjascript is suspended when the ninjascript is inactive.
        /// </summary>
        public bool IsSuspendedWhileInactive { get; private set; }

        #endregion

        #region Delegates

        /// <summary>
        /// Delegate to execute in OnBarUpdate method.
        /// </summary>
        public Action BarUpdateAction;

        #endregion

        #region State changed methods

        /// <summary>
        /// Load the Script.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="o">Any object necesary to load the script.</param>
        public virtual void Load(NinjaScriptBase ninjascript, Bars bars, object o = null) { }

        /// <summary>
        /// Free the memory of the script.
        /// </summary>
        public virtual void Terminated() { }

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

        #region Configure methods

        /// <summary>
        /// Method to configure the ninjascript default properties
        /// </summary>
        /// <param name="ninjascript">The ninjascript to configure.</param>
        /// <param name="options">The ninjascript configure options.</param>
        public virtual T ConfigureNtScripts<T>(Action<NtScriptConfigureOptions> options = null)
            where T : NtScript, new()
        {
            return new T();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Mapper <see cref="NtScript"/> from <see cref="NtScriptConfigureOptions"/>.
        /// </summary>
        /// <param name="options">The ninjascript configure options.</param>
        public void AutoMapper(NtScriptConfigureOptions options)
        {
            Description = options.Description;
            Name = options.Name;
            Calculate = options.Calculate;
            IsOverlay = options.IsOverlay;
            DisplayInDataBox = options.DisplayInDataBox;
            DrawOnPricePanel = options.DrawOnPricePanel;
            DrawHorizontalGridLines = options.DrawHorizontalGridLines;
            DrawVerticalGridLines = options.DrawVerticalGridLines;
            PaintPriceMarkers = options.PaintPriceMarkers;
            ScaleJustification = options.ScaleJustification;
            IsSuspendedWhileInactive = options.IsSuspendedWhileInactive;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Execute the delegate in the OnBarUpdate method
        /// </summary>
        /// <param name="action">The delegate to execute.</param>
        protected void ExecuteInBarUpdateMethod(Action action)
            {
                action?.Invoke();
            }

            /// <summary>
            /// Execute the delegate in the OnBarUpdate method
            /// </summary>
            /// <param name="action">The delegate to execute.</param>
            protected void ExecuteInMarketDataMethod(Action action)
            {
                action?.Invoke();
            }

            #endregion

        #region Helper methods

        /// <summary>
        /// Mapper <see cref="NtScript"/> from <see cref="NtScriptConfigureOptions"/>.
        /// </summary>
        /// <param name="script">The ninjascript.</param>
        /// <param name="options">The ninjascript configure options.</param>
        public static void AutoMapper(NtScript script, NtScriptConfigureOptions options)
        {
            script.Description = options.Description;
            script.Name = options.Name;
            script.Calculate = options.Calculate;
            script.IsOverlay = options.IsOverlay;
            script.DisplayInDataBox = options.DisplayInDataBox;
            script.DrawOnPricePanel = options.DrawOnPricePanel;
            script.DrawHorizontalGridLines = options.DrawHorizontalGridLines;
            script.DrawVerticalGridLines = options.DrawVerticalGridLines;
            script.PaintPriceMarkers = options.PaintPriceMarkers;
            script.ScaleJustification = options.ScaleJustification;
            script.IsSuspendedWhileInactive = options.IsSuspendedWhileInactive;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Automapper from <see cref="NtScript"/> to <see cref="NinjaScriptBase"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="script"></param>
        public void AutoMapper(NinjaScriptBase ninjascript, NtScript script)
        {
            ninjascript.Name = script.Name;
            ninjascript.Calculate = script.Calculate;
            ninjascript.IsOverlay = script.IsOverlay;
            ninjascript.DisplayInDataBox = script.DisplayInDataBox;
            ninjascript.ScaleJustification = script.ScaleJustification;
        }

        #endregion
    }
}
