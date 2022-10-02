using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The interface for any ninjascript builder.
    /// </summary>
    public interface IBuilder
    {

        #region Public properties

        ///// <summary>
        ///// The script options.
        ///// </summary>
        //IOptions Options {get;}

        INinjascript Script { get; }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <returns>The script object created by the builder.</returns>
        INinjascript Build(NinjaScriptBase ninjascript = null);

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <returns>The script object created by the builder.</returns>
        //INinjascript Build();

        /// <summary>
        /// Configure options into the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        IBuilder Configure<Script, Options>(Action<Options> options);

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        IBuilder Configure<Script, Options>(Options options)
            where Options : IOptions;

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        //IBuilder Configure(Action<IOptions> options);

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options"><see cref="IOptions"/> object with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        //IBuilder Configure(IOptions options);

        #endregion

    }
}
