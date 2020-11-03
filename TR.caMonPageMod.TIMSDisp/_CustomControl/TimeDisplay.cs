using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Microsoft.VisualBasic;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class TimeDisplay : Control
	{
		public static readonly DependencyProperty IsHankakuProperty = DependencyProperty.Register("IsHankaku", typeof(bool), typeof(TimeDisplay), new PropertyMetadata(false));
		
		public static readonly DependencyProperty HHProperty = DependencyProperty.Register("HH", typeof(string), typeof(TimeDisplay), new PropertyMetadata("１２", null, WideNarrowConv));
		public static readonly DependencyProperty MMProperty = DependencyProperty.Register("MM", typeof(string), typeof(TimeDisplay), new PropertyMetadata("３４", null, WideNarrowConv));
		public static readonly DependencyProperty SSProperty = DependencyProperty.Register("SS", typeof(string), typeof(TimeDisplay), new PropertyMetadata("５６", null, WideNarrowConv));
		public static readonly DependencyProperty SeparatorProperty = DependencyProperty.Register("Separator", typeof(string), typeof(TimeDisplay), new PropertyMetadata("：", null, WideNarrowConv));

		public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(TimeSpan), typeof(TimeDisplay), new PropertyMetadata(new TimeSpan(12, 34, 56), TimeChangedCallback));

		private static void TimeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var t = d as TimeDisplay;
			TimeSpan tn = (TimeSpan)e.NewValue;
			TimeSpan to = (TimeSpan)e.OldValue;

			if (tn.Hours != to.Hours)
				t.HH = tn.Hours.ToString();
			if (tn.Minutes != to.Minutes)
				t.MM = tn.Minutes.ToString("D2");
			if (tn.Seconds != to.Seconds)
				t.SS = tn.Seconds.ToString("D2");
		}

		public static readonly DependencyProperty VariableTextBrushProperty = DependencyProperty.Register("VariableTextBrush", typeof(Brush), typeof(TimeDisplay), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty ConstantTextBrushProperty = DependencyProperty.Register("ConstantTextBrush", typeof(Brush), typeof(TimeDisplay), new PropertyMetadata(Brushes.Lime));

		private static object WideNarrowConv(DependencyObject d, object baseValue)
			=> Strings.StrConv(baseValue as string, (d as TimeDisplay).IsHankaku ? VbStrConv.Narrow : VbStrConv.Wide, 0x411);


		static TimeDisplay() => DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeDisplay), new FrameworkPropertyMetadata(typeof(TimeDisplay)));
		

		public bool IsHankaku
		{
			get => (bool)GetValue(IsHankakuProperty);
			set => SetValue(IsHankakuProperty, value);
		}
		public string HH
		{
			get => (string)GetValue(HHProperty);
			set => SetValue(HHProperty, value);
		}
		public string MM
		{
			get => (string)GetValue(MMProperty);
			set => SetValue(MMProperty, value);
		}
		public string SS
		{
			get => (string)GetValue(SSProperty);
			set => SetValue(SSProperty, value);
		}
		public string Separator
		{
			get => (string)GetValue(SeparatorProperty);
			set => SetValue(SeparatorProperty, value);
		}
		public TimeSpan Time
		{
			get => (TimeSpan)GetValue(TimeProperty);
			set => SetValue(TimeProperty, value);
		}

		public Brush VariableTextBrush
		{
			get => (Brush)GetValue(VariableTextBrushProperty);
			set => SetValue(VariableTextBrushProperty, value);
		}
		public Brush ConstantTextBrush
		{
			get => (Brush)GetValue(ConstantTextBrushProperty);
			set => SetValue(ConstantTextBrushProperty, value);
		}

	}
}
