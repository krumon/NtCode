namespace Nt.Core.Trading
{
    public interface ISessionBuilder
    {
        ISessionProvider Build();
    }
}