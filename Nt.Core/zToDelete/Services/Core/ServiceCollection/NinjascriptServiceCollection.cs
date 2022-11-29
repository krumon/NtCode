using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Services
{
    /// <summary>
    /// Default implementation of <see cref="INinjascriptServiceCollection"/>.
    /// </summary>
    public class NinjascriptServiceCollection : INinjascriptServiceCollection
    {

        #region Private membres

        /// <summary>
        /// Represents the descriptors collection.
        /// </summary>
        private readonly IList<NinjascriptServiceDescriptor> _descriptors = new List<NinjascriptServiceDescriptor>();

        #endregion

        #region Implamentation

        /// <inheritdoc />
        public NinjascriptServiceDescriptor this[int index]
        {
            get => _descriptors[index];
            set => _descriptors[index] = value;
        }

        /// <inheritdoc />
        public int Count => _descriptors.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public void Add(NinjascriptServiceDescriptor item)
        {
            _descriptors.Add(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            _descriptors.Clear();
        }

        /// <inheritdoc />
        public bool Contains(NinjascriptServiceDescriptor item)
        {
            return _descriptors.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(NinjascriptServiceDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public IEnumerator<NinjascriptServiceDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        /// <inheritdoc />
        public int IndexOf(NinjascriptServiceDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }

        /// <inheritdoc />
        public void Insert(int index, NinjascriptServiceDescriptor item)
        {
            _descriptors.Insert(index, item);
        }

        /// <inheritdoc />
        public bool Remove(NinjascriptServiceDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
