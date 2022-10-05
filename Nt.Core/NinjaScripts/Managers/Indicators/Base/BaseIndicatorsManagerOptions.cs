namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseIndicatorsManagerOptions<TOptions> : BaseManagerOptions<TOptions>, IIndicatorsManagerOptions
        where TOptions : BaseIndicatorsManagerOptions<TOptions>, IIndicatorsManagerOptions
    {
    }
}
