using System.ComponentModel;
using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	public class UpperArea_RadioData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		private bool __IsTsukokuEnabled;
		public bool IsTsukokuEnabled
		{
			get => __IsTsukokuEnabled;
			set
			{
				if (IsTsukokuEnabled == value)
					return;

				__IsTsukokuEnabled = value;
				OnPropertyChanged(nameof(IsTsukokuEnabled));
			}
		}

		private bool __IsJohoEnabled;
		public bool IsJohoEnabled
		{
			get => __IsJohoEnabled;
			set
			{
				if (IsJohoEnabled == value)
					return;

				__IsJohoEnabled = value;
				OnPropertyChanged(nameof(IsJohoEnabled));
			}
		}

		private bool __IsMonitoringEnabled;
		public bool IsMonitoringEnabled
		{
			get => __IsMonitoringEnabled;
			set
			{
				if (IsMonitoringEnabled == value)
					return;

				__IsMonitoringEnabled = value;
				OnPropertyChanged(nameof(IsMonitoringEnabled));
			}
		}


		private ButtonSetting __TsukokuBtn = null;
		public ButtonSetting TsukokuBtn
		{
			get => __TsukokuBtn;
			set
			{
				__TsukokuBtn = value;
				OnPropertyChanged(nameof(TsukokuBtn));
			}
		}

		private ButtonSetting __KiseiBtn = null;
		public ButtonSetting KiseiBtn
		{
			get => __KiseiBtn;
			set
			{
				__KiseiBtn = value;
				OnPropertyChanged(nameof(KiseiBtn));
			}
		}

		private ButtonSetting __ShireiBtn = null;
		public ButtonSetting ShireiBtn
		{
			get => __ShireiBtn;
			set
			{
				__ShireiBtn = value;
				OnPropertyChanged(nameof(ShireiBtn));
			}
		}

		private ButtonSetting __UnkoBtn = null;
		public ButtonSetting UnkoBtn
		{
			get => __UnkoBtn;
			set
			{
				__UnkoBtn = value;
				OnPropertyChanged(nameof(UnkoBtn));
			}
		}

		public class ButtonSetting : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;
			private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

			private bool __IsBlinking;
			public bool IsBlinking
			{
				get => __IsBlinking;
				set
				{
					if (IsBlinking == value)
						return;

					__IsBlinking = value;
					OnPropertyChanged(nameof(IsBlinking));
				}
			}

			private Brush __UsualTextColor = Brushes.Aqua;
			public Brush UsualTextColor
			{
				get => __UsualTextColor;
				set
				{
					__UsualTextColor = value;
					OnPropertyChanged(nameof(UsualTextColor));
				}
			}
			private Brush __UsualBackground = Brushes.Black;
			public Brush UsualBackground
			{
				get => __UsualBackground;
				set
				{
					__UsualBackground = value;
					OnPropertyChanged(nameof(UsualBackground));
				}
			}

		}
	}
}
