﻿using Nt.Core.Hosting;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the ninjatrader chart bars style to use bay the ninjascript objects.
    /// </summary>
    public interface IChartStyleService : IHostedService, IOnBarUpdateService, IOnMarketDataService
    {
    }
}
