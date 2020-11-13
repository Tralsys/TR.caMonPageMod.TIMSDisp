using System.Windows.Controls;

using TR.caMonPageMod.TIMSDisp._BindClasses;

namespace TR.caMonPageMod.TIMSDisp.Driver.D01
{
	/// <summary>
	/// A_5Row.xaml の相互作用ロジック
	/// </summary>
	public partial class A_5Row : Page
	{
		D01AA_Data d2show = new D01AA_Data();
		public A_5Row()
		{
			InitializeComponent();

			DataContext = d2show;
		}
	}
}
