using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._Resources
{
	static public class ConstVars
	{
		static public readonly Brush STATION_DEFAULT_TEXTCOLOR = Brushes.White;

		static public readonly string DEFAULT_TIME_SEPARATOR = ":";

		static public readonly string CURRENT_EXECUTING_ASSEMBLY_FULLPATH = Assembly.GetExecutingAssembly().Location;
		static public readonly string CURRENT_EXECUTING_ASSEMBLY_DIRECTORY = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
	}
}
