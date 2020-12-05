using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._Interfaces
{
	public interface IStationData
	{
		public double Location { get; set; }

		public Brush TextColor { get; set; }

		public bool IsOpStop { get; set; }

		public string StationName { get; set; }

		#region Runtime
		public bool IsRuntimeVisible { get; set; }

		public int RuntimeMM { get; set; }

		public int RuntimeSS { get; set; }
		#endregion Runtime

		public ITimeData ArrTime { get; set; }
		public ITimeData DepTime { get; set; }

		#region Track Number
		public string TrackNumber { get; set; }
		public bool IsTrackNumberVisible { get; set; }
		#endregion

		#region Run In/Out Limit
		public int RunInLimit { get; set; }
		public bool IsRunInLimitVisible { get; set; }
		public int RunOutLimit { get; set; }
		public bool IsRunOutLimitVisible { get; set; }
		#endregion

	}
}
