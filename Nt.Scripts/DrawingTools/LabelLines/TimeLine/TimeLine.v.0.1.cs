using Nt.Core;

namespace NinjaTrader.NinjaScript.DrawingTools
{
    public class TimeLine : LabelLine
    {

		#region Public properties		

		/// <summary>
		/// The icon to display in the menu item.
		/// </summary>
		public override object Icon { get { return Gui.Tools.Icons.DrawVertLineTool; } }

		#endregion

		protected override void OnStateChange()
		{
			// Call to parent.
			base.OnStateChange();

			if (State == State.SetDefaults)
			{
				LabelType = LabelLineType.Time;
				Name = Nt.Core.Texts.TimeLineDisplayName;
				Anchor.DisplayName = Nt.Core.Texts.AnchorXDisplayName;
				Anchor.IsYPropertyVisible = false;
				DisplayOnChartsMenus = true;
			}
		}
	}
}
