﻿namespace Nt.Core.Trading
{
    /// <summary>
    /// The type of result when compare trading sessions.
    /// </summary>
    public enum TradingSessionCompareResult
    {
        /// <summary>
        /// The trading sessions are equals.
        /// </summary>
        Equals = 0,
        /// <summary>
        /// The second trading session is inner on the first trading session.
        /// </summary>
        Parent = 3,
        /// <summary>
        /// The first trading session is inner on the second tranding session.
        /// </summary>
        Child = -3,
        /// <summary>
        /// The times of the first trading session are minor or equals than the times of the second trading session.
        /// </summary>
        Minor = -2,
        /// <summary>
        /// The times of the first trading session are greater or equals than the times of the second trading session.
        /// </summary>
        Major = 2,
        /// <summary>
        /// The begin time of the first trading session are minor than te begin time of the second trading session and
        /// the end time of the first trading session is inner on the second trading session.
        /// </summary>
        MinorAndInner = -1,
        /// <summary>
        /// The begin time of the first trading session is inner on the second trading session and
        /// the end time of the first trading session are greater than the end time of the second trading session.
        /// </summary>
        MajorAndInner = 1

    }
}
