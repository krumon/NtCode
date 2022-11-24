namespace Nt.Core.Data
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
