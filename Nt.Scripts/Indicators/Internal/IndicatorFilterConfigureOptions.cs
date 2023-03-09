using Nt.Core.Attributes;
using Nt.Core.Configuration;
using Nt.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nt.Scripts.Indicators.Internal
{
    internal sealed class IndicatorFilterConfigureOptions : IConfigureOptions<IndicatorFilterOptions>
    {
        private const string LogLevelKey = "NinjascriptLevel";
        private const string DefaultCategory = "Default";
        private readonly IConfiguration _configuration;

        public IndicatorFilterConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(IndicatorFilterOptions options)
        {
            LoadDefaultConfigValues(options);
        }

        private void LoadDefaultConfigValues(IndicatorFilterOptions options)
        {
            if (_configuration == null)
                return;

            options.CaptureScopes = GetCaptureScopesValue(options);
            foreach (IConfigurationSection configurationSection in _configuration.GetChildren())
            {
                if (configurationSection.Key.Equals(LogLevelKey, StringComparison.OrdinalIgnoreCase))
                {
                    // Load global category defaults
                    LoadRules(options, configurationSection, null);
                }
                else
                {
                    IConfigurationSection logLevelSection = configurationSection.GetSection(LogLevelKey);
                    if (logLevelSection != null)
                    {
                        // Load logger specific rules
                        string logger = configurationSection.Key;
                        LoadRules(options, logLevelSection, logger);
                    }
                }
            }
        }

        [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode",
            Justification = "IConfiguration.GetValue is safe when T is a bool.")]
        private bool GetCaptureScopesValue(IndicatorFilterOptions options) => _configuration.GetValue(nameof(options.CaptureScopes), options.CaptureScopes);

        private void LoadRules(IndicatorFilterOptions options, IConfigurationSection configurationSection, string logger)
        {
            foreach (KeyValuePair<string, string> section in configurationSection.AsEnumerable(true))
            {
                if (TryGetSwitch(section.Value, out IndicatorState level))
                {
                    string category = section.Key;
                    if (category.Equals(DefaultCategory, StringComparison.OrdinalIgnoreCase))
                    {
                        category = null;
                    }
                    var newRule = new IndicatorFilterRule(logger, category, null);
                    options.Rules.Add(newRule);
                }
            }
        }

        private static bool TryGetSwitch(string value, out IndicatorState level)
        {
            if (string.IsNullOrEmpty(value))
            {
                level = IndicatorState.Disable;
                return false;
            }
            else if (Enum.TryParse(value, true, out level))
            {
                return true;
            }
            else
            {
                throw new InvalidOperationException($"Value Not Supported, {value}.");
            }
        }
    }
}
