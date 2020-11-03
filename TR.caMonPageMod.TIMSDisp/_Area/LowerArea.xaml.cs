using System;
using System.ComponentModel;
using System.Windows.Controls;

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

			DataContext = bs;
		}
		ButtonSetting bs = new ButtonSetting();
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

		public class ButtonSetting : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;
			private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

			private string __L1BText = "L1B";
			public string L1BText
			{
				get => __L1BText;
				set
				{
					if (L1BText == value)
						return;

					__L1BText = value;
					OnPropertyChanged(nameof(L1BText));
				}
			}
			private string __L2BText = "L2B";
			public string L2BText
			{
				get => __L2BText;
				set
				{
					if (L2BText == value)
						return;

					__L2BText = value;
					OnPropertyChanged(nameof(L2BText));
				}
			}
			private string __L3BText = "L3B";
			public string L3BText
			{
				get => __L3BText;
				set
				{
					if (L3BText == value)
						return;

					__L3BText = value;
					OnPropertyChanged(nameof(L3BText));
				}
			}
			private string __L4BText = "L4B";
			public string L4BText
			{
				get => __L4BText;
				set
				{
					if (L4BText == value)
						return;

					__L4BText = value;
					OnPropertyChanged(nameof(L4BText));
				}
			}
		}

		private void SPBtn_OnCVChanged(object s, ValueChangedEventArgs<int> e)
			=> SPBtnCVChanged?.Invoke(s, e);
		private void BLBtn_OnCVChanged(object s, ValueChangedEventArgs<int> e)
			=> BLBtnCVChanged?.Invoke(s, e);
	}
}
