namespace Nt.Core.Providers
{
    public interface ISessionBuilder
    {
        ISessionProvider Build();
    }
}