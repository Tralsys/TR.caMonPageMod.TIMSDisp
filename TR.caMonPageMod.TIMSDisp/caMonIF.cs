using System;
using System.Text;
using System.Windows.Controls;

using caMon;

namespace TR.caMonPageMod.TIMSDisp
{
	public class caMonIF : IPages
	{
		static caMonIF()
		{
			//ref : https://aquasoftware.net/blog/?p=895
			//エンコード932(Shift-JIS)に対応するため
			//Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);// => _UsefulFuncs/UsefulFuncs.csに移動
		}

		public Page FrontPage { get; } = new FrontPage();

		public event EventHandler BackToHome;
		public event EventHandler CloseApp;

		public void Dispose()
		{
			
		}
	}
}
