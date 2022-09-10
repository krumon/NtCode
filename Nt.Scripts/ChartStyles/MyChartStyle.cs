using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.ChartStyles;

namespace Nt.Scripts
{
	/*
    public class MyChartStyle : ChartStyle
    {
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description = @"Enter the description for your new custom Chart style here.";
				Name = "MyCustomChartStyle";
				ChartStyleType = (ChartStyleType)11;
				BarWidth = 2;
				MyParameter = 1;
			}
			else if (State == State.ConfigureSessionsManager)
			{
			}
		}

		public override bool IsTransparent { get { return false; } } // replace with your logic to check if entire style is transparent

		public override int GetBarPaintWidth(int barWidth)
		{
			return 1; //replace with your bar width logic
		}

		public override void OnRender(ChartControl chartControl, ChartScale chartScale, ChartBars chartBars)
		{
			// replace with your custom rendering logic
		}


		#region Properties

		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name = "MyParameter", Order = 1, GroupName = "Parameters")]
		public int MyParameter
		{ get; set; }

		#endregion

	}
	*/
}
