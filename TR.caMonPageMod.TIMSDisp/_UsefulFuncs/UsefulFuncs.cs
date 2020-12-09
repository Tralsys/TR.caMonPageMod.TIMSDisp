using System;
using System.Text;
using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._UsefulFuncs
{
	public static partial class UsefulFuncs
	{
		static UsefulFuncs()
		{
			//ref : https://aquasoftware.net/blog/?p=895
			//エンコード932(Shift-JIS)に対応するため
			//Char_WideNarrowがらみ
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}

		static public byte[] GetPixels(Brush b, byte[] arr = null) => GetPixels(b as SolidColorBrush, arr);
		static public byte[] GetPixels(SolidColorBrush b, byte[] arr = null)
		{
			if (b == null)
				return null;

			arr ??= new byte[4];
			if (arr.Length < 4)
				arr = new byte[4];

			int i = 0;
			arr[i++] = b.Color.B;
			arr[i++] = b.Color.G;
			arr[i++] = b.Color.R;
			arr[i++] = b.Color.A;

			return arr;
		}

		static public bool StringToBool(in string s)
		{
			bool ret;
			if (bool.TryParse(s, out ret))//True / False
				return ret;
			else
				return s == "1";// 1:True, 0:False
		}
	}
}
