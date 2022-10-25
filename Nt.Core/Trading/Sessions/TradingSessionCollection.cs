using System;
using System.Collections;
using System.Collections.Generic;
using Kr.Core.Helpers;

namespace Nt.Core.Trading
{
    /// <summary>
    /// Represents the ninjascript trading sessions
    /// </summary>
    public class TradingSessionCollection :
        IList<TradingSession>,
        ICollection<TradingSession>,
        IEnumerable<TradingSession>,
        IEnumerable
    {

        #region Private members

        private readonly IList<TradingSession> _sessions = new List<TradingSession>();

        #endregion

        #region Implementation methods

        /// <inheritdoc/>
        public TradingSession this[int index] 
        { 
            get => _sessions[index]; 
            set => _sessions[index] = value; 
        }

        /// <inheritdoc/>
        public int Count => _sessions.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public void Add(TradingSession item)
        {
            // Make sure item doesn't exist.
            if (_sessions.Contains(item))
                return;
            // If item is the first element...
            if(_sessions.Count == 0)
                _sessions.Add(item);
            // Otherwise...
            for (int i = 0; i < _sessions.Count; i++)
            {
                TradingSessionCompareResult result = item.CompareSessionTo(_sessions[i]);
                switch (result)
                {
                    case TradingSessionCompareResult.Equals:
                        return;
                    case TradingSessionCompareResult.Minor:
                    case TradingSessionCompareResult.MinorAndInner:
                        _sessions.Insert(i,item);
                        return;
                    case TradingSessionCompareResult.Child:
                        _sessions[i].Sessions.Add(item);
                        return;
                    case TradingSessionCompareResult.Major:
                    case TradingSessionCompareResult.MajorAndInner:
                        break;
                    case TradingSessionCompareResult.Parent:
                        {
                            TradingSession tradingSession = _sessions[i];
                            //_sessions[i] = null;
                            item.Sessions.Add(tradingSession);
                            _sessions[i] = item;
                            return;
                        }
                    default:
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public void Clear()
        {
            if (_sessions != null && _sessions.Count > 0)
            {
                foreach(TradingSession item in _sessions)
                {
                    if (item.Sessions != null && item.Sessions.Count > 0)
                        item.Sessions.Clear();
                }
                _sessions.Clear();
            }
        }

        /// <inheritdoc/>
        public bool Contains(TradingSession item)
        {
            if (_sessions != null && _sessions.Count > 0)
            {
                foreach (TradingSession ts in _sessions)
                {
                    if (ts.Sessions != null && ts.Sessions.Count > 0)
                        ts.Sessions.Contains(item);
                }
                if (_sessions.Contains(item))
                    return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public void CopyTo(TradingSession[] array, int arrayIndex)
        {
            _sessions.CopyTo(array,arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<TradingSession> GetEnumerator()
        {
            return _sessions.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(TradingSession item)
        {
            return _sessions.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, TradingSession item)
        {
            _sessions.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(TradingSession item)
        {
            return _sessions.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            _sessions.RemoveAt(index);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add a <see cref="TradingSession"/> element by the generic type.
        /// </summary>
        /// <param name="type">the type of the trading session.</param>
        public void Add(TradingSessionType type)
        {
            TradingSession ts = TradingSession.CreateTradingSessionByType(type);
            Add(ts);
        }

        /// <summary>
        /// Add a <see cref="TradingSessionType"/> array.
        /// </summary>
        /// <param name="types">The <see cref="TradingSession"/> array to add.</param>
        public void Add(params TradingSessionType[] types) 
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            foreach (var type in types) 
                Add(type);
        }

        /// <summary>
        /// Add all <see cref="TradingSessionType"/> enums.
        /// </summary>
        /// <typeparam name="T">The generic parameter <see cref="TradingSessionType"/>.</typeparam>
        public void Add<T>()
            where T : Enum
        {
            if (typeof(T).Name == nameof(TradingSessionType))
            {
                EnumHelpers.ForEach<TradingSessionType>((type) =>
                {
                    Add(type);
                });
            }
        }

        #endregion
    }
}
