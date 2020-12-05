using System;
using System.ComponentModel;
using System.Windows.Controls;

using TR.caMonPageMod.TIMSDisp._BindClasses;

namespace TR.caMonPageMod.TIMSDisp._Area
{
	/// <summary>
	/// LowerArea.xaml の相互作用ロジック
	/// </summary>
	public partial class LowerArea : Page
	{
		public event EventHandler<ValueChangedEventArgs<int>> SPBtnCVChanged;
		public event EventHandler<ValueChangedEventArgs<int>> BLBtnCVChanged;

		public LowerArea()
		{
			InitializeComponent();

			toB.ButtonData = new LowerArea_Btn()
			{
				L1BText = "運転士\nメニュー",
				IsL1BEnabled = true,
				L2BText = "運転情報\n画面",
				IsL2BEnabled = true,
				L3BText = "応急マニ\nュアル",
				IsL3BEnabled = false,
				L4BText = null,
				IsL4BEnabled = false,
			};
			toB.NoticeData = new LowerArea_NoticeData()
			{
				IsKassoBlinking = false,
				IsKutenBlinking = false,
				IsOVDBlinking = false,
				IsSeigyoIjoBlinking = false
			};

			DataContext = toB;
		}
		LowerArea_toBind toB = new LowerArea_toBind();
		private void OnL1Pushed(object sender, EventArgs e)
		{

		}
		private void OnL2Pushed(object sender, EventArgs e)
		{

		}
		private void OnL3Pushed(object sender, EventArgs e)
		{

		}
		private void OnL4Pushed(object sender, EventArgs e)
		{

		}

		private void OnSPPushed(object sender, EventArgs e)
		{

		}

		private void OnBLPushed(object sender, EventArgs e)
		{

		}

		public class LowerArea_toBind : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;
			private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

			private LowerArea_Btn __ButtonData = null;
			public LowerArea_Btn ButtonData
			{
				get => __ButtonData;
				set
				{
					__ButtonData = value;
					OnPropertyChanged(nameof(ButtonData));
				}
			}

			private LowerArea_NoticeData __NoticeData = null;
			public LowerArea_NoticeData NoticeData
			{
				get => __NoticeData;
				set
				{
					__NoticeData = value;
					OnPropertyChanged(nameof(NoticeData));
				}
			}
		}

		private void SPBtn_OnCVChanged(object s, ValueChangedEventArgs<int> e)
			=> SPBtnCVChanged?.Invoke(s, e);
		private void BLBtn_OnCVChanged(object s, ValueChangedEventArgs<int> e)
			=> BLBtnCVChanged?.Invoke(s, e);
	}
}
