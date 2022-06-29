using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaTrader.Cbi;
using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;

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

    public class Rendering : DrawingTool
    {
        public override void OnRender(ChartControl chartControl, ChartScale chartScale)
        {
            base.OnRender(chartControl, chartScale);
        }
    }

    public class IndicatorTester : IndicatorBase
    {
        public override string LogTypeName => throw new NotImplementedException();

        public override void SetState(State state) => throw new NotImplementedException();

        protected override void OnBarUpdate()
        {
            base.OnBarUpdate();

            var high = High[0];
            var low = Low[0];
            var close = Close[0];
            var open = Open[0];
        }

    }

    public class StrategyTester : StrategyBase
    {
        protected override void OnOrderUpdate(Order order, double limitPrice, double stopPrice, int quantity, int filled, double averageFillPrice, OrderState orderState, DateTime time, ErrorCode error, string comment)
        {
            base.OnOrderUpdate(order, limitPrice, stopPrice, quantity, filled, averageFillPrice, orderState, time, error, comment);
        }
    }
}
