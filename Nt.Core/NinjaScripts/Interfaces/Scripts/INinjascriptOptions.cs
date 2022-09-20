using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Interface for any ninjascript options.
    /// </summary>
    public interface INinjascriptOptions<TOptions> : IOptions<TOptions>, INinjascriptOptions
        where TOptions : INinjascriptOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any ninjascript options.
    /// </summary>
    public interface INinjascriptOptions
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
        void CopyToNinjascript(NinjaScriptBase ninjascript);

        #endregion

    }
}