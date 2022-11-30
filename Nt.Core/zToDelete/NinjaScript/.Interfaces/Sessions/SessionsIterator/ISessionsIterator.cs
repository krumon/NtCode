﻿using Nt.Core.Events;
using System;

namespace Nt.Core.Ninjascript
{

    /// <summary>
    /// Interface for any sessions iterator.
    /// </summary>
    public interface ISessionsIterator : ISession
    {

        /// <summary>
        /// Event thats is raised when the sessoin changed.
        /// </summary>
        event Action<SessionChangedEventArgs> SessionChanged;

        /// <summary>
        /// Creates the <see cref="ISessionsIteratorBuilder"/> to construct the <see cref="ISessionsIterator"/> object.
        /// </summary>
        /// <returns>The <see cref="ISessionsIteratorBuilder"/> to construct the <see cref="ISessionsIterator"/> object.</returns>
        ISessionsIteratorBuilder CreateSessionsIteratorBuilder();

    }
}