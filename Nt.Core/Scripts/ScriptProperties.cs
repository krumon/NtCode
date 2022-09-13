using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Ninjascript configure options.
    /// </summary>
    public class ScriptProperties
    {

        #region Private members / Default value

		/// <summary>
		/// Represents the ninjascript description.
		/// </summary>
		private string description = @"Script created by kRuMoN.";

		/// <summary>
		/// Represents the ninjascript name.
		/// </summary>
		private string name = "kRuMoN Script";

		/// <summary>
		/// Represents the ninjascript calculate mode.
		/// </summary>
		private Calculate calculate = Calculate.OnBarClose;

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

        #endregion

        #region Properties

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        public string Description { get { return description; } set { description = value; } }

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        public Calculate Calculate { get { return calculate; } set { calculate = value; } }

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

        #endregion

    }
}
