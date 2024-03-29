﻿using System;

namespace Nt.Core.Options
{
    /// <summary>
    ///  Used by <see cref="IOptionsMonitor{TOptions}"/> to cache TOptions instances.
    /// </summary>
    /// <typeparam name="TOptions">The type of options being requested.</typeparam>
    public interface IOptionsMonitorCache<TOptions> where TOptions : class
    {
        /// <summary>
        /// Gets a named options instance, or adds a new instance created with createOptions.
        /// </summary>
        /// <param name="name">The name of the options instance.</param>
        /// <param name="createOptions">The func used to create the new instance.</param>
        /// <returns>The options instance.</returns>
        TOptions GetOrAdd(string name, Func<TOptions> createOptions);

        /// <summary>
        /// Tries to adds a new option to the cache, will return false if the name already
        /// exists.
        /// </summary>
        /// <param name="name">The name of the options instance.</param>
        /// <param name="options">The options instance.</param>
        /// <returns>Whether anything was added.</returns>
        bool TryAdd(string name, TOptions options);

        /// <summary>
        /// Try to remove an options instance.
        /// </summary>
        /// <param name="name">The name of the options instance.</param>
        /// <returns> Whether anything was removed.</returns>
        bool TryRemove(string name);

        /// <summary>
        /// Clears all options instances from the cache.
        /// </summary>
        void Clear();
    }
}
