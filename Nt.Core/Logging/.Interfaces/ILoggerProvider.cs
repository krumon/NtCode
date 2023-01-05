using System;

namespace Nt.Core.Logging
{
    //
    // Resumen:
    //     Represents a type that can create instances of Microsoft.Extensions.Logging.ILogger.
    public interface ILoggerProvider : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="ILogger"/> instance.
        /// </summary>
        /// <param name="categoryName">The category name for messages produced by the logger.</param>
        /// <returns>The instance of <see cref="ILogger"/> that was created.</returns>
        ILogger CreateLogger(string categoryName);
    }
}
