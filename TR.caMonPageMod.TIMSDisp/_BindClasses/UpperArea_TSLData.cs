using System.ComponentModel;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	class UpperArea_TSLData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		#region Time
		private int __HH = 0;
		public int HH
		{
			get => __HH;
			set
			{
				if (HH == value)
					return;

				__HH = value;
				OnPropertyChanged(nameof(HH));
			}
		}

		private int __MM = 0;
		public int MM
		{
			get => __MM;
			set
			{
				if (MM == value)
					return;

				__MM = value;
				OnPropertyChanged(nameof(MM));
			}
		}

		private int __SS = 0;
		public int SS
		{
			get => __SS;
			set
			{
				if (SS == value)
					return;

				__SS = value;
				OnPropertyChanged(nameof(SS));
			}
		}
		#endregion Time

		#region Speed
		private int __Speed = 0;
		public int Speed
		{
			get => __Speed;
			set
			{
				if (Speed == value)
					return;

				__Speed = value;
				OnPropertyChanged(nameof(Speed));
			}
		}
		#endregion Speed

		#region Location
		private bool __IsLocationEnabled = false;
		public bool IsLocationEnabled
		{
			get => __IsLocationEnabled;
			set
			{
				if (IsLocationEnabled == value)
					return;

				__IsLocationEnabled = value;
				OnPropertyChanged(nameof(IsLocationEnabled));
			}
		}

		private double __Location = 0.0;
		public double Location
		{
			get => __Location;
			set
			{
				if (Location == value)
					return;

				__Location = value;
				OnPropertyChanged(nameof(Location));
			}
		}
		#endregion Location
	}
}
