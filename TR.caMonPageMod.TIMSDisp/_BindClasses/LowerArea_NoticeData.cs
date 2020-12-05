using System.ComponentModel;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	public class LowerArea_NoticeData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		private bool __IsKutenBlinking = false;
		public bool IsKutenBlinking
		{
			get => __IsKutenBlinking;
			set
			{
				if (IsKutenBlinking == value)
					return;

				__IsKutenBlinking = value;
				OnPropertyChanged(nameof(IsKutenBlinking));
			}
		}

		private bool __IsKassoBlinking = false;
		public bool IsKassoBlinking
		{
			get => __IsKassoBlinking;
			set
			{
				if (IsKassoBlinking == value)
					return;

				__IsKassoBlinking = value;
				OnPropertyChanged(nameof(IsKassoBlinking));
			}
		}

		private bool __IsOVDBlinking = false;
		public bool IsOVDBlinking
		{
			get => __IsOVDBlinking;
			set
			{
				if (IsOVDBlinking == value)
					return;

				__IsOVDBlinking = value;
				OnPropertyChanged(nameof(IsOVDBlinking));
			}
		}

		private bool __IsSeigyoIjoBlinking = false;
		public bool IsSeigyoIjoBlinking
		{
			get => __IsSeigyoIjoBlinking;
			set
			{
				if (IsSeigyoIjoBlinking == value)
					return;

				__IsSeigyoIjoBlinking = value;
				OnPropertyChanged(nameof(IsSeigyoIjoBlinking));
			}
		}


	}
}
