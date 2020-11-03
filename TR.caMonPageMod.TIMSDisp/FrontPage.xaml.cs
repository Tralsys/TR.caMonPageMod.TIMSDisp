using System;
using System.Windows.Controls;
using System.Windows.Threading;

using TR.caMonPageMod.TIMSDisp._Area;

namespace TR.caMonPageMod.TIMSDisp
{
	/// <summary>
	/// FrontPage.xaml の相互作用ロジック
	/// </summary>
	public partial class FrontPage : Page
	{
		public static bool DT300_TFLoop { get; private set; } = false;
		public static event EventHandler DT300Tick { add => DT300.Tick += value; remove => DT300.Tick -= value; }
		static DispatcherTimer DT300 = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 300) };

		public static bool DT400_TFLoop { get; private set; } = false;
		public static event EventHandler DT400Tick { add => DT400.Tick += value; remove => DT400.Tick -= value; }
		static DispatcherTimer DT400 = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 400) };

		static readonly double[] BLRecOpacitySettings = new double[5] { 0.6, 0.45, 0.3, 0.15, 0.0 };

		static FrontPage()
		{
			DT300Tick += (s, e) => DT300_TFLoop = !DT300_TFLoop;
			DT400Tick += (s, e) => DT400_TFLoop = !DT400_TFLoop;
		
			DT300.Start();
			DT400.Start();
		}

		public FrontPage() => InitializeComponent();
		

		private void FrontPage_BLBtnCVChanged(object sender, ValueChangedEventArgs<int> e)
			=> BLRec.Opacity = BLRecOpacitySettings[e.NewValue];

		private void FrontPage_SPBtnCVChanged(object sender, ValueChangedEventArgs<int> e)
			=> CommonMethods.VolumeSettingUpdated(e.NewValue);

		private void LowerAreaRendered(object sender, EventArgs e)
		{
			if ((sender as Frame)?.Content is LowerArea la)
			{
				la.BLBtnCVChanged += FrontPage_BLBtnCVChanged;
				la.SPBtnCVChanged += FrontPage_SPBtnCVChanged;
			}
		}
	}
}
