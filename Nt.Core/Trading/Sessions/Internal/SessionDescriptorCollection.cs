using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Trading.Internal
{
    /// <summary>
    /// Represents a deafault implementation of session descriptor collection.
    /// </summary>
    internal class SessionDescriptorCollection :
        IList<SessionDescriptor>,
        ICollection<SessionDescriptor>,
        IEnumerable<SessionDescriptor>,
        IEnumerable
    {

        #region Private members

        private readonly IList<SessionDescriptor> _descriptors = new List<SessionDescriptor>();

        #endregion

        #region Implementation methods

        /// <inheritdoc/>
        public SessionDescriptor this[int index] 
        { 
            get => _descriptors[index]; 
            set => _descriptors[index] = value; 
        }

        /// <inheritdoc/>
        public int Count => _descriptors.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public void Add(SessionDescriptor item)
        {
            if (_descriptors.Count < 1)
            {
                _descriptors.Add(item);
                return;
            }

            int count = _descriptors.Count;
            for (int i = 0; i < count; i++)
            {
                if (item.Duration > _descriptors[i].Duration)
                {
                    _descriptors.Insert(i, item);
                    break;
                }
                if (i == Count - 1)
                {
                    _descriptors.Add(_descriptors[i]);
                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _descriptors.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(SessionDescriptor item)
        {
            return _descriptors.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(SessionDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array,arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<SessionDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(SessionDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, SessionDescriptor item)
        {
            _descriptors.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(SessionDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Public methods


        #endregion
    }
}
