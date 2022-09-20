namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseIndicatorsManagerOptions<TOptions> : BaseIndicatorOptions<TOptions>
        where TOptions : BaseIndicatorsManagerOptions<TOptions>, new()
    {
    }
}
