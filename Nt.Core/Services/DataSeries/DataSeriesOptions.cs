using Nt.Core.DependencyInjection;

namespace Nt.Core.Services
{
    public class DataSeriesOptions : IOptions<DataSeriesOptions>
    {
        public DataSeriesOptions Value => new DataSeriesOptions();
    }
}
