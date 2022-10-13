using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Default implementation of <see cref="INinjascriptsServiceCollection"/>.
    /// </summary>
    public class NinjascriptsServiceCollection : INinjascriptsServiceCollection
    {

        public NinjascriptsServiceCollection()
        {
            //ServiceCollection sc = null;
            //ServiceDescriptor sd = null;
            //ServiceProvider sp = null;
            //HostBuilder builder;
            //IHost host = Host.CreateDefaultBuilder().Build();
            //ApplicationLifetime lifetime;
        }

        #region Private membres

        /// <summary>
        /// Represents the descriptors collection.
        /// </summary>
        private readonly IList<NinjascriptsServiceDescriptor> _descriptors = new List<NinjascriptsServiceDescriptor>();

        #endregion

        #region Implamentation

        /// <inheritdoc />
        public NinjascriptsServiceDescriptor this[int index]
        {
            get => _descriptors[index];
            set => _descriptors[index] = value;
        }

        /// <inheritdoc />
        public int Count => _descriptors.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public void Add(NinjascriptsServiceDescriptor item)
        {
            _descriptors.Add(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            _descriptors.Clear();
        }

        /// <inheritdoc />
        public bool Contains(NinjascriptsServiceDescriptor item)
        {
            return _descriptors.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(NinjascriptsServiceDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public IEnumerator<NinjascriptsServiceDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        /// <inheritdoc />
        public int IndexOf(NinjascriptsServiceDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }

        /// <inheritdoc />
        public void Insert(int index, NinjascriptsServiceDescriptor item)
        {
            _descriptors.Insert(index, item);
        }

        /// <inheritdoc />
        public bool Remove(NinjascriptsServiceDescriptor item)
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
