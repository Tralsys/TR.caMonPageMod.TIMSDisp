using System.Windows;
using System.Windows.Controls;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class TrainNumberDisplay : Control
	{
		public static readonly DependencyProperty TNPrefixProperty = DependencyProperty.Register("TNPrefix", typeof(string), typeof(TIMSButton), new PropertyMetadata("試験"));
		public static readonly DependencyProperty TrainNumberProperty = DependencyProperty.Register("TrainNumber", typeof(string), typeof(TIMSButton), new PropertyMetadata("9999"));
		public static readonly DependencyProperty TNSuffixProperty = DependencyProperty.Register("TNSuffix", typeof(string), typeof(TIMSButton), new PropertyMetadata("MH"));
		static TrainNumberDisplay() => DefaultStyleKeyProperty.OverrideMetadata(typeof(TrainNumberDisplay), new FrameworkPropertyMetadata(typeof(TrainNumberDisplay)));
		
		public string TNPrefix
		{
			get => (string)GetValue(TNPrefixProperty);
			set => SetValue(TrainNumberProperty, value);
		}
		public string TrainNumber
		{
			get => (string)GetValue(TrainNumberProperty);
			set => SetValue(TrainNumberProperty, value);
		}
		public string TNSuffix
		{
			get => (string)GetValue(TNSuffixProperty);
			set => SetValue(TrainNumberProperty, value);
		}
	}
}
