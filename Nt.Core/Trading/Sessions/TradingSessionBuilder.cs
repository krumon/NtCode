using Kr.Core.Helpers;
using Nt.Core.Trading.Internal;
using System;
using System.Collections.Generic;

namespace Nt.Core.Trading
{
    public class TradingSessionBuilder : ITradingSessionBuilder
    {
        #region Private members

        private IList<TradingSessionType> _types = new List<TradingSessionType>();
        private TradingSessionCollection _sessionsCache;
        private readonly TradingSession _tradingSession;
        private bool _isBuild;

        #endregion

        #region Implementation methods

        public ITradingSession Build()
        {
            _isBuild = true;
            AddTypesToTradingSessionCollection();
            SortTradingSessionCollection();
            CreateTradingSession();
            return _tradingSession;
        }

        #endregion

        #region Public Methods

        public ITradingSessionBuilder Add(TradingSessionType type)
        {
            if (_types.Contains(type))
                return this;
            _types.Add(type);
            return this;
        }

        public ITradingSessionBuilder Add(params TradingSessionType[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            foreach (var type in types)
                Add(type);

            return this;
        }

        public ITradingSessionBuilder Add<T>()
            where T : Enum
        {
            if (typeof(T).Name == nameof(TradingSessionType))
            {
                EnumHelpers.ForEach<TradingSessionType>((type) =>
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
            throw new NotImplementedException();
        }

        private void SortTradingSessionCollection()
        {
            throw new NotImplementedException();
        }

        private void CreateTradingSession()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
