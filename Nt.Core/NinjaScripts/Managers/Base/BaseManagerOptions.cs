﻿namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// The base class to ninjascripts manager options.
    /// </summary>
    public abstract class BaseManagerOptions<TOptions> : BaseOptions<TOptions>, IManagerOptions
        where TOptions : BaseManagerOptions<TOptions>, IManagerOptions
    {
    }
}
