using Microsoft.Extensions.Options;
using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public class BaseNinjascriptOptions<T,N> : BaseOptions<T,N>
        where T : BaseNinjascriptOptions<T,N>, new()
        where N : NinjaScriptBase
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
        private int barsRequiredToPlot;

        #endregion

        #region Properties

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        public string Description { get { return description; } private set { description = value; } }

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        public string Name { get { return name; } private set { name = value; } }

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        public Calculate Calculate { get { return calculate; } private set { calculate = value; } }

        /// <summary>
        /// Represents the minimum bars required to plot.
        /// </summary>
        public int BarsRequiredToPlot { get { return barsRequiredToPlot; } private set { barsRequiredToPlot = value; } }

        #endregion

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public override void CopyTo(T options)
        {
            // Copy the new options
            options.Name = string.IsNullOrEmpty(Name) ? "kRuMoN Script" : Name;
            options.Description = string.IsNullOrEmpty(Description) ? @"Script created by kRuMoN." : Description;
            options.Calculate = Calculate;
            options.BarsRequiredToPlot = BarsRequiredToPlot;
        }

        /// <summary>
        /// Copy options to ninjatrader properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public override void CopyTo(N ninjascript)
        {
            // Copy the new options
            ninjascript.Name = Name;
            ninjascript.Calculate = Calculate;
            ninjascript.BarsRequiredToPlot = BarsRequiredToPlot;
        }

        #endregion

    }
}
