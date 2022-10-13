using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Hosting.Internal
{
    internal class NinjascriptHost : INinjascriptHost
    {

        #region Private members



        #endregion

        #region Public properties

        /// <inheritdoc/>
        public IServiceProvider Services => throw new NotImplementedException();

        #endregion

        #region Public mnethods



        #endregion

        #region Implementation methods

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
