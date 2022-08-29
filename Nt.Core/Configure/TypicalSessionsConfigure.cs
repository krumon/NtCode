namespace Nt.Core
{
    /// <summary>
    /// Represents the <see cref="UserSession"/> configure.
    /// </summary>
    public class TypicalSessionsConfigure
    {
        // Sessions
        public bool includeAmericanRegularSession = true;
        public bool includeAmericanOvernightSession = true;
        public bool includeAmericanAndEuropeanSession = true;
        public bool includeAssianSession = true;
        public bool includeEuropeanSession = true;
        public bool includeAmericanResidualSession = true;
        public bool includeAsianResidualSession = false;

        // Balances


    }

    /// <summary>
    /// Represents the <see cref="UserSession"/> default configure.
    /// </summary>
    public class UserSessionDefaultConfigure : TypicalSessionsConfigure
    {

    }


}
