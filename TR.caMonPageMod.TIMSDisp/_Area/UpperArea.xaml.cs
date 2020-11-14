using System.Windows.Controls;

using TR.caMonPageMod.TIMSDisp._BindClasses;

namespace TR.caMonPageMod.TIMSDisp._Area
{
	/// <summary>
	/// UpperArea.xaml の相互作用ロジック
	/// </summary>
	public partial class UpperArea : Page
	{
		UpperArea_toBind toB = new UpperArea_toBind()
		{
			RadioData = new UpperArea_RadioData()
			{
				IsJohoEnabled = false,
				IsMonitoringEnabled = false,
				IsTsukokuEnabled = false,
				KiseiBtn = new UpperArea_RadioData.ButtonSetting(),
				ShireiBtn = new UpperArea_RadioData.ButtonSetting(),
				TsukokuBtn = new UpperArea_RadioData.ButtonSetting(),
				UnkoBtn = new UpperArea_RadioData.ButtonSetting(),
			},

			TSLData = new UpperArea_TSLData()
			{
				HH = 0,
				MM = 1,
				SS = 2,
				Speed = 9999,
				IsLocationEnabled = true,
				Location = (double)123456789 / 10
			}
		};
		public UpperArea()
		{
			InitializeComponent();

			DataContext = toB;
		}


	}
}
