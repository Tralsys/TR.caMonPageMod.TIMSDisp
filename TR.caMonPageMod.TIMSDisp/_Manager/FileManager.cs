using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

using TR.caMonPageMod.TIMSDisp._DataClasses;
using TR.caMonPageMod.TIMSDisp._Resources;

namespace TR.caMonPageMod.TIMSDisp._Manager
{
	public class FileManager
	{
		static public bool IsLoggingMode { get; set; } = false;

		static private string LogFilePath = ConstVars.CURRENT_EXECUTING_ASSEMBLY_FULLPATH + DateTime.Now.ToString(".yyyy-MM-dd.HH-mm-ss-ffff") + ".log";
		static public async void AddToLog(string str)
		{
			if (!IsLoggingMode)
				return;

			DateTime dt = DateTime.Now;

			//ref : https://www.tetsuyanbo.net/tetsuyanblog/27347
			MethodBase mb = new StackFrame(1).GetMethod();

			await File.AppendAllTextAsync(LogFilePath,
				new StringBuilder('[').Append(dt.ToString("")).Append(']')//時刻
				.Append(",\t").Append(mb.ReflectedType.FullName)//クラス名
				.Append(",\t").Append(mb.Name)//メソッド名
				.Append(",\t").Append(str)//ログ文字列
				.Append(Environment.NewLine).ToString());
		}


		static public ICData GetICDataFromFile(string Path)
		{
			return null;
		}
	}
}
