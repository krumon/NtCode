namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// Represents properties and methods of ninjascript instance in ninjatrader plattform.
    /// </summary>
    public interface INinjascript 
    {
    }

    /// <summary>
    /// A generic interface for ninjascripts where the category name is derived from the specified
    /// TCategoryName type name. Generally used to enable activation of a named INinjascriptProvider
    /// from dependency injection.
    /// </summary>
    /// <typeparam name="TCategoryName">The type whose name is used for the ninjascript category name.</typeparam>
    public interface INinjascript<out TCategoryName> : INinjascript
    {
    }
}
