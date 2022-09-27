using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{

    /// <summary>
    /// Interface for any ninjascript
    /// </summary>
    public interface INinjascript : IElement
    {

        #region Protected members

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        NinjaScriptBase Ninjascript { get; }

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        Bars Bars { get; }

        #endregion

        #region State changed methods

        /// <summary>
        /// Method to set default properties in the script in "OnStateChanged.Configure" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript parent object.</param>
        void SetDefault(NinjaScriptBase ninjascript);

        /// <summary>
        /// Loaded the Script in OnStateChanged method.
        /// </summary>
        /// <param name="ninjascript">The parent ninjascript object.</param>
        /// <param name="bars">The chart bars object loaded.</param>
        void Load(NinjaScriptBase ninjascript, Bars bars);

        /// <summary>
        /// Free the memory of the script.
        /// </summary>
        void Terminated();

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        void OnBarUpdate();

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        void OnMarketData();

        #endregion

        #region Configure methods

        /// <summary>
        /// Configure options into the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        INinjascript Configure<Script, Options>(Action<Options> options);

        /// <summary>
        /// Add properties to configure the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        INinjascript Configure(IOptions options);

        /// <summary>
        /// Create a ninjascript default builder.
        /// </summary>
        /// <returns>Default instance of <see cref="TBuilder"/>.</returns>
        IBuilder CreateBuilder();

        #endregion

    }

    ///// <summary>
    ///// Interface for any ninjascript.
    ///// </summary>
    //public interface INinjascript<TScript,TOptions, TBuilder> : IElement
    //    where TScript : INinjascript<TScript,TOptions, TBuilder>
    //    where TOptions : IOptions<TOptions>
    //    where TBuilder : IBuilder<TScript,TOptions, TBuilder>
    //{

    //    #region Protected members

    //    /// <summary>
    //    /// The ninjascript parent of the class.
    //    /// </summary>
    //    NinjaScriptBase Ninjascript { get; }

    //    /// <summary>
    //    /// The bars of the chart control.
    //    /// </summary>
    //    Bars Bars { get; }

    //    #endregion

    //    #region State changed methods

    //    /// <summary>
    //    /// Method to set default properties in the script in "OnStateChanged.Configure" method.
    //    /// </summary>
    //    /// <param name="ninjascript">The ninjascript parent object.</param>
    //    void SetDefault(NinjaScriptBase ninjascript);

    //    /// <summary>
    //    /// Loaded the Script in OnStateChanged method.
    //    /// </summary>
    //    /// <param name="ninjascript">The parent ninjascript object.</param>
    //    /// <param name="bars">The chart bars object loaded.</param>
    //    void Load(NinjaScriptBase ninjascript, Bars bars);

    //    /// <summary>
    //    /// Free the memory of the script.
    //    /// </summary>
    //    void Terminated();

    //    #endregion

    //    #region Market Data methods

    //    /// <summary>
    //    /// Event driven method which is called whenever a bar is updated. 
    //    /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
    //    /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
    //    /// </summary>
    //    void OnBarUpdate();

    //    /// <summary>
    //    /// Event driven method which is called and guaranteed to be in the correct sequence 
    //    /// for every change in level one market data for the underlying instrument. 
    //    /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
    //    /// </summary>
    //    void OnMarketData();

    //    #endregion

    //    #region Configure methods

    //    /// <summary>
    //    /// Configure options into the script.
    //    /// </summary>
    //    /// <param name="options"></param>
    //    /// <returns></returns>
    //    TScript Configure(Action<TOptions> options);

    //    /// <summary>
    //    /// Add properties to configure the script.
    //    /// </summary>
    //    /// <param name="options"></param>
    //    /// <returns></returns>
    //    TScript Configure(TOptions options);

    //    /// <summary>
    //    /// Create a ninjascript default builder.
    //    /// </summary>
    //    /// <returns>Default instance of <see cref="TBuilder"/>.</returns>
    //    TBuilder CreateBuilder();

    //    #endregion

    //}

}