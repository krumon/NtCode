namespace Nt.Core
{
    /// <summary>
    /// The interfece for any script element.
    /// </summary>
    public interface IOptions<TOptions> : IElement
        where TOptions : IOptions<TOptions>
    {

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        void CopyTo(TOptions options);

        #endregion
    }

    /// <summary>
    /// The interfece for any script element.
    /// </summary>
    public interface IOptions : IOptions<IOptions>
    {
    }
}
