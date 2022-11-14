namespace Nt.Core.Data
{

    /// <summary>
    /// Defines an engine to gets a service object.
    /// </summary>
    public interface IOptions<TOptions>
        where TOptions : IOptions<TOptions>
    {

        /// <summary>
        /// Gets service options instance.
        /// </summary>
        TOptions Value{get;}

    }
}
