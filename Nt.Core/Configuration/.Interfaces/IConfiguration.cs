﻿using Nt.Core.Primitives;
using System.Collections.Generic;

namespace Nt.Core.Configuration
{
    /// <summary>
    /// Represents a set of key/value application configuration properties.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets or sets a configuration value.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <returns>The configuration value.</returns>
        string this[string key] { get; set; }

        /// <summary>
        /// Gets a configuration sub-section with the specified key.
        /// </summary>
        /// <param name="key">The key of the configuration section.</param>
        /// <returns>The <see cref="IConfigurationSection"/>.</returns>
        /// <remarks>
        /// This method will never return null. If no matching sub-section is found with
        /// the specified key, an empty <see cref="IConfigurationSection"/> will be returned.
        /// </remarks>
        IConfigurationSection GetSection(string key);

        /// <summary>
        /// Gets the immediate descendant configuration sub-sections.
        /// </summary>
        /// <returns>The configuration sub-sections.</returns>
        IEnumerable<IConfigurationSection> GetChildren();

        /// <summary>
        /// Returns a <see cref="IChangeToken"/> that can be used to observe
        /// when this configuration is reloaded.
        /// </summary>
        /// <returns>A <see cref="IChangeToken"/>.</returns>
        IChangeToken GetReloadToken();

    }
}
