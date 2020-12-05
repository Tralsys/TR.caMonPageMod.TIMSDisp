using System.ComponentModel;
using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._BindClasses
{
	public class MessageAreaData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		private MessageData __Left = null;
		public MessageData Left
		{
			get => __Left;
			set
			{
				__Left = value;
				OnPropertyChanged(nameof(Left));
			}
		}
		private MessageData __Right = null;
		public MessageData Right
		{
			get => __Right;
			set
			{
				__Right = value;
				OnPropertyChanged(nameof(Right));
			}
		}

		public class MessageData : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;
			private void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

			private bool __IsBlinking = false;
			public bool IsBlinking
			{
				get => __IsBlinking;
				set
				{
					if (IsBlinking == value)
						return;

					__IsBlinking = value;
					OnPropertyChanged(nameof(IsBlinking));
				}
			}
			private bool __IsFullWidth = false;
			public bool IsFullWidth
			{
				get => __IsFullWidth;
				set
				{
					if (IsFullWidth == value)
						return;

					__IsFullWidth = value;
					OnPropertyChanged(nameof(IsFullWidth));
				}
			}
			private string __Message = null;
			public string Message
			{
				get => __Message;
				set
				{
					if (Message == value)
						return;

					__Message = value;
					OnPropertyChanged(nameof(Message));
				}
			}
			private string __RightEdgeMessage = null;
			public string RightEdgeMessage
			{
				get => __RightEdgeMessage;
				set
				{
					if (RightEdgeMessage == value)
						return;

					__RightEdgeMessage = value;
					OnPropertyChanged(nameof(RightEdgeMessage));
				}
			}

			private Brush __UsualTextColor = Brushes.White;
			public Brush UsualTextColor
			{
				get => __UsualTextColor;
				set
				{
					__UsualTextColor = value;
					OnPropertyChanged(nameof(UsualTextColor));
				}
			}
			private Brush __UsualBackground = Brushes.Red;
			public Brush UsualBackground
			{
				get => __UsualBackground;
				set
				{
					__UsualBackground = value;
					OnPropertyChanged(nameof(UsualBackground));
				}
			}
		}
	}
}
