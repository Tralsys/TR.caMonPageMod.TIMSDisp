using System.Collections.Generic;

namespace TR.caMonPageMod.TIMSDisp._Interfaces
{
	public interface I_ICData
	{
		/// <summary>ICファイルのフォーマットのバージョン</summary>
		public string FormatVersion { get; set; }

		/// <summary>ICを識別するためのID  要らないとは思うけど, 一応</summary>
		public string UniqueID { get; set; }

		/// <summary>区所名</summary>
		public string OfficeName { get; set; }

		/// <summary>行路名</summary>
		public string WorkName { get; set; }

		/// <summary>改正日(?)</summary>
		public string ReleaseDate { get; set; }


		public List<ITrainData> TrainData { get; set; }
	}
}
