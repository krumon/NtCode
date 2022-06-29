using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    public class Test : NinjaScriptBase
    {
        #region Implementation methods

        public override string LogTypeName => throw new NotImplementedException();

        #endregion

        #region Override methods

        protected override void OnBarUpdate()
        {

            base.OnBarUpdate();

            var high = High[0];
            var low = Low[0];
            var close = Close[0];
            var open = Open[0];

        }

        
        
        #endregion

    }
}
