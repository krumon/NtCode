using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The interface for any ninjascript builder.
    /// </summary>
    public interface IBuilder
    {
        #region Protected members

        /// <summary>
        /// The script options.
        /// </summary>
        IOptions options {get;} //  protected IOptions options;

        #endregion

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <returns>The script object created by the builder.</returns>
        INinjascript Build(NinjaScriptBase ninjascript);

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <returns>The script object created by the builder.</returns>
        INinjascript Build();

        /// <summary>
        /// Configure options into the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        IBuilder Configure<Script, Options>(Action<Options> options);

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        IBuilder Configure<Script,Options>(Options op);

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        IBuilder Configure(Action<IOptions> op);

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op"><see cref="IOptions"/> object with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        IBuilder Configure(IOptions op);

        #endregion

    }

    ///// <summary>
    ///// The interfece for any ninjascript builder.
    ///// </summary>
    ///// <typeparam name="TScript"></typeparam>
    ///// <typeparam name="TOptions"></typeparam>
    //public interface IBuilder<TScript, TOptions, TBuilder>
    //    where TScript : INinjascript<TScript, TOptions, TBuilder>
    //    where TOptions : IOptions<TOptions>
    //    where TBuilder : IBuilder<TScript,TOptions, TBuilder>
    //{
    //}

}
