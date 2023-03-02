using Nt.Core.Options;
using System;

namespace Nt.Scripts.Indicators.Internal
{
    internal sealed class StaticIndicatorFilterOptionsMonitor : IOptionsMonitor<IndicatorFilterOptions>
    {
        public StaticIndicatorFilterOptionsMonitor(IndicatorFilterOptions currentValue)
        {
            CurrentValue = currentValue;
        }

        public IDisposable OnChange(Action<IndicatorFilterOptions, string> listener) => null;

        public IndicatorFilterOptions Get(string name) => CurrentValue;

        public IndicatorFilterOptions CurrentValue { get; }
    }
}
