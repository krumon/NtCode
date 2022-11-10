namespace Nt.Core.Data
{
    /// <summary>
    /// Represents a default implementation of data series descriptor collection.
    /// </summary>
    public class InstrumentDescriptorCollection : BaseDescriptorCollection<InstrumentDescriptor>
    {

        #region Implementation methods

        /// <inheritdoc/>
        public override void Add(InstrumentDescriptor item)
        {
            _descriptors.Add(item);
        }

        /// <inheritdoc/>
        public override bool Remove(InstrumentDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        #endregion

    }
}
