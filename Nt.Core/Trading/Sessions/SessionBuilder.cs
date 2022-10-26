using Kr.Core.Helpers;
using Nt.Core.Trading.Internal;
using System;
using System.Collections.Generic;

namespace Nt.Core.Trading
{
    public class SessionBuilder : ISessionBuilder
    {
        #region Private members

        private IList<SessionCode> _types = new List<SessionCode>();
        private SessionCollection _descriptors;
        private readonly TradingSession _tradingSession = new TradingSession();
        private bool _isBuild;

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

        public ISessionBuilder Add(SessionCode type)
        {
            if (_types.Contains(type))
                return this;
            _types.Add(type);
            return this;
        }

        public ISessionBuilder Add(params SessionCode[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            foreach (var type in types)
                Add(type);

            return this;
        }

        public ISessionBuilder Add<T>()
            where T : Enum
        {
            if (typeof(T).Name == nameof(SessionCode))
            {
                EnumHelpers.ForEach<SessionCode>((type) =>
                {
                    Add(type);
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
