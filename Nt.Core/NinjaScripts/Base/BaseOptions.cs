using NinjaTrader.NinjaScript;
using System.Xml.Linq;

namespace Nt.Core
{

    /// <summary>
    /// The options for any ninjascript.
    /// </summary>
    public class BaseOptions : IOptions
    {

        #region Private members / Default values

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        private string description;

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        private string name;

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
        /// Represents the minimum bars required to plot.
        /// </summary>
        public int BarsRequiredToPlot { get { return barsRequiredToPlot; } set { barsRequiredToPlot = value; } }

        #endregion

        #region Public methods

        /// <summary>
        /// Copy the ninjascript properties to ninjatrader script.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader script.</param>
        public void CopyToNinjascript(NinjaScriptBase ninjascript)
        {
            // Copy the new options
            ninjascript.Name = Name;
            ninjascript.Calculate = Calculate;
            ninjascript.BarsRequiredToPlot = BarsRequiredToPlot;
        }

        #endregion

    }

    /// <summary>
    /// The base class for all ninjascripts options to configure.
    /// </summary>
    public abstract class BaseOptions<TOptions> : BaseOptions, IOptions<TOptions>
        where TOptions : BaseOptions<TOptions>, new()
    {

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public virtual void CopyTo(TOptions options)
        {
            // Copy the new options
            options.Name = string.IsNullOrEmpty(Name) ? "kRuMoN Script" : Name;
            options.Description = string.IsNullOrEmpty(Description) ? @"Script created by kRuMoN." : Description;
            options.Calculate = Calculate;
            options.BarsRequiredToPlot = BarsRequiredToPlot;
        }

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public virtual void CopyTo<TScript>(TScript script, TOptions options)
            where TScript : BaseNinjascript<TScript, TOptions>, new()
        {
            // Copy the new options
            script.Options.Name = string.IsNullOrEmpty(Name) ? "kRuMoN Script" : Name;
            script.Options.Description = string.IsNullOrEmpty(Description) ? @"Script created by kRuMoN." : Description;
            script.Options.Calculate = Calculate;
            script.Options.BarsRequiredToPlot = BarsRequiredToPlot;
        }

        #endregion

    }

}
