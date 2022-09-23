namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseIndicatorsManagerOptions<TOptions> : BaseManagerOptions<TOptions>, IIndicatorsManagerOptions<TOptions>
        where TOptions : BaseIndicatorsManagerOptions<TOptions>, new()
    {
    }
}
