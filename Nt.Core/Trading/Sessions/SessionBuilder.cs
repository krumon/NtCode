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
        private readonly TradingSession _tradingSession = new TradingSession();
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

        public ITradingSession Build()
        {
            // The trading session can be only once time created.
            if (_isBuild)
                return _tradingSession;

            AddTypesToTradingSessionCollection();
            SortTradingSessionCollection();
            CreateTradingSession();

            // Sets the flag to indicate the Trading session is created.
            _isBuild = true;

            return _tradingSession;
        }

        #endregion

        #region Public Methods

        public ISessionBuilder AddTradingSessionConfigure()
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

        #endregion

    }
}
