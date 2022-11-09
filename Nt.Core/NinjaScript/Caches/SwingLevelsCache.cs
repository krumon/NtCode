﻿using NinjaTrader.NinjaScript;
using Nt.Core.Data;
using Nt.Core.Ninjascript;
using System;

namespace Nt.Core.Ninjascripts
{
    public class SwingLevelsCache : Cache<SwingPoint>
    {

        #region Private members

        private SwingPointsCache swingPointsCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SwingLevelsCache(NinjaScriptBase ninjascript, int capacity, int period, int minSwingStrength, int maxSwingStrength) : base(ninjascript,capacity,period)
        {
            swingPointsCache = new SwingPointsCache(ninjascript,capacity, period,1,5);
        }

        #endregion

        #region Implementation Methods

        public override SwingPoint CreateCacheElement()
        {
            throw new NotImplementedException();
        }

        public override void OnBarUpdated()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public methods


        #endregion

    }
}
