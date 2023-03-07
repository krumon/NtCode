using System;

namespace Nt.Core.Ninjascripts.Indicators
{
    internal class Indicators : IIndicators
    {
        public Action<object> Print => throw new NotImplementedException();

        public Action ClearOutputWindow => throw new NotImplementedException();

        public bool IsConfigured => throw new NotImplementedException();

        public void Configure()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OnBarUpdate()
        {
            throw new NotImplementedException();
        }

        public void OnMarketData()
        {
            throw new NotImplementedException();
        }
    }
}
