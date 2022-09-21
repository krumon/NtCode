﻿namespace Nt.Core
{

    /// <summary>
    /// Interface for any indicator options.
    /// </summary>
    public interface IIndicatorOptions : IOptions
    {
    }

    /// <summary>
    /// Interface for any indicator options.
    /// </summary>
    public interface IIndicatorOptions<TOptions> : IOptions<TOptions>, IIndicatorOptions
        where TOptions : IIndicatorOptions<TOptions>
    {
    }

}
