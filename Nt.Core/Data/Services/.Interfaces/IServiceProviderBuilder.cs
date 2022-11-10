using System;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    public interface IServiceProviderBuilder
    {

        ///// <summary>
        ///// A central location for sharing state between components during the host building process.
        ///// </summary>
        //IDictionary<object, object> Properties { get; }

        /// <summary>
        /// Run the given actions to initialize the service provider. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="IServiceProvider"/></returns>
        IServiceProvider Build();

        ///// <summary>
        ///// Adds services to the container. This can be called multiple times and the results will be additive.
        ///// </summary>
        ///// <param name="configureDelegate">The delegate for configuring the <see cref="INinjascriptServiceCollection"/>
        ///// that will be used to construct the <see cref="INinjascriptServiceProvider"/>.</param>
        ///// <returns>The same instance of the <see cref="IServiceProviderBuilder"/> for chaining.</returns>
        //IServiceProviderBuilder ConfigureService(Action<IServiceCollection<IServiceDescriptor>> configureDelegate);

    }
}