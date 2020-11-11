using System.Windows.Controls;

using TR.caMonPageMod.TIMSDisp._BindClasses;

namespace TR.caMonPageMod.TIMSDisp.Driver.D01
{
	/// <summary>
	/// A_5Row.xaml の相互作用ロジック
	/// </summary>
	public partial class A_5Row : Page
	{
		public StationData Row0 { get; private set; }
		= new StationData
		{
			StationName = "しけん"
		};
		public StationData Row1 { get; private set; }
		public StationData Row2 { get; private set; }
		public StationData Row3 { get; private set; }
		public StationData Row4 { get; private set; }
		public StationData NextStop { get; private set; }
		= new StationData()
		{
			StationName = "テスト",
			ArrTime = new TimeData()
			{
				HH = 12,
				IsHHVisible = true,
				IsMMVisible = false,
				IsSeparatorVisible = true,
				IsSSVisible = true,
				IsVisible = true,
				MM = 0,
				Separator = "=",
				SS = 99
			},
			TrackNumber = "N/A",
			IsTrackNumberVisible = true
		};

		public bool IsNextStopComing { get; set; } = false;

		public A_5Row()
		{
			InitializeComponent();

			DataContext = this;
		}
	}
}
