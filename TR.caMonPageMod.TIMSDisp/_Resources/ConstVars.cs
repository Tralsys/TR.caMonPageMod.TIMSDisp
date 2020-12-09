using System.IO;
using System.Reflection;
using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._Resources
{
	static public class ConstVars
	{
		static public readonly Brush STATION_DEFAULT_TEXTCOLOR = Brushes.White;

		static public readonly string DEFAULT_TIME_SEPARATOR = ":";

		static public readonly string CURRENT_EXECUTING_ASSEMBLY_FULLPATH = Assembly.GetExecutingAssembly().Location;
		static public readonly string CURRENT_EXECUTING_ASSEMBLY_DIRECTORY = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		static public readonly string[] CSV_COMMENT_HEADER = new string[] { "//", "#", ";" };

		static public int ToInt(in TRTimeTableIC100 a) => (int)a;
		static public int ToInt(in TRTimeTable100Station a) => (int)a;
		public enum TRTimeTableIC100
		{
			Identifier,
			OfficeName,
			WorkNumber,
			ReleaseYear,
			ReleaseMonth,
			ReleaseDay
		}
		public enum TRTimeTable100Station
		{
			StaName,
			Location,
			RunMM,
			RunSS,
			ArrHH,
			ArrMM,
			ArrSS,
			ArrSymbol,
			IsPass,
			DepHH,
			DepMM,
			DepSS,
			DepSymbol,
			TrackName,
			RuninLim,
			RunoutLim,
			StaWork,
			DispColor,

		}
	}
}
