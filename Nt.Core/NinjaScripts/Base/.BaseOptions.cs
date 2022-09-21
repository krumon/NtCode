namespace Nt.Core
{
    /// <summary>
    /// The base class for all ninjascripts options to configure.
    /// </summary>
    public abstract class BaseOptions<TOptions> : BaseElement, IOptions<TOptions>
        where TOptions : BaseOptions<TOptions>, new()
    {

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        // TODO: Cambiar por atributos en las propiedades [Script], [Indicator], [Strategy]
        public virtual void CopyTo(TOptions options)
        {
        }

        #endregion

    }

    /// <summary>
    /// The base class for all ninjascripts options to configure.
    /// </summary>
    public class Options : BaseOptions<Options>, IOptions
    {
        public void CopyTo(IOptions options)
        {
            base.CopyTo((Options)options);
        }
    }

}
