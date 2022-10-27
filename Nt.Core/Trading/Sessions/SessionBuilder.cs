using Kr.Core.Helpers;
using Nt.Core.Trading.Internal;
using System;
using System.Collections.Generic;

namespace Nt.Core.Trading
{
    public class SessionBuilder : ISessionBuilder
    {
        #region Private members

        private SessionDescriptorCollection _descriptors = new SessionDescriptorCollection();
        private SessionFactory _sessionFactory;
        private SessionProvider _sessionProvider;
        private List<Func<SessionProviderConfiguration, SessionBuilder>> _sessionProviderConfigureActions;
        private Dictionary<string, Func<TradingSessionConfiguration, SessionProvider>> _tradingSessionConfigureActions;
        private bool _isBuild;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="SessionBuilder"/> default instance.
        /// </summary>
        public SessionBuilder()
        {

        }

        #endregion

        #region Implementation methods

        public ISessionProvider Build()
        {
            // The trading session can be only once time created.
            if (_isBuild)
                return _sessionProvider;

            if (_descriptors == null || _descriptors.Count < 1)
                CreateDefaultSessionProvider();
            else
            {
                _sessionFactory = new SessionFactory(_descriptors);
                AddTypesToTradingSessionCollection();
                SortTradingSessionCollection();
                CreateTradingSession();
            }

            // Sets the flag to indicate the Trading session is created.
            _isBuild = true;

            return _sessionProvider;
        }

        #endregion

        #region Public Methods

        public ISessionBuilder AddSessionConfigure()
        {
            throw new NotImplementedException();
        }

        public ISessionBuilder AddDescriptor(SessionType type)
        {
            SessionDescriptor descriptor = SessionDescriptor.CreateTradingSessionByType(type);
            _descriptors.Add(descriptor);
            return this;
        }

        public ISessionBuilder AddDescriptors(params SessionType[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            foreach (var type in types)
                AddDescriptor(type);

            return this;
        }

        public ISessionBuilder Add<T>()
            where T : Enum
        {
            if (typeof(T).Name == nameof(SessionType))
            {
                EnumHelpers.ForEach<SessionType>((type) =>
                {
                    AddDescriptor(type);
                });
            }

            return this;
        }

        #endregion

        #region Private methods

        private void AddTypesToTradingSessionCollection()
        {
            //throw new NotImplementedException();
        }

        private void SortTradingSessionCollection()
        {
            //throw new NotImplementedException();
        }

        private void CreateTradingSession()
        {
            //throw new NotImplementedException();
        }

        private void CreateDefaultSessionProvider()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
