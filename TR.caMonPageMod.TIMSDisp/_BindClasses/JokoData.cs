using System.ComponentModel;

using TR.caMonPageMod.TIMSDisp._Interfaces;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	public class JokoData : INotifyPropertyChanged, IJokoData
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		private double __StartDistance;
		public double StartDistance
		{
			get => __StartDistance;
			set
			{
				if (StartDistance == value)
					return;

				__StartDistance = value;
				OnPropertyChanged(nameof(StartDistance));
			}
		}

		private double __EndDistance;
		public double EndDistance
		{
			get => __EndDistance;
			set
			{
				if (EndDistance == value)
					return;

				__EndDistance = value;
				OnPropertyChanged(nameof(EndDistance));
			}
		}

		private int __LimitSpeed;
		public int LimitSpeed
		{
			get => __LimitSpeed;
			set
			{
				if (LimitSpeed == value)
					return;

				__LimitSpeed = value;
				OnPropertyChanged(nameof(LimitSpeed));
			}
		}
	}
}
