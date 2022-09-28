using NinjaTrader.NinjaScript;

namespace Nt.Core
{

    /// <summary>
    /// The interfece for any script element.
    /// </summary>
    public interface IOptions 
    {

        #region Properties

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        Calculate Calculate { get; set; }

        /// <summary>
        /// Represents the minimum bars required to plot.
        /// </summary>
        int BarsRequiredToPlot { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Copy properties from script to nijatrader ninjascript.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader script.</param>
        //void CopyToNinjascript(NinjaScriptBase ninjascript);

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        //void CopyTo(IOptions options);

        #endregion

    }
}
