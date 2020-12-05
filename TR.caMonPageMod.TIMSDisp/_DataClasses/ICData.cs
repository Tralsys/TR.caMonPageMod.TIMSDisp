using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

using TR.caMonPageMod.TIMSDisp._Interfaces;

namespace TR.caMonPageMod.TIMSDisp._DataClasses
{
	public class ICData : I_ICData
	{
		#region Load Methods
		//ref : https://qiita.com/hkiribayashi/items/79f8336d9098218d87e3
		static public ICData FromJson(string s) => JsonSerializer.Deserialize<ICData>(s);
		/// <summary>UTF8なXMLをICDataに変換する</summary>
		/// <param name="s">UTF8な文字列</param>
		/// <returns></returns>
		static public ICData FromXML(string s)
			//ref : https://garafu.blogspot.com/2016/09/cs-string-memorystream.html
			=> (new XmlSerializer(typeof(ICData))).Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(s))) as ICData;
		#endregion Convert Methods

		#region Save Methods
		//ref : https://qiita.com/hkiribayashi/items/79f8336d9098218d87e3
		static public string ToJson(ICData icd) => JsonSerializer.Serialize(icd);

		static public string ToXML(ICData icd)
		{
			MemoryStream ms = new MemoryStream();
			(new XmlSerializer(typeof(ICData))).Serialize(ms, icd);
			return Encoding.UTF8.GetString(ms.ToArray());
		}
		#endregion Save Methods

		#region Properties
		public string FormatVersion { get; set; }
		public string UniqueID { get; set; }
		public string OfficeName { get; set; }
		public string WorkName { get; set; }
		public string ReleaseDate { get; set; }
		public List<ITrainData> TrainData { get; set; }
		#endregion Properties
	}
}
