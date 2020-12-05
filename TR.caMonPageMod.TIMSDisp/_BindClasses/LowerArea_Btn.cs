using System.ComponentModel;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	public class LowerArea_Btn : INotifyPropertyChanged
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


		private bool __IsL1BEnabled = false;
		public bool IsL1BEnabled
		{
			get => __IsL1BEnabled;
			set
			{
				if (IsL1BEnabled == value)
					return;

				__IsL1BEnabled = value;
				OnPropertyChanged(nameof(IsL1BEnabled));
			}
		}
		private bool __IsL2BEnabled = false;
		public bool IsL2BEnabled
		{
			get => __IsL2BEnabled;
			set
			{
				if (IsL2BEnabled == value)
					return;

				__IsL2BEnabled = value;
				OnPropertyChanged(nameof(IsL2BEnabled));
			}
		}
		private bool __IsL3BEnabled = false;
		public bool IsL3BEnabled
		{
			get => __IsL3BEnabled;
			set
			{
				if (IsL3BEnabled == value)
					return;

				__IsL3BEnabled = value;
				OnPropertyChanged(nameof(IsL3BEnabled));
			}
		}
		private bool __IsL4BEnabled = false;
		public bool IsL4BEnabled
		{
			get => __IsL4BEnabled;
			set
			{
				if (IsL4BEnabled == value)
					return;

				__IsL4BEnabled = value;
				OnPropertyChanged(nameof(IsL4BEnabled));
			}
		}

	}
}
