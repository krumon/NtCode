namespace Nt.Core.Hosting
{

    /// <summary>
    /// Represents ninjatrader configure services.
    /// </summary>
    public interface IConfigurable
    {
        /// <summary>
        /// Method to configure the service.        
        /// </summary>
        void Configure();

        /// <summary>
        /// indicates whether the service is configured. 
        /// </summary>
        bool IsConfigured { get; }

    }
}
