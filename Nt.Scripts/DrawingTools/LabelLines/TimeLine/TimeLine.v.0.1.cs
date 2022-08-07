using NtCore;

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
				Name = NtCore.Texts.TimeLineDisplayName;
				Anchor.DisplayName = NtCore.Texts.AnchorXDisplayName;
				Anchor.IsYPropertyVisible = false;
				DisplayOnChartsMenus = true;
			}
		}
	}
}
