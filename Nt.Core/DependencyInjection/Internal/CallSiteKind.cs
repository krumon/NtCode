namespace Nt.Core.DependencyInjection.Internal
{
    internal enum CallSiteKind
    {
        Factory,
        Constructor,
        Constant,
        IEnumerable,
        ServiceProvider,
        Scope,
        Transient,
        Singleton
    }
}
