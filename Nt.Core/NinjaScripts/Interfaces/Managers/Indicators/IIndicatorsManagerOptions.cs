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
    public interface IIndicatorsManagerOptions<TIndicatorsManagerOptions> : IManagerOptions<TIndicatorsManagerOptions>, IIndicatorsManagerOptions
        where TIndicatorsManagerOptions : IIndicatorsManagerOptions<TIndicatorsManagerOptions>
    {
    }

}
