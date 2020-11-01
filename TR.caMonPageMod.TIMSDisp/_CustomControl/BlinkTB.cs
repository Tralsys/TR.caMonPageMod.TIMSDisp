using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class BlinkTB : Control
	{
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(BlinkTB), new PropertyMetadata("BlinkTB"));
		public static readonly DependencyProperty IsBlinkingProperty = DependencyProperty.Register("IsBlinking", typeof(bool), typeof(BlinkTB), new PropertyMetadata(false));
		public static readonly DependencyProperty UsualBackgroundProperty = DependencyProperty.Register("UsualBackground", typeof(Brush), typeof(BlinkTB), new PropertyMetadata(Brushes.Blue));
		public static readonly DependencyProperty FlippedBackgroundProperty = DependencyProperty.Register("FlippedBackground", typeof(Brush), typeof(BlinkTB), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty UsualTextColorProperty = DependencyProperty.Register("UsualTextColor", typeof(Brush), typeof(BlinkTB), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty FlippedTextColorProperty = DependencyProperty.Register("FlippedTextColor", typeof(Brush), typeof(BlinkTB), new PropertyMetadata(Brushes.Black));

		static BlinkTB()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(BlinkTB), new FrameworkPropertyMetadata(typeof(BlinkTB)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			TextBlock tb= GetTemplateChild("tb") as TextBlock;
			Border b = GetTemplateChild("b") as Border;

			FrontPage.DT400Tick += (s, e) =>
			{
				if (!IsEnabled || !IsBlinking)
					return;

				if (FrontPage.DT400_TFLoop)
				{
					b.Background = FlippedBackground;
					tb.Foreground = FlippedTextColor;
				}
				else
				{
					b.Background = UsualBackground;
					tb.Foreground = UsualTextColor;
				}
			};

		}


		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}
		public bool IsBlinking
		{
			get => (bool)GetValue(IsBlinkingProperty);
			set => SetValue(IsBlinkingProperty, value);
		}
		public Brush UsualBackground
		{
			get => (Brush)GetValue(UsualBackgroundProperty);
			set => SetValue(UsualBackgroundProperty, value);
		}
		public Brush FlippedBackground
		{
			get => (Brush)GetValue(FlippedBackgroundProperty);
			set => SetValue(FlippedBackgroundProperty, value);
		}
		public Brush UsualTextColor
		{
			get => (Brush)GetValue(UsualTextColorProperty);
			set => SetValue(UsualTextColorProperty, value);
		}
		public Brush FlippedTextColor
		{
			get => (Brush)GetValue(FlippedTextColorProperty);
			set => SetValue(FlippedTextColorProperty, value);
		}
	}
}
