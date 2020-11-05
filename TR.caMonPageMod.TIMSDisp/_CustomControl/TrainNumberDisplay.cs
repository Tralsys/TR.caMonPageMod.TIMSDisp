using System.Windows;
using System.Windows.Controls;

using TR.caMonPageMod.TIMSDisp._UsefulFuncs;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class TrainNumberDisplay : Control, IChar_WideNarrow
	{
		public static readonly DependencyProperty TNPrefixProperty = DependencyProperty.Register("TNPrefix", typeof(string), typeof(TrainNumberDisplay), new PropertyMetadata("試験", null, UsefulFuncs.WideNarrowConv));
		public static readonly DependencyProperty TrainNumberProperty = DependencyProperty.Register("TrainNumber", typeof(string), typeof(TrainNumberDisplay), new PropertyMetadata("９９９９", null, UsefulFuncs.WideNarrowConv));
		public static readonly DependencyProperty TNSuffixProperty = DependencyProperty.Register("TNSuffix", typeof(string), typeof(TrainNumberDisplay), new PropertyMetadata("ＭＨ", null, UsefulFuncs.WideNarrowConv));
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
		public bool IsHankaku { get; } = false;
	}
}
