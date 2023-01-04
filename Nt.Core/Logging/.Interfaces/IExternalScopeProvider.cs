using System;

namespace Nt.Core.Logging
{
    //
    // Resumen:
    //     Represents a storage of common scope data.
    public interface IExternalScopeProvider
    {
        //
        // Resumen:
        //     Executes callback for each currently active scope objects in order of creation.
        //     All callbacks are guaranteed to be called inline from this method.
        //
        // Parámetros:
        //   callback:
        //     The callback to be executed for every scope object
        //
        //   state:
        //     The state object to be passed into the callback
        //
        // Parámetros de tipo:
        //   TState:
        //     The type of state to accept.
        void ForEachScope<TState>(Action<object, TState> callback, TState state);

        //
        // Resumen:
        //     Adds scope object to the list
        //
        // Parámetros:
        //   state:
        //     The scope object
        //
        // Devuelve:
        //     The System.IDisposable token that removes scope on dispose.
        IDisposable Push(object state);
    }
}
