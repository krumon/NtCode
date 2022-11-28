namespace Nt.Core.Data
{
    public interface IHost
    {
        /// <summary>
        /// The ninjascript configured services.
        /// </summary>
        IServiceProvider Services { get; }

    }
}
