using Nt.Core.DependencyInjection;

namespace Nt.Scripts.Indicators
{
    public static class SessionsBuilderExtensions
    {
        public static ISessionsBuilder ConfigureSessionsIterator(this ISessionsBuilder builder)
        {
            builder.Services.AddSingleton<ISessionsIterator, SessionsIterator>();

            return builder;
        }

        public static ISessionsBuilder ConfigureSessionsFilter(this ISessionsBuilder builder)
        {
            builder.Services.AddSingleton<ISessionsFilters, SessionsFilters>();

            return builder;
        }
    }
}
