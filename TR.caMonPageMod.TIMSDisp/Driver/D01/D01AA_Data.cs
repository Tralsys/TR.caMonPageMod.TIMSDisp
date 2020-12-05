using System.ComponentModel;

using TR.caMonPageMod.TIMSDisp._BindClasses;

namespace TR.caMonPageMod.TIMSDisp.Driver.D01
{
	public class D01AA_Data : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		#region Train Number Area Settings
		private TrainNumberData __TrainNumber = null;
		public TrainNumberData TrainNumber
		{
			get => __TrainNumber;
			set
			{
				__TrainNumber = value;
				OnPropertyChanged(nameof(TrainNumber));
			}
		}
		private TrainNumberData __PTrainNumber = null;
		public TrainNumberData PTrainNumber
		{
			get => __PTrainNumber;
			set
			{
				__PTrainNumber = value;
				OnPropertyChanged(nameof(PTrainNumber));
			}
		}

		private bool __IsPTrainNumSetDone = false;
		public bool IsPTrainNumSetDone
		{
			get => __IsPTrainNumSetDone;
			set
			{
				if (IsPTrainNumSetDone == value)
					return;

				__IsPTrainNumSetDone = value;
				OnPropertyChanged(nameof(IsPTrainNumSetDone));
			}
		}

		private bool __IsPassSettingSetDone = false;
		public bool IsPassSettingSetDone
		{
			get => __IsPassSettingSetDone;
			set
			{
				if (IsPassSettingSetDone == value)
					return;

				__IsPassSettingSetDone = value;
				OnPropertyChanged(nameof(IsPassSettingSetDone));
			}
		}

		private string __RadioCH = "- ";
		public string RadioCH
		{
			get => __RadioCH;
			set
			{
				if (RadioCH == value)
					return;

				__RadioCH = value;
				OnPropertyChanged(nameof(RadioCH));
			}
		}

		#endregion

		#region Timetable Area
		private StationData __Row0 = null;
		public StationData Row0
		{
			get => __Row0;
			set
			{
				__Row0 = value;
				OnPropertyChanged(nameof(Row0));
			}
		}
		private StationData __Row1 = null;
		public StationData Row1
		{
			get => __Row1;
			set
			{
				__Row1 = value;
				OnPropertyChanged(nameof(Row1));
			}
		}
		private StationData __Row2 = null;
		public StationData Row2
		{
			get => __Row2;
			set
			{
				__Row2 = value;
				OnPropertyChanged(nameof(Row2));
			}
		}
		private StationData __Row3 = null;
		public StationData Row3
		{
			get => __Row3;
			set
			{
				__Row3 = value;
				OnPropertyChanged(nameof(Row3));
			}
		}
		private StationData __Row4 = null;
		public StationData Row4
		{
			get => __Row4;
			set
			{
				__Row4 = value;
				OnPropertyChanged(nameof(Row4));
			}
		}
		#endregion

		#region Next Stop
		private StationData __NextStop = null;
		public StationData NextStop
		{
			get => __NextStop;
			set
			{
				__NextStop = value;
				OnPropertyChanged(nameof(NextStop));
			}
		}

		private bool __IsNextStopComing = false;
		public bool IsNextStopComing
		{
			get => __IsNextStopComing;
			set
			{
				if (IsNextStopComing == value)
					return;

				__IsNextStopComing = value;
				OnPropertyChanged(nameof(IsNextStopComing));
			}
		}
		#endregion

		private JokoData __Joko1 = null;
		public JokoData Joko1
		{
			get => __Joko1;
			set
			{
				__Joko1 = value;
				OnPropertyChanged(nameof(Joko1));
			}
		}

		private JokoData __Joko2 = null;
		public JokoData Joko2
		{
			get => __Joko2;
			set
			{
				__Joko2 = value;
				OnPropertyChanged(nameof(Joko2));
			}
		}
	}
}
