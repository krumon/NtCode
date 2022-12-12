using Nt.Core.Hosting;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the properties and methods to create a default implementation of <see cref="ChartStyleService"/>.
    /// </summary>
    public interface IChartStyleService : IHostedService, IOnBarUpdateService, IOnMarketDataService
    {
    }
}
