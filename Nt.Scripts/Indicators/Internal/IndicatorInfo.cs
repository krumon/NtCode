using System;

namespace Nt.Scripts.Indicators.Internal
{
    internal readonly struct IndicatorInfo
    {
        public IndicatorInfo(IIndicatorProvider provider, string name) : this()
        {
            ProviderType = provider.GetType();
            Indicator = provider.CreateLogger(name);
            Name = name;
        }

        public IIndicator Indicator { get; }

        public string Name { get; }

        public Type ProviderType { get; }

    }
}
