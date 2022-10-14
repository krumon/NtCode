namespace Nt.Core.Hosting
{
    /// <summary>
    /// Specifies the ninjascript service.
    /// </summary>
    public enum ServiceLifetime
    {

        //
        // Resumen:
        //     Specifies that a single instance of the service will be created.
        /// <summary>
        /// Specifies that a single instance of the service will be created.
        /// </summary>
        Singleton,

        /// <summary>
        /// Specifies that a new instance of the service will be created for each scope.
        /// </summary>
        Scoped,

        /// <summary>
        /// Specifies that a new instance of the service will be created every time it is requested.
        /// </summary>
        Transient

    }
}
