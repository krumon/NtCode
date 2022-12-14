using Nt.Core.Options;
using System;

namespace Nt.Core.Services
{
    public class SessionsOptions : BaseOptions<SessionsOptions>, IOptions<SessionsOptions>
    {
        public SessionsOptions(Action<SessionsOptions> action) : base(action)
        {
        }
    }
}
