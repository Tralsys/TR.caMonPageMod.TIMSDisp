using System.Collections.Generic;

namespace TR.caMonPageMod.TIMSDisp._Interfaces
{
	public interface ITrainData
	{
		public string TrainNum_Prefix { get; set; }
		public string PTrainNum_Prefix { get; set; }
		public int TrainNumber_Number { get; set; }
		public string TrainNumber_Suffix { get; set; }

		/// <summary>この電車の終着駅</summary>
		public string LastStop { get; set; }

		public string InitRadioCH { get; set; }
		public bool InitPassDefaultSetting { get; set; }


		/// <summary>各駅の情報</summary>
		public List<IStationData> Stations { get; set; }

		#region Options
		public List<IJokoData> JokoData { get; set; }
		public List<IStartEndDistance> AirSection { get; set; }
		public List<IStartEndDistance> ACDCSection { get; set; }
		public List<IStartEndDistance> ACACSection { get; set; }
		#endregion Options
	}

	public class PureTrainData : ITrainData
	{
		public string TrainNum_Prefix { get; set; }
		public string PTrainNum_Prefix { get; set; }
		public int TrainNumber_Number { get; set; }
		public string TrainNumber_Suffix { get; set; }
		public string LastStop { get; set; }
		public string InitRadioCH { get; set; }
		public bool InitPassDefaultSetting { get; set; }
		public List<IStationData> Stations { get; set; }
		public List<IJokoData> JokoData { get; set; }
		public List<IStartEndDistance> AirSection { get; set; }
		public List<IStartEndDistance> ACDCSection { get; set; }
		public List<IStartEndDistance> ACACSection { get; set; }
	}
}
