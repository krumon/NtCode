using NinjaTrader.Data;
using Nt.Core.Services;

namespace Nt.Scripts.Ninjascripts
{
    public class SessionIteratorScript : SessionIteratorService
    {

        private SessionIterator sessionIterator;

        public SessionIteratorScript(IGlobalDataScript globalDataScript) : base(globalDataScript)
        {
        }

        public override void Configure(object[] ninjascriptObjects)
        {
            throw new System.NotImplementedException();
        }

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            throw new System.NotImplementedException();
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
