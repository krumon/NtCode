﻿using Nt.Core.Configuration;

namespace Nt.Core.Logging.Configuration
{
    /// <summary>
    /// Allows access to configuration section associated with logger provider
    /// </summary>
    /// <typeparam name="T">Type of logger provider to get configuration for</typeparam>
    public interface ILoggerProviderConfiguration<T>
    {
        /// <summary>
        /// Configuration section for requested logger provider
        /// </summary>
        IConfiguration Configuration { get; }
    }
}
