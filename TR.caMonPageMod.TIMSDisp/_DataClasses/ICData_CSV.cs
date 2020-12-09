using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using TR.caMonPageMod.TIMSDisp._Interfaces;
using TR.caMonPageMod.TIMSDisp._Resources;
using TR.caMonPageMod.TIMSDisp._UsefulFuncs;

namespace TR.caMonPageMod.TIMSDisp._DataClasses
{
	static class ICData_CSV
	{
		static public ICData FromCSV(in string data) => FromCSV(data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList());

		static int GetIntValue(in string s, out bool ConvertSuccess)
		{
			int tmp_i;
			ConvertSuccess = false;

			if (!string.IsNullOrWhiteSpace(s))
			{
				if (int.TryParse(s, out tmp_i))
				{
					ConvertSuccess = true;
					return tmp_i;
				}
				else
					throw new Exception("不正な入力  整数文字列のみ許容");
			}
			else
				return 0;
		}
		static int GetIntValue(in List<string> sl, in ConstVars.TRTimeTable100Station pos, out bool ConvertSuccess) => GetIntValue(sl[ConstVars.ToInt(pos)], out ConvertSuccess);
		static public ICData FromCSV(in List<string> data)
		{
			ICData ret = new ICData();
			PureTrainData ptd = new PureTrainData();
			bool IsTimeTableEditing = false;
			bool IsOptDataEditing = false;

			foreach(var s in data)
			{
				if (ConstVars.CSV_COMMENT_HEADER?.Length > 0)
					foreach (var c in ConstVars.CSV_COMMENT_HEADER)
						if (s.StartsWith(c))//コメントアウト設定であるかを確認
							return null;

				List<string> sl = GetColDataFromRowData(s);

				if (IsTimeTableEditing)
				{
					if (IsOptDataEditing)
					{
						switch (sl[0])
						{
							case "TRTimeTableOPEOF"://オプション設定部終了
								IsOptDataEditing = false;
								break;
							case "SlowPoint":
								var jd = new PureJokoData();
								SetStartEndDistance(sl, jd, 2);
								int tmp = 0;
								if (int.TryParse(sl[1], out tmp))
									jd.LimitSpeed = tmp;
								else
									throw new Exception("徐行設定 制限速度設定が不正");//あとで適切な伝達方法に変更する

								ptd.JokoData.Add(jd);
								break;
							case "AirSec":
							case "ACDCChange":
							case "ACACSec":
								(sl[0] switch
								{
									"AirSec" => ptd.AirSection,
									"ACDCChange" => ptd.ACDCSection,
									"ACACSec" => ptd.ACACSection,
									_ => null
								})?.Add(SetStartEndDistance(sl));
								break;
							case "RadioCH"://未対応
								break;
							case "LineColorChange"://未対応
								break;
						}
					}
					else
					{
						switch (sl[0])
						{
							case "TRTimeTableOP100":
								IsOptDataEditing = true;
								break;
							case "TRTimeTableEOF":
								IsTimeTableEditing = false;
								ptd = null;//誤って編集しないように
								break;

							default://各駅設定
								double tmp_d;
								bool tf;


								PureStationData psd = new PureStationData();
								

								psd.StationName = sl[ConstVars.ToInt(ConstVars.TRTimeTable100Station.StaName)];

								if (double.TryParse(sl[ConstVars.ToInt(ConstVars.TRTimeTable100Station.Location)], out tmp_d))
									psd.Location = tmp_d;
								else//空白は許容されない
									throw new Exception("各駅設定が不正です.");//あとで適切な伝達方法に変更する

								psd.RuntimeMM = GetIntValue(sl,ConstVars.TRTimeTable100Station.RunMM, out tf);
								psd.RuntimeSS = GetIntValue(sl,ConstVars.TRTimeTable100Station.RunSS, out tf);
								psd.IsRuntimeVisible = psd.RuntimeMM != 0 || psd.RuntimeSS != 0;

								psd.ArrTime = new PureTimeData();
								psd.ArrTime.HH = GetIntValue(sl, ConstVars.TRTimeTable100Station.ArrHH, out tf);
								psd.ArrTime.IsHHVisible = tf;
								psd.ArrTime.MM = GetIntValue(sl, ConstVars.TRTimeTable100Station.ArrMM, out tf);
								psd.ArrTime.IsMMVisible = tf;
								psd.ArrTime.SS = GetIntValue(sl, ConstVars.TRTimeTable100Station.ArrSS, out tf);
								psd.ArrTime.IsSSVisible = tf && psd.ArrTime.SS != 0;//0は表示しない
								psd.ArrTime.Separator = sl[ConstVars.ToInt(ConstVars.TRTimeTable100Station.ArrSymbol)];
								psd.ArrTime.IsSeparatorVisible = !string.IsNullOrWhiteSpace(psd.ArrTime.Separator);

								psd.ArrTime.IsVisible = psd.ArrTime.IsHHVisible || psd.ArrTime.IsMMVisible || psd.ArrTime.IsSeparatorVisible || psd.ArrTime.IsSSVisible;


								psd.IsPass = UsefulFuncs.StringToBool(sl[ConstVars.ToInt(ConstVars.TRTimeTable100Station.IsPass)]);
								//運転停車は非対応

								psd.DepTime = new PureTimeData();
								psd.DepTime.HH = GetIntValue(sl, ConstVars.TRTimeTable100Station.DepHH, out tf);
								psd.DepTime.IsHHVisible = tf;
								psd.DepTime.MM = GetIntValue(sl, ConstVars.TRTimeTable100Station.DepMM, out tf);
								psd.DepTime.IsMMVisible = tf;
								psd.DepTime.SS = GetIntValue(sl, ConstVars.TRTimeTable100Station.DepSS, out tf);
								psd.DepTime.IsSSVisible = tf && psd.DepTime.SS != 0;//0は表示しない
								psd.DepTime.Separator = sl[ConstVars.ToInt(ConstVars.TRTimeTable100Station.DepSymbol)];
								psd.DepTime.IsSeparatorVisible = !string.IsNullOrWhiteSpace(psd.DepTime.Separator);

								psd.DepTime.IsVisible = psd.DepTime.IsHHVisible || psd.DepTime.IsMMVisible || psd.DepTime.IsSeparatorVisible || psd.DepTime.IsSSVisible;

								psd.TrackNumber = sl[ConstVars.ToInt(ConstVars.TRTimeTable100Station.TrackName)];
								psd.IsTrackNumberVisible = !string.IsNullOrWhiteSpace(psd.TrackNumber);

								psd.RunInLimit = GetIntValue(sl, ConstVars.TRTimeTable100Station.RuninLim, out tf);
								psd.IsRunInLimitVisible = tf;
								psd.RunOutLimit = GetIntValue(sl, ConstVars.TRTimeTable100Station.RunoutLim, out tf);
								psd.IsRunOutLimitVisible = tf;

								char[] colors = sl[ConstVars.ToInt(ConstVars.TRTimeTable100Station.DispColor)].ToCharArray();
								psd.TextColor = new SolidColorBrush(Color.FromRgb(colors[0] == '1' ? 0xFF : 0, colors[1] == '1' ? 0xFF : 0, colors[2] == '1' ? 0xFF : 0));

								//駅仕業は未対応

								ptd.Stations.Add(psd);
								break;
						}
					}
				}
				else
				{
					switch (sl[0])
					{
						case "TRTimeTable100":
							ptd = SetTrainData_Header(sl);
							
							if (ptd is null)
								throw new Exception("時刻表テーブルフォーマット不正");//あとで適切な伝達方法に変更する

							ret.TrainData.Add(ptd);
							IsTimeTableEditing = true;//時刻法入力モードに移行
							break;

						case "TRTimeTableIC100":
							if (sl.Count < 6)
								throw new Exception("IC設定不正 データ数不足");//あとで適切な伝達方法に変更する

							ret.OfficeName = sl[1];
							ret.WorkName = sl[2];
							ret.ReleaseYear = sl[3];
							ret.ReleaseMonth = sl[4];
							ret.ReleaseDay = sl[5];
							break;
					}
				}
			}

			if (IsTimeTableEditing || IsOptDataEditing)
			{
				throw new Exception("設定未完了");//あとで適切な伝達方法に変更する
			}

			return ret;
		}

		static private List<string> GetColDataFromRowData(in string s) => s?.Split(',', StringSplitOptions.None).ToList();

		static private PureTrainData SetTrainData_Header(in List<string> cols)
		{
			if (cols.Count < 8)
				return null;

			PureTrainData ptd = new PureTrainData();
			string s;
			int i = 0;
			

			#region 列番
			foreach (var c in cols[1].ToCharArray())
			{
				if (!char.IsLetterOrDigit(c))//制御文字は使用しない
					continue;

				//i は初期状態で 0 <=> Prefix部分
				if (i <= 1 && char.IsDigit(c))//(まだ数値部に入っていない or 現在数値部) かつ 文字が数字
				{
					ptd.TrainNumber_Number = ptd.TrainNumber_Number * 10 + (int)char.GetNumericValue(c);//全角半角を気にせず使うために
					i = 1;//現在数値部分
				}
				else if (i == 0)//現在Prefix部分
					ptd.TrainNum_Prefix += c;//Prefixに文字を追加
				else
				{
					ptd.TrainNumber_Suffix += c;//Suffixに文字追加
					i = 2;//現在Suffix部分
				}
			}
			#endregion

			#region P列番(Prefix部のみ)
			foreach(var c in cols[2].ToCharArray())
			{
				if (char.IsDigit(c))
					break;//数値が来たら探索終了

				if (char.IsLetter(c))
					ptd.PTrainNum_Prefix += c;//P_Prefixに文字を追加
			}
			#endregion

			ptd.InitPassDefaultSetting = UsefulFuncs.StringToBool(cols[3]);//通過設定の初期値

			//方向設定は削除

			ptd.InitRadioCH = cols[5] == string.Empty ? "-" : cols[5];//無線Chの初期値

			//線色設定は削除

			ptd.LastStop = cols[7];


			return ptd;
		}

		static private IStartEndDistance SetStartEndDistance(in List<string> cols, IStartEndDistance ret = null, in int index = 1)
		{
			ret ??= new PureStartEndDistance();
			double d;

			if (double.TryParse(cols[index], out d))
				ret.StartDistance = d;
			else
				throw new Exception("始点設定が不正");//あとで適切な伝達方法に変更する

			if (double.TryParse(cols[index + 1], out d))
				ret.StartDistance = d;
			else
				throw new Exception("始点設定が不正");//あとで適切な伝達方法に変更する

			return ret;
		}

		static public string ToCSV(in ICData data)
		{
			throw new NotImplementedException();
		}
	}
}
