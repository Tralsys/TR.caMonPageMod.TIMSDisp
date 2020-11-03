using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;

using WMPLib;

namespace TR.caMonPageMod.TIMSDisp
{
	static internal class CommonMethods
	{
		//ref : https://qiita.com/fujieda/items/d8642eae891d096d4028
		static readonly WindowsMediaPlayer Btn_Pi;
		static readonly int[] VolumeSettings = new int[3] { 0, 30, 60 };


		static int CurrentVolumeSetting = VolumeSettings[2];
		static public void VolumeSettingUpdated(int num)
			=> CurrentVolumeSetting = VolumeSettings[num < 0 ? 0 : (num > 2 ? 2 : num)];

		static public readonly string CurrentDLLPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

		static CommonMethods()
		{
			//ref : http://dobon.net/vb/dotnet/programing/playmidifile.html#wmp
			//Btn_Pi = Activator.CreateInstance(Type.GetTypeFromProgID("WMPlayer.OCX.7"));
			Btn_Pi = new WindowsMediaPlayer();
			Btn_Pi.URL = Path.Combine(CurrentDLLPath, "_Resources", "Sounds", "Btn_Pi.wav");
			//Btn_Pi.URL = @"pack://application:,,,/TR.caMonPageMod.TIMSDisp;component/_Resources/Sounds/Btn_Pi.wav";
		}

		static public void ButtonPushed()
		{
			if (CurrentVolumeSetting <= 0)
				return;

			//ref : http://www.gan.st/gan/blog/index.php?itemid=1406
			if (Btn_Pi.playState == WMPPlayState.wmppsPlaying)
				Btn_Pi.controls.stop();

			Btn_Pi.settings.volume = CurrentVolumeSetting;
			//Btn_Pi.controls.currentPosition = 0;
			Btn_Pi.controls.play();
		}
		
	}
}
