namespace Nt.Core.Options
{
    public interface IOptions<TOptions>
        where TOptions : class
    {
        /// <summary>
        ///  Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        void Configure(TOptions options);
    }
}
