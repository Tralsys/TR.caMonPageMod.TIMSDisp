using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;

using TR.caMonPageMod.TIMSDisp._Resources;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	public interface IStationName
	{
		public string StationName { get; set; }
	}
	public class TimeData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		private bool __IsVisible;
		public bool IsVisible
		{
			get => __IsVisible;
			set
			{
				if (IsVisible == value)
					return;

				__IsVisible = value;
				OnPropertyChanged(nameof(IsVisible));
			}
		}

		private int __HH;
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

		private int __MM;
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

		private int __SS;
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

		private bool __IsHHVisible;
		public bool IsHHVisible
		{
			get => __IsHHVisible;
			set
			{
				if (IsHHVisible == value)
					return;

				__IsHHVisible = value;
				OnPropertyChanged(nameof(IsHHVisible));
			}
		}

		private bool __IsMMVisible;
		public bool IsMMVisible
		{
			get => __IsMMVisible;
			set
			{
				if (IsMMVisible == value)
					return;

				__IsMMVisible = value;
				OnPropertyChanged(nameof(IsMMVisible));
			}
		}

		private bool __IsSSVisible;
		public bool IsSSVisible
		{
			get => __IsSSVisible;
			set
			{
				if (IsSSVisible == value)
					return;

				__IsSSVisible = value;
				OnPropertyChanged(nameof(IsSSVisible));
			}
		}

		private string __Separator = ConstVars.DEFAULT_TIME_SEPARATOR;
		public string Separator
		{
			get => __Separator;
			set
			{
				if (Separator == value)
					return;

				__Separator = value;
				OnPropertyChanged(nameof(Separator));
			}
		}

		private bool __IsSeparatorVisible;
		public bool IsSeparatorVisible
		{
			get => __IsSeparatorVisible;
			set
			{
				if (IsSeparatorVisible == value)
					return;

				__IsSeparatorVisible = value;
				OnPropertyChanged(nameof(IsSeparatorVisible));
			}
		}




	}

	public class StationData : INotifyPropertyChanged, IStationName
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		private Brush __TextColor = ConstVars.STATION_DEFAULT_TEXTCOLOR;
		public Brush TextColor
		{
			get => __TextColor;
			set
			{
				if (TextColor == value)
					return;

				__TextColor = value;
				OnPropertyChanged(nameof(TextColor));
			}
		}

		private bool __IsOpStop;
		public bool IsOpStop
		{
			get => __IsOpStop;
			set
			{
				if (IsOpStop == value)
					return;

				__IsOpStop = value;
				OnPropertyChanged(nameof(IsOpStop));
			}
		}


		#region Runtime
		private bool __IsRuntimeVisible = false;
		public bool IsRuntimeVisible
		{
			get => __IsRuntimeVisible;
			set
			{
				if (IsRuntimeVisible == value)
					return;

				__IsRuntimeVisible = value;
				OnPropertyChanged(nameof(IsRuntimeVisible));
			}
		}

		private int __RuntimeMM = 0;
		public int RuntimeMM
		{
			get => __RuntimeMM;
			set
			{
				if (RuntimeMM == value)
					return;

				__RuntimeMM = value;
				OnPropertyChanged(nameof(RuntimeMM));
			}
		}

		private int __RuntimeSS = 0;
		public int RuntimeSS
		{
			get => __RuntimeSS;
			set
			{
				if (RuntimeSS == value)
					return;

				__RuntimeSS = value;
				OnPropertyChanged(nameof(RuntimeSS));
			}
		}
		#endregion Runtime

		#region Station Name
		private string __StationName = string.Empty;
		public string StationName
		{
			get => __StationName;
			set
			{
				if (StationName == value)
					return;

				__StationName = value;
				OnPropertyChanged(nameof(StationName));
			}
		}
		#endregion

		#region ArrTime
		private TimeData __ArrTime = new TimeData();
		public TimeData ArrTime
		{
			get => __ArrTime;
			set
			{
				__ArrTime = value;
				OnPropertyChanged(nameof(ArrTime));
			}
		}
		#endregion
		#region DepTime
		private TimeData __DepTime = new TimeData();
		public TimeData DepTime
		{
			get => __DepTime;
			set
			{
				__DepTime = value;
				OnPropertyChanged(nameof(DepTime));
			}
		}
		#endregion

		#region Track Number
		private string __TrackNumber = string.Empty;
		public string TrackNumber
		{
			get => __TrackNumber;
			set
			{
				if (TrackNumber == value)
					return;

				__TrackNumber = value;
				OnPropertyChanged(nameof(TrackNumber));
			}
		}
		private bool __IsTrackNumberVisible;
		public bool IsTrackNumberVisible
		{
			get => __IsTrackNumberVisible;
			set
			{
				if (IsTrackNumberVisible == value)
					return;

				__IsTrackNumberVisible = value;
				OnPropertyChanged(nameof(IsTrackNumberVisible));
			}
		}
		#endregion

		#region Run In/Out Limit
		private int __RunInLimit;
		public int RunInLimit
		{
			get => __RunInLimit;
			set
			{
				if (RunInLimit == value)
					return;

				__RunInLimit = value;
				OnPropertyChanged(nameof(RunInLimit));
			}
		}
		private bool __IsRunInLimitVisible;
		public bool IsRunInLimitVisible
		{
			get => __IsRunInLimitVisible;
			set
			{
				if (IsRunInLimitVisible == value)
					return;

				__IsRunInLimitVisible = value;
				OnPropertyChanged(nameof(IsRunInLimitVisible));
			}
		}
		private int __RunOutLimit;
		public int RunOutLimit
		{
			get => __RunOutLimit;
			set
			{
				if (RunOutLimit == value)
					return;

				__RunOutLimit = value;
				OnPropertyChanged(nameof(RunOutLimit));
			}
		}
		private bool __IsRunOutLimitVisible;
		public bool IsRunOutLimitVisible
		{
			get => __IsRunOutLimitVisible;
			set
			{
				if (IsRunOutLimitVisible == value)
					return;

				__IsRunOutLimitVisible = value;
				OnPropertyChanged(nameof(IsRunOutLimitVisible));
			}
		}
		#endregion

	}
}
