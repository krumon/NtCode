using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Trading.Internal
{
    internal class TradingSessionCollection : 
        IList<ITradingSession>,
        ICollection<ITradingSession>,
        IEnumerable<ITradingSession>,
        IEnumerable

    {

        #region Private members

        private IList<TradingSession> _sessions = new List<TradingSession>();

        #endregion

        #region Implementation methods

        public ITradingSession this[int index] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int Count => throw new System.NotImplementedException();

        public bool IsReadOnly => throw new System.NotImplementedException();

        public void Add(ITradingSession item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(ITradingSession item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(ITradingSession[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<ITradingSession> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public int IndexOf(ITradingSession item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, ITradingSession item)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(ITradingSession item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}
