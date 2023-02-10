using Nt.Core.DependencyInjection;
using Nt.Core.Options;
using System;

namespace Nt.Scripts.Logging
{
    internal sealed class NinjascriptFormatterOptionsMonitor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] TOptions> : IOptionsMonitor<TOptions>
        where TOptions : NinjascriptFormatterOptions
    {
        private readonly TOptions _options;

        public NinjascriptFormatterOptionsMonitor(TOptions options)
        {
            _options = options;
        }

        public TOptions Get(string name) => _options;

        public IDisposable OnChange(Action<TOptions, string> listener)
        {
            return null;
        }

        public TOptions CurrentValue => _options;
    }
}