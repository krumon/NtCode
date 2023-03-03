using Nt.Core.Logging;
using System;

namespace Nt.Scripts.Indicators.Internal
{
    internal static class IndicatorRuleSelector
    {
        public static void Select(IndicatorFilterOptions options, Type providerType, string name, out IndicatorState? state, out Func<string, string, IndicatorState, bool> filter)
        {
            filter = null;
            state = null; // This value is for testing.
            //state = options.MinLevel;

            // Filter rule selection:
            // 1. Select rules for current logger type, if there is none, select ones without logger type specified
            // 2. Select rules with longest matching categories
            // 3. If there nothing matched by category take all rules without category
            // 3. If there is only one rule use it's level and filter
            // 4. If there are multiple rules use last
            // 5. If there are no applicable rules use global minimal level

            string providerAlias = ProviderAliasAttribute.GetAlias(providerType);
            IndicatorFilterRule current = null;
            foreach (IndicatorFilterRule rule in options.RulesInternal)
            {
                if (IsBetter(rule, current, providerType.FullName, name)
                    || (!string.IsNullOrEmpty(providerAlias) && IsBetter(rule, current, providerAlias, name)))
                {
                    current = rule;
                }
            }

            if (current != null)
            {
                filter = current.Filter;
                //state = current.LogLevel;
            }
        }

        private static bool IsBetter(IndicatorFilterRule rule, IndicatorFilterRule current, string provider, string name)
        {
            // Skip rules with inapplicable type or category
            if (rule.ProviderName != null && rule.ProviderName != provider)
            {
                return false;
            }

            string indicatorName = rule.IndicatorName;
            if (indicatorName != null)
            {
                const char WildcardChar = '*';

                int wildcardIndex = indicatorName.IndexOf(WildcardChar);
                if (wildcardIndex != -1 && indicatorName.IndexOf(WildcardChar, wildcardIndex + 1) != -1)
                {
                    throw new InvalidOperationException("More Than One Wild card");
                }

                ReadOnlySpan<char> prefix, suffix;
                if (wildcardIndex == -1)
                {
                    prefix = indicatorName.AsSpan();
                    suffix = default;
                }
                else
                {
                    prefix = indicatorName.AsSpan(0, wildcardIndex);
                    suffix = indicatorName.AsSpan(wildcardIndex + 1);
                }

                if (!name.AsSpan().StartsWith(prefix, StringComparison.OrdinalIgnoreCase) ||
                    !name.AsSpan().EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            if (current?.ProviderName != null)
            {
                if (rule.ProviderName == null)
                {
                    return false;
                }
            }
            else
            {
                // We want to skip category check when going from no provider to having provider
                if (rule.ProviderName != null)
                {
                    return true;
                }
            }

            if (current?.IndicatorName != null)
            {
                if (rule.IndicatorName == null)
                {
                    return false;
                }

                if (current.IndicatorName.Length > rule.IndicatorName.Length)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
