using Nt.Core.DependencyInjection;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents default implementation of <see cref="DataSeriesProvider"/>.
    /// </summary>
    public interface IDataSeriesBuilder : IServiceProviderBuilder<DataSeriesProvider, DataSeriesBuilder, DataSeriesCollection, DataSeriesDescriptor,DataSeriesOptions>
    {
    }
}
