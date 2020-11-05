using System.Text;

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
	}
}
