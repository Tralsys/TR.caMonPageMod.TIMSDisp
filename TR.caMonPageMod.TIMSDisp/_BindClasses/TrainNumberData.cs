using System.ComponentModel;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	public class TrainNumberData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		private string __Prefix = null;
		public string Prefix
		{
			get => __Prefix;
			set
			{
				if (Prefix == value)
					return;

				__Prefix = value;
				OnPropertyChanged(nameof(Prefix));
			}
		}

		private int __Number = 0;
		public int Number
		{
			get => __Number;
			set
			{
				if (Number == value)
					return;

				__Number = value;
				OnPropertyChanged(nameof(Number));
			}
		}
		private string __Suffix = null;
		public string Suffix
		{
			get => __Suffix;
			set
			{
				if (Suffix == value)
					return;

				__Suffix = value;
				OnPropertyChanged(nameof(Suffix));
			}
		}

	}
}
