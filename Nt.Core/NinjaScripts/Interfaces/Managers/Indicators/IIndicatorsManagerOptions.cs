namespace Nt.Core
{

    /// <summary>
    /// Interface for any indicators manager options.
    /// </summary>
    public interface IIndicatorsManagerOptions : IManagerOptions
    {
    }

    /// <summary>
    /// Interface for any indicators manager options.
    /// </summary>
    public interface IIndicatorsManagerOptions<TManagerOptions> : IManagerOptions<TManagerOptions>, IIndicatorsManagerOptions
        where TManagerOptions : IIndicatorsManagerOptions<TManagerOptions>
    {
    }

}
