using Nt.Core.Logging;
using Nt.Scripts.Ninjascripts;
using Nt.Scripts.Services;

namespace Nt.Scripts.Indicators
{
    [NinjascriptProviderAlias("Sessions")]
    public class SessionsIndicatorProvider : INinjascriptProvider
    {
        public SessionsIndicatorProvider(INinjascriptBase ninjascript, IGlobalsData globalsData, ILogger<SessionsIndicator> logger)
        {

        }

        public INinjascript CreateNinjascript(string categoryName)
        {
            return new SessionsIndicator();
        }

        public void Dispose()
        {
        }
    }
}
