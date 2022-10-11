﻿namespace Nt.Core.Services
{
    /// <summary>
    /// Details about the current ninjascript environment
    /// </summary>
    public interface INinjascriptEnvironment
    {

        /// <summary>
        /// The configuration of the environment, typically Development or Production
        /// </summary>
        string Configuration { get; }

        /// <summary>
        /// True if we are in a development (specifically, debuggable) environment
        /// </summary>
        bool IsDevelopment { get; }

        /// <summary>
        /// Indicates if we are a mobile platform
        /// </summary>
        bool IsMobile { get; }

    }
}
