﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Core.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="IConfigurationRoot"/>.
    /// </summary>
    public static class ConfigurationRootExtensions
    {
        /// <summary>
        /// Generates a human-readable view of the configuration showing where each value came from.
        /// </summary>
        /// <returns> The debug view. </returns>
        public static string GetDebugView(this IConfigurationRoot root)
        {
            void RecurseChildren(
                StringBuilder stringBuilder,
                IEnumerable<IConfigurationSection> children,
                string indent)
            {
                foreach (IConfigurationSection child in children)
                {
                    (string Value, IConfigurationProvider Provider) valueAndProvider = GetValueAndProvider(root, child.Path);

                    if (valueAndProvider.Provider != null)
                    {
                        stringBuilder
                            .Append(indent)
                            .Append(child.Key)
                            .Append('=')
                            .Append(valueAndProvider.Value)
                            .Append(" (")
                            .Append(valueAndProvider.Provider)
                            .AppendLine(")");
                    }
                    else
                    {
                        stringBuilder
                            .Append(indent)
                            .Append(child.Key)
                            .AppendLine(":");
                    }

                    RecurseChildren(stringBuilder, child.GetChildren(), indent + "  ");
                }
            }

            var builder = new StringBuilder();

            RecurseChildren(builder, root.GetChildren(), "");

            return builder.ToString();
        }

        private static (string Value, IConfigurationProvider Provider) GetValueAndProvider(
            IConfigurationRoot root,
            string key)
        {
            foreach (IConfigurationProvider provider in root.Providers.Reverse())
            {
                if (provider.TryGet(key, out string value))
                {
                    return (value, provider);
                }
            }

            return (null, null);
        }
    }
}
