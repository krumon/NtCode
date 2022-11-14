using System;

namespace Nt.Core.Data
{
    public interface IServiceProviderBuilder
    {

        /// <summary>
        /// Run the given actions to initialize the service provider. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="IServiceProvider"/></returns>
        IServiceProvider Build();

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="INinjascriptServiceCollection"/>
        /// that will be used to construct the <see cref="INinjascriptServiceProvider"/>.</param>
        /// <returns>The same instance of the <see cref="IServiceProviderBuilder"/> for chaining.</returns>
        IServiceProviderBuilder ConfigureService(Action<IServiceCollection<IServiceDescriptor>> configureDelegate);

    }

    public interface IServiceProviderBuilder<TProvider, TBuilder, TCollection,TDescriptor>
        where TProvider : class, IServiceProvider
        where TBuilder : class, IServiceProviderBuilder<TProvider, TBuilder,TCollection,TDescriptor>
        where TCollection : BaseServiceCollection<TDescriptor>
        where TDescriptor : IServiceDescriptor
    {
        /// <summary>
        /// Run the given actions to initialize the service provider. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="IServiceProvider"/></returns>
        TProvider Build();

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="INinjascriptServiceCollection"/>
        /// that will be used to construct the <see cref="INinjascriptServiceProvider"/>.</param>
        /// <returns>The same instance of the <see cref="IServiceProviderBuilder"/> for chaining.</returns>
        TBuilder ConfigureService(Action<TCollection> configureDelegate);

    }
}