using System;
using System.Windows.Controls;

using caMon;

namespace TR.caMonPageMod.TIMSDisp
{
	public class caMonIF : IPages
	{
		public Page FrontPage { get; } = new FrontPage();

		public event EventHandler BackToHome;
		public event EventHandler CloseApp;

		public void Dispose()
		{
			
		}
	}
}
