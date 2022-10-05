using NinjaTrader.NinjaScript;
using System.Xml.Linq;

namespace Nt.Core.Ninjascript
{

    /// <summary>
    /// The base class for all ninjascripts options to configure.
    /// </summary>
    public abstract class BaseOptions<TOptions> : IOptions
        where TOptions : BaseOptions<TOptions>
    {

        #region Private members / Default values

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
        /// Represents the minimum bars required to plot.
        /// </summary>
        private int barsRequiredToPlot = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        public string Description 
        { 
            get => description; 
            set 
            {
                // make sure value changes
                if (description == value)
                    return;

                if (string.IsNullOrEmpty(value))
                    description = @"Script created by kRuMoN.";
                else
                    description = value; 
            } 
        }

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        public string Name 
        { 
            get => name; 
            set 
            {
                // make sure value changes
                if (name == value)
                    return;

                if (string.IsNullOrEmpty(value))
                    name = "kRuMoN Script";
                else
                    name = value; 
            } 
        }

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        public Calculate Calculate { get { return calculate; } set { calculate = value; } }

        /// <summary>
        /// Represents the minimum bars required to plot.
        /// </summary>
        public int BarsRequiredToPlot { get { return barsRequiredToPlot; } set { barsRequiredToPlot = value; } }

        #endregion

    }
}
