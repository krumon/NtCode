﻿using Nt.Core.Events;

namespace Nt.Core.Ninjascript
{

    /// <summary>
    /// Interface for any session script.
    /// </summary>
    public interface ISession : INinjascript
    {
        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        void OnSessionChanged(SessionChangedEventArgs e);

    }

}