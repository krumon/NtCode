namespace Nt.Core.Data
{
    /// <summary>
    /// Represents a default implementation of data series descriptor collection.
    /// </summary>
    public class InstrumentServiceCollection : BaseServiceCollection<InstrumentServiceDescriptor>
    {

        #region Implementation methods

        /// <inheritdoc/>
        public override void Add(InstrumentServiceDescriptor item)
        {
            _descriptors.Add(item);
        }

        /// <inheritdoc/>
        public override bool Remove(InstrumentServiceDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        #endregion

    }
}
