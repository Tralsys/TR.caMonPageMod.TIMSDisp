using System.ComponentModel;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	public class UpperArea_NoticeData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		private bool __IsKaihoBlinking = false;
		public bool IsKaihoBlinking
		{
			get => __IsKaihoBlinking;
			set
			{
				if (IsKaihoBlinking == value)
					return;

				__IsKaihoBlinking = value;
				OnPropertyChanged(nameof(IsKaihoBlinking));
			}
		}

		private bool __IsChushaBlinking = false;
		public bool IsChushaBlinking
		{
			get => __IsChushaBlinking;
			set
			{
				if (IsChushaBlinking == value)
					return;

				__IsChushaBlinking = value;
				OnPropertyChanged(nameof(IsChushaBlinking));
			}
		}

		private bool __IsIdoKinshiBlinking = false;
		public bool IsIdoKinshiBlinking
		{
			get => __IsIdoKinshiBlinking;
			set
			{
				if (IsIdoKinshiBlinking == value)
					return;

				__IsIdoKinshiBlinking = value;
				OnPropertyChanged(nameof(IsIdoKinshiBlinking));
			}
		}

		private bool __IsASBlinking = false;
		public bool IsASBlinking
		{
			get => __IsASBlinking;
			set
			{
				if (IsASBlinking == value)
					return;

				__IsASBlinking = value;
				OnPropertyChanged(nameof(IsASBlinking));
			}
		}

		private bool __IsKoChokuBlinking = false;
		public bool IsKoChokuBlinking
		{
			get => __IsKoChokuBlinking;
			set
			{
				if (IsKoChokuBlinking == value)
					return;

				__IsKoChokuBlinking = value;
				OnPropertyChanged(nameof(IsKoChokuBlinking));
			}
		}

		private bool __IsKoKoBlinking = false;
		public bool IsKoKoBlinking
		{
			get => __IsKoKoBlinking;
			set
			{
				if (IsKoKoBlinking == value)
					return;

				__IsKoKoBlinking = value;
				OnPropertyChanged(nameof(IsKoKoBlinking));
			}
		}

	}
}
