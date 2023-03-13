using Nt.Core.Logging;
using Nt.Scripts.Ninjascripts;
using Nt.Scripts.Services;

namespace Nt.Scripts.Indicators
{
    [NinjascriptProviderAlias("Sessions")]
    public class SessionsProvider : INinjascriptProvider
    {
        public SessionsProvider(INinjascriptBase ninjascript, IGlobalsData globalsData, ILogger<Sessions> logger)
        {

        }

        public INinjascript CreateNinjascript(string categoryName)
        {
            return new Sessions();
        }

        public void Dispose()
        {
        }
    }
}
