using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{

    /// <summary>
    /// Interface for any ninjascript builder.
    /// </summary>
    public interface INinjascriptBuilder<TScript,TOptions> : INinjascriptBuilder
        where TScript : INinjascriptBuilder<TScript,TOptions>
        where TOptions : INinjascriptOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any ninjascript builder.
    /// </summary>
    public interface INinjascriptBuilder : IBuilder
    {
    }

}