using NinjaTrader.NinjaScript;
using System.Xml.Linq;

namespace Nt.Core
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

        #region Public methods

        ///// <summary>
        ///// Copy the ninjascript properties to ninjatrader script.
        ///// </summary>
        ///// <param name="ninjascript">The ninjatrader script.</param>
        //public void CopyToNinjascript(NinjaScriptBase ninjascript)
        //{
        //    // Copy the new options
        //    ninjascript.Name = Name;
        //    ninjascript.Calculate = Calculate;
        //    ninjascript.BarsRequiredToPlot = BarsRequiredToPlot;
        //}

        ///// <summary>
        ///// Copy options to ninjascript object.
        ///// </summary>
        ///// <param name="options"></param>
        //public virtual void CopyTo(IOptions options)
        //{
        //    // Copy the new options
        //    options.Name = string.IsNullOrEmpty(Name) ? "kRuMoN Script" : Name;
        //    options.Description = string.IsNullOrEmpty(Description) ? @"Script created by kRuMoN." : Description;
        //    options.Calculate = Calculate;
        //    options.BarsRequiredToPlot = BarsRequiredToPlot;
        //}

        #endregion

    }
}
