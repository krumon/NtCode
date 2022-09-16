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


        #endregion

    }
}
