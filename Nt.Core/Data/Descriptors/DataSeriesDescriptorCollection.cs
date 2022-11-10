namespace Nt.Core.Data
{
    /// <summary>
    /// Represents a default implementation of data series descriptor collection.
    /// </summary>
    public class DataSeriesDescriptorCollection : BaseDescriptorCollection<DataSeriesDescriptor>
    {

        #region Implementation methods

        /// <inheritdoc/>
        public override void Add(DataSeriesDescriptor item)
        {
            _descriptors.Add(item);
        }

        /// <inheritdoc/>
        public override bool Remove(DataSeriesDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        #endregion

    }
}
