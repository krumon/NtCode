using System;

namespace Nt.Scripts.Indicators.Internal
{
    internal readonly struct IndicatorConfig
    {
        public IndicatorConfig(IIndicator indicator, string name, string providerTypeFullName, IndicatorState? state, Func<string, string, IndicatorState, bool> filter)
        {
            Indicator = indicator;
            Name = name;
            ProviderTypeFullName = providerTypeFullName;
            State = state;
            Filter = filter;
        }

        public IIndicator Indicator { get; }

        public string Name { get; }

        private string ProviderTypeFullName { get; }

        public IndicatorState? State { get; }

        public Func<string, string, IndicatorState, bool> Filter { get; }

        public bool IsEnabled(IndicatorState level)
        {
            var nLevel = (int)level;
            var nState = (int)State;
            if (State != null && (int)level < (int)State)
                return false;

            if (Filter != null)
                return Filter(ProviderTypeFullName, Name, level);

            return true;
        }
    }
}
