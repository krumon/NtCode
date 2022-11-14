﻿using System;

namespace Nt.Core.Data
{
    public interface IHostBuilder
    {

        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="INinjascriptHost"/></returns>
        IHostService Build();

        /// <summary>
        /// Adds host options to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="optionsDelegate">The delegate for configuring the <see cref="IHostService"/>.
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureHostOptions(Action<HostOptions> optionsDelegate);

        /// <summary>
        /// Adds data series services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="dataSeriesDelegate">The delegate for configuring the <see cref="DataSeriesService"/>
        /// that will be used to construct the <see cref="DataSeriesProvider"/>.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder UseDataSeries(Action<DataSeriesBuilder> dataSeriesDelegate);
    }
}