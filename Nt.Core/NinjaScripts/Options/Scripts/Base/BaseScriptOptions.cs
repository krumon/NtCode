using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public class BaseScriptOptions<TOptions> : BaseNinjascriptOptions<TOptions,NinjaScriptBase>
        where TOptions : BaseScriptOptions<TOptions>, new()
    {

        #region Private members / Default values

        #endregion

        #region Properties
        
        // Declare Properties that not use in indicators and strategies. This properties haven't been hieralchical.

        #endregion

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public override void CopyTo(TOptions options)
        {
            // Copy parent properties
            base.CopyTo(options);

        }

        /// <summary>
        /// Copy options to ninjatrader properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public sealed override void CopyTo(NinjaScriptBase ninjascript)
        {
            // Copy parent properties
            base.CopyTo((NinjaScriptBase)ninjascript);

        }

        #endregion

    }
}
