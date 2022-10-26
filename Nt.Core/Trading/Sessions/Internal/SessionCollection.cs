using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Trading.Internal
{
    /// <summary>
    /// Represents a deafault implementation of session descriptor collection.
    /// </summary>
    internal class SessionCollection :
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
            _descriptors.Add(item);
            //// Make sure item doesn't exist.
            //if (_descriptors.Contains(item))
            //    return;
            //// If item is the first element...
            //if(_descriptors.Count == 0)
            //    _descriptors.Add(item);
            //// Otherwise...
            //for (int i = 0; i < _descriptors.Count; i++)
            //{
            //    SessionCompareResult result = item.CompareSessionTo(_descriptors[i]);
            //    switch (result)
            //    {
            //        case SessionCompareResult.Equals:
            //            return;
            //        case SessionCompareResult.Minor:
            //        case SessionCompareResult.MinorAndInner:
            //            _descriptors.Insert(i,item);
            //            return;
            //        case SessionCompareResult.Child:
            //            _descriptors[i].Sessions.Add(item);
            //            return;
            //        case SessionCompareResult.Major:
            //        case SessionCompareResult.MajorAndInner:
            //            break;
            //        case SessionCompareResult.Parent:
            //            {
            //                ITradingSession tradingSession = _descriptors[i];
            //                //_sessions[i] = null;
            //                item.Sessions.Add(tradingSession);
            //                _descriptors[i] = item;
            //                return;
            //            }
            //        default:
            //            break;
            //    }
            //}
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

        /// <summary>
        /// Add a <see cref="TradingSession"/> element by the generic type.
        /// </summary>
        /// <param name="type">the type of the trading session.</param>
        //public void Add(SessionDescriptor descriptor)
        //{
        //    _descriptors.Add(descriptor);
        //}

        /// <summary>
        /// Add a <see cref="SessionCode"/> array.
        /// </summary>
        /// <param name="types">The <see cref="TradingSession"/> array to add.</param>
        //public void Add(params SessionType[] types) 
        //{
        //    if (types == null)
        //        throw new ArgumentNullException(nameof(types));

        //    foreach (var type in types) 
        //        Add(type);
        //}

        /// <summary>
        /// Add all <see cref="SessionType"/> enums.
        /// </summary>
        /// <typeparam name="T">The generic parameter <see cref="SessionType"/>.</typeparam>
        //public void Add<T>()
        //    where T : Enum
        //{
        //    if (typeof(T).Name == nameof(SessionType))
        //    {
        //        EnumHelpers.ForEach<SessionType>((type) =>
        //        {
        //            Add(type);
        //        });
        //    }
        //}

        #endregion
    }
}
