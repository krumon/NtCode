﻿namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options.</typeparam>
    public abstract class BaseManager<TScript, TOptions> : BaseScript<TScript, TOptions>
        where TScript : BaseManager<TScript, TOptions>, new()
        where TOptions : BaseManagerOptions<TOptions>, new()
    {
    }
}
