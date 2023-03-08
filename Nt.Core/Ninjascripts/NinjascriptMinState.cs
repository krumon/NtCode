namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// The ninjascript object enable states.
    /// </summary>
    public enum NinjascriptMinState
    {
        /// <summary>
        /// The ninjascript is always active.
        /// </summary>
        /// <remarks>
        /// The ninjascript enter in 'SetDefault', 'Configure', 'Active', 'DataLoaded', 'Historical', 'Transition', 'Realtime' and 'Terminated' states.
        /// </remarks>
        Configure,

        /// <summary>
        /// The ninjascript is active only in the historical state
        /// </summary>
        /// <remarks>
        /// This value is used for ninjascripts that gets historical data for calculate values and inyect to other ninjascript.
        /// The ninjascript enter in 'SetDefault', 'Configure', 'Active', 'DataLoaded', 'Historical', 'Transition' and 'Terminated' states.
        /// </remarks>
        Historical,

        /// <summary>
        /// The ninjascript is enable only in the real state. This ninjascript doesn´t nedd historical values. 
        /// </summary>
        /// <remarks>
        /// The ninjascript enter in 'SetDefault', 'Configure', 'Active', 'DataLoaded', 'Realtime' and 'Terminated' states.
        /// </remarks>
        Real,

        /// <summary>
        /// The ninjascript is disable.
        /// </summary>
        None
    }
}
