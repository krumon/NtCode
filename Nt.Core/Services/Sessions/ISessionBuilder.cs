namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the properties and methods to built a <see cref="SessionService"/>.
    /// </summary>
    public interface ISessionBuilder
    {

        ISessionBuilder AddFilters();
        ISessionBuilder AddGenericSessions();
        ISessionBuilder AddCustomSessions();
        ISessionBuilder AddStats();
        TImplementation Build<TImplementation>() where TImplementation: ISessionService, new();

    }
}
