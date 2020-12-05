using System.Windows.Controls;
using System.Windows.Media;

using TR.caMonPageMod.TIMSDisp._BindClasses;

namespace TR.caMonPageMod.TIMSDisp._Area
{
	/// <summary>
	/// MessageArea.xaml の相互作用ロジック
	/// </summary>
	public partial class MessageArea : Page
	{
		MessageAreaData mad = new MessageAreaData
		{
			Left = new MessageAreaData.MessageData
			{
				Message = "Test Message",
				IsBlinking = true,
				UsualBackground = Brushes.Yellow,
				UsualTextColor = Brushes.Black,
				IsFullWidth = true,
				RightEdgeMessage = "(Right Edge Msg)"
			},
			Right = new MessageAreaData.MessageData
			{
				Message = "Test Message",
				IsBlinking = true,
				UsualBackground = Brushes.Red,
				UsualTextColor = Brushes.White,
				IsFullWidth = true,
				RightEdgeMessage = "(Right Edge Msg)"
			}
		};
		public MessageArea()
		{
			InitializeComponent();

			DataContext = mad;
		}
	}
}
