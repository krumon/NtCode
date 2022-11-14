namespace Nt.Core.Data
{
    /// <summary>
    /// Default service provider host.
    /// </summary>
    public class ServicesHost : IServicesHost
    {
        public IServiceProvider[] Services => throw new System.NotImplementedException();

        public IServiceProvider GetServiceProvider(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}
