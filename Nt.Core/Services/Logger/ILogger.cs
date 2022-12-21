namespace Nt.Core.Services
{
    public interface ILogger
    {

        //
        // Resumen:
        //     Represents a type used to perform logging.
        //
        // Comentarios:
        //     Aggregates most logging patterns to a single method.
        public interface ILogger
        {
            //
            // Resumen:
            //     Writes a log entry.
            //
            // Parámetros:
            //   logLevel:
            //     Entry will be written on this level.
            //
            //   eventId:
            //     Id of the event.
            //
            //   state:
            //     The entry to be written. Can be also an object.
            //
            //   exception:
            //     The exception related to this entry.
            //
            //   formatter:
            //     Function to create a System.String message of the state and exception.
            //
            // Parámetros de tipo:
            //   TState:
            //     The type of the object to be written.
            void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter);

            //
            // Resumen:
            //     Checks if the given logLevel is enabled.
            //
            // Parámetros:
            //   logLevel:
            //     Level to be checked.
            //
            // Devuelve:
            //     true if enabled.
            bool IsEnabled(LogLevel logLevel);

            //
            // Resumen:
            //     Begins a logical operation scope.
            //
            // Parámetros:
            //   state:
            //     The identifier for the scope.
            //
            // Parámetros de tipo:
            //   TState:
            //     The type of the state to begin scope for.
            //
            // Devuelve:
            //     An System.IDisposable that ends the logical operation scope on dispose.
            IDisposable BeginScope<TState>(TState state);
        }

    }
}
