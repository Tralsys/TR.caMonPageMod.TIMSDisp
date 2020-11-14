using System.ComponentModel;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	class UpperArea_toBind : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		private UpperArea_RadioData __RadioData;
		public UpperArea_RadioData RadioData
		{
			get => __RadioData;
			set
			{
				__RadioData = value;
				OnPropertyChanged(nameof(RadioData));
			}
		}

		private UpperArea_TSLData __TSLData;
		public UpperArea_TSLData TSLData
		{
			get => __TSLData;
			set
			{
				__TSLData = value;
				OnPropertyChanged(nameof(TSLData));
			}
		}

		private UpperArea_NoticeData __NoticeData;
		public UpperArea_NoticeData NoticeData
		{
			get => __NoticeData;
			set
			{
				__NoticeData = value;
				OnPropertyChanged(nameof(NoticeData));
			}
		}

		private bool __IsKoshoHasseiVisible = false;
		public bool IsKoshoHasseiVisible
		{
			get => __IsKoshoHasseiVisible;
			set
			{
				if (IsKoshoHasseiVisible == value)
					return;

				__IsKoshoHasseiVisible = value;
				OnPropertyChanged(nameof(IsKoshoHasseiVisible));
			}
		}

		private bool __IsKoshoHasseiBlinking = false;
		public bool IsKoshoHasseiBlinking
		{
			get => __IsKoshoHasseiBlinking;
			set
			{
				if (IsKoshoHasseiBlinking == value)
					return;

				__IsKoshoHasseiBlinking = value;
				OnPropertyChanged(nameof(IsKoshoHasseiBlinking));
			}
		}

		private bool __IsKikiJohoVisible = false;
		public bool IsKikiJohoVisible
		{
			get => __IsKikiJohoVisible;
			set
			{
				if (IsKikiJohoVisible == value)
					return;

				__IsKikiJohoVisible = value;
				OnPropertyChanged(nameof(IsKikiJohoVisible));
			}
		}

		private bool __IsKikiJohoBlinking = false;
		public bool IsKikiJohoBlinking
		{
			get => __IsKikiJohoBlinking;
			set
			{
				if (IsKikiJohoBlinking == value)
					return;

				__IsKikiJohoBlinking = value;
				OnPropertyChanged(nameof(IsKikiJohoBlinking));
			}
		}

		private bool __IsHijoTsuhoVisible = false;
		public bool IsHijoTsuhoVisible
		{
			get => __IsHijoTsuhoVisible;
			set
			{
				if (IsHijoTsuhoVisible == value)
					return;

				__IsHijoTsuhoVisible = value;
				OnPropertyChanged(nameof(IsHijoTsuhoVisible));
			}
		}

		private bool __IsHijoTsuhoBlinking = false;
		public bool IsHijoTsuhoBlinking
		{
			get => __IsHijoTsuhoBlinking;
			set
			{
				if (IsHijoTsuhoBlinking == value)
					return;

				__IsHijoTsuhoBlinking = value;
				OnPropertyChanged(nameof(IsHijoTsuhoBlinking));
			}
		}


		private bool __IsShokiSentakuVisible = false;
		public bool IsShokiSentakuVisible
		{
			get => __IsShokiSentakuVisible;
			set
			{
				if (IsShokiSentakuVisible == value)
					return;

				__IsShokiSentakuVisible = value;
				OnPropertyChanged(nameof(IsShokiSentakuVisible));
			}
		}
	}
}
