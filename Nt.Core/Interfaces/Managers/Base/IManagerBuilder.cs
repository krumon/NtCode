using System;

namespace Nt.Core
{

    /// <summary>
    /// Interface for any manager builder.
    /// </summary>
    public interface IManagerBuilder : IBuilder
    {

        /// <summary>
        /// Adds one <see cref="INinjascript"/> object to the ninjascripts collection.
        /// </summary>
        /// <typeparam name="Script">The <see cref="INinjascript"/> object to add object.</typeparam>
        /// <typeparam name="Options">The <see cref="INinjascript"/> configuration object.</typeparam>
        /// <param name="options">The specific configuration to add.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        IManagerBuilder Add<Script, Options>(Action<Options> options)
            where Script : INinjascript;

    }

}
