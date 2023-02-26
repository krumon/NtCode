﻿namespace Nt.Core.Hosting
{

    /// <summary>
    /// Represents ninjatrader disposable services.
    /// </summary>
    public interface IDisposable
    {
        /// <summary>
        /// Method to dispose the service.        
        /// </summary>
        void Dispose();
    }
}
