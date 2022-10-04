﻿using System;

namespace Nt.Core
{
    [Serializable]
    public class SetOptionsException : Exception
    {
        #region Public properties

        /// <summary>
        /// the method thats call the Ninjascript.SetOptions method.
        /// </summary>
        public string CallerMember { get;}

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SetDefaultException"/> default instance.
        /// </summary>
        public SetOptionsException(){ }

        /// <summary>
        /// Creates a <see cref="SetDefaultException"/> with a default message instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        public SetOptionsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a <see cref="SetDefaultException"/> with a default message and inner exception instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        /// <param name="inner">The inner exception taht produce the error.</param>
        public SetOptionsException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Creates a <see cref="SetDefaultException"/> with a default message and inner exception instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        /// <param name="callerMember">The Ninjascript.SetOptions caller member.</param>
        public SetOptionsException(string message, string callerMember) : base(message)
        {
            CallerMember = callerMember;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Message to display when converts the exception to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{GetType()}. CallerMember: {CallerMember}. {Message}.";
        }

        #endregion

    }
}
