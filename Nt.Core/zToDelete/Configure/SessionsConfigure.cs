namespace Nt.Core
{

    #region Interfaces

    /// <summary>
    /// Session ConfigureSessionHoursList implementation
    /// </summary>
    public interface ISessionConfigure
    {
        int initialBalance { get; set; }
        int finalBalance { get; set; }
        bool includeSessionBetweenBalances { get; set; }
        bool includeEarlyEnd { get; set; }
        bool includeLateBegin { get; set; }
    }

    /// <summary>
    /// SessionHours configure implementation
    /// </summary>
    public interface ISessionsConfigure : ISessionConfigure
    {

    }

    #endregion

    #region Abstract classes

    /// <summary>
    /// Represents the base session configure.
    /// </summary>
    public abstract class BaseSessionConfigure : ISessionConfigure
    {
        public int initialBalance { get; set; }
        public int finalBalance { get; set; }
        public bool includeSessionBetweenBalances { get; set; }
        public bool includeEarlyEnd { get; set; }
        public bool includeLateBegin { get; set; }
    }

    /// <summary>
    /// Represents the base sessionHoursList configure.
    /// </summary>
    public abstract class BaseSessionsConfigure : BaseSessionConfigure, ISessionsConfigure
    {

    }

    #endregion

    #region Generic and custom session configure

    /// <summary>
    /// Represents the <see cref="TradingSession"/> configure.
    /// </summary>
    public class GenericSessionConfigure : BaseSessionConfigure
    {
    }

    /// <summary>
    /// Represents the <see cref="TradingSession"/> default configure.
    /// </summary>
    public class GenericSessionDefaultConfigure : GenericSessionConfigure
    {
    }

    /// <summary>
    /// Represents the custom session configure.
    /// </summary>
    public class CustomSessionConfigure : BaseSessionConfigure
    {
    }

    /// <summary>
    /// Represents the custom session default configure.
    /// </summary>
    public class CustomSessionDefaultConfigure : CustomSessionConfigure
    {
    }

    #endregion

    #region Sessions configure

    /// <summary>
    /// Represents the generic <see cref="TradingSessions"/> configure.
    /// </summary>
    public class GenericSessionsConfigure : BaseSessionsConfigure
    {
        // SessionHours
        //public bool includeAmericanRegularSession = true;
        //public bool includeAmericanOvernightSession = true;
        //public bool includeAmericanAndEuropeanSession = true;
        //public bool includeAssianSession = true;
        //public bool includeEuropeanSession = true;
        //public bool includeAmericanResidualSession = true;
        //public bool includeAsianResidualSession = false;
        // Balances
    }

    /// <summary>
    /// Represents the generic <see cref="TradingSessions"/> default configure.
    /// </summary>
    public class GenericSessionsDefaultConfigure : GenericSessionsConfigure
    {

    }

    /// <summary>
    /// Represents the custom session configure.
    /// </summary>
    public class CustomSessionsConfigure : BaseSessionsConfigure
    {
    }

    /// <summary>
    /// Represents the custom session default configure.
    /// </summary>
    public class CustomSessionsDefaultConfigure : CustomSessionsConfigure
    {

    }

    #endregion

}
