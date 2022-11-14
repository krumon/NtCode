﻿using System;

namespace Nt.Core.Data
{

    /// <summary>
    /// Defines any optional configure service.
    /// </summary>
    public interface IHostedService<T>
        where T : Enum
    {

        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        T Key { get; }

    }
}
