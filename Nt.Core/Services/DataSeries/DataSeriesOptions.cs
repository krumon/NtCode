using Nt.Core.DependencyInjection;
using Nt.Core.Options;

namespace Nt.Core.Services
{
    public class DataSeriesOptions : IOptions<DataSeriesOptions>
    {
        public DataSeriesOptions Value => new DataSeriesOptions();

        public void Configure(DataSeriesOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}
