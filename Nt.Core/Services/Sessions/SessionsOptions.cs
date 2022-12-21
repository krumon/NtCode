using Nt.Core.Options;
using System;

namespace Nt.Core.Services
{
    public class SessionsOptions : ConfigureOptions<SessionsOptions>
    {
        public SessionsOptions(Action<SessionsOptions> action) : base(action)
        {
        }
    }
}
