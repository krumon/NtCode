using System;
using System.Collections.Generic;

namespace Nt.Scripts.Indicators.Internal
{
    internal sealed class Indicator : IIndicator
    {
        public IndicatorInfo[] IndicatorsInfo { get; set; }
        public IndicatorConfig[] IndicatorsConfig { get; set; }

        public bool IsEnabled(IndicatorState state)
        {
            IndicatorConfig[] indicatorsConfig = IndicatorsConfig;
            if (indicatorsConfig == null)
            {
                return false;
            }

            List<Exception> exceptions = null;
            int i = 0;
            for (; i < indicatorsConfig.Length; i++)
            {
                ref readonly IndicatorConfig indicatorConfig = ref indicatorsConfig[i];
                if (!indicatorConfig.IsEnabled(state))
                {
                    continue;
                }

                if (IndicatorIsEnabled(state, indicatorConfig.Indicator, ref exceptions))
                {
                    break;
                }
            }

            if (exceptions != null && exceptions.Count > 0)
            {
                ThrowIndicatorError(exceptions);
            }

            return i < indicatorsConfig.Length;

        }
        private static void ThrowIndicatorError(List<Exception> exceptions)
        {
            throw new AggregateException(
                message: "An error occurred while writing to indicator(s).", innerExceptions: exceptions);
        }
        private static bool IndicatorIsEnabled(IndicatorState state, IIndicator indicator, ref List<Exception> exceptions)
        {
            try
            {
                if (indicator.IsEnabled(state))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (exceptions == null)
                {
                    exceptions = new List<Exception>();
                }

                exceptions.Add(ex);
            }

            return false;
        }

    }
}
