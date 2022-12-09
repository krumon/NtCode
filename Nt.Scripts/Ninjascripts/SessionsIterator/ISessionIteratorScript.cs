using NinjaTrader.Data;
using Nt.Core.Services;

namespace Nt.Scripts.Ninjascripts
{
    public interface ISessionIteratorScript : ISessionIteratorService
    {

        /// <summary>
        /// The session iterator to store the actual and the next session data.
        /// </summary>
        SessionIterator SessionIterator { get; }

    }
}
