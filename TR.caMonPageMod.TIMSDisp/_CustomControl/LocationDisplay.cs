using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

using Microsoft.VisualBasic;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class LocationDisplay : Control
	{
		public static readonly DependencyProperty IsHankakuProperty = DependencyProperty.Register("IsHankaku", typeof(bool), typeof(LocationDisplay), new PropertyMetadata(false));

		public static readonly DependencyProperty IntegerPartProperty = DependencyProperty.Register("IntegerPart", typeof(string), typeof(LocationDisplay), new PropertyMetadata("１２３４５６", null, WideNarrowConv));
		public static readonly DependencyProperty DisabledSeparatorProperty = DependencyProperty.Register("DisabledSeparator", typeof(string), typeof(LocationDisplay), new PropertyMetadata("－", null, WideNarrowConv));
		public static readonly DependencyProperty SeparatorProperty = DependencyProperty.Register("Separator", typeof(string), typeof(LocationDisplay), new PropertyMetadata("．", null, WideNarrowConv));
		public static readonly DependencyProperty DecimalPartProperty = DependencyProperty.Register("DecimalPart", typeof(string), typeof(LocationDisplay), new PropertyMetadata("７", null, WideNarrowConv));

		public static readonly DependencyProperty LocationProperty = DependencyProperty.Register("Location", typeof(double), typeof(LocationDisplay), new PropertyMetadata(1.2, LocationChangedCallback));

		private static void LocationChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var t = d as LocationDisplay;
			int n1 = (int)e.NewValue;
			int o1 = (int)e.OldValue;
			int n0 = (int)(((double)e.NewValue - n1) * 10);
			int o0 = (int)(((double)e.OldValue - o1) * 10);

			if (n1 != o1)
				t.IntegerPart = n1.ToString();

			if (n0 != o0)
				t.DecimalPart = n0.ToString();
		}

		public static readonly DependencyProperty VariableTextBrushProperty = DependencyProperty.Register("VariableTextBrush", typeof(Brush), typeof(LocationDisplay), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty ConstantTextBrushProperty = DependencyProperty.Register("ConstantTextBrush", typeof(Brush), typeof(LocationDisplay), new PropertyMetadata(Brushes.Lime));

		private static object WideNarrowConv(DependencyObject d, object baseValue)
			=> Strings.StrConv(baseValue as string, (d as LocationDisplay).IsHankaku ? VbStrConv.Narrow : VbStrConv.Wide, 0x411);


		static LocationDisplay() => DefaultStyleKeyProperty.OverrideMetadata(typeof(LocationDisplay), new FrameworkPropertyMetadata(typeof(LocationDisplay)));

		TextBlock tbi, tbs, tbd;

		public LocationDisplay()
		{
			IsEnabledChanged += (s, e) => LocEnabledDisabledChanger((bool)e.NewValue);
		}
		private void LocEnabledDisabledChanger(in bool tf)
		{
			if (tbi == null || tbs == null || tbd == null)
				return;

			if (tf)
			{
				//Enabled
				tbi.Visibility = tbd.Visibility = Visibility.Visible;
				tbs.SetBinding(TextBlock.TextProperty, new Binding(nameof(Separator)) { Source = this });
			}
			else
			{
				//Disabled
				tbi.Visibility = tbd.Visibility = Visibility.Hidden;
				tbs.SetBinding(TextBlock.TextProperty, new Binding(nameof(DisabledSeparator)) { Source = this });
			}
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			tbi = GetTemplateChild("tbi") as TextBlock;
			tbs = GetTemplateChild("tbs") as TextBlock;
			tbd = GetTemplateChild("tbd") as TextBlock;

			LocEnabledDisabledChanger(IsEnabled);
		}

		public bool IsHankaku
		{
			get => (bool)GetValue(IsHankakuProperty);
			set => SetValue(IsHankakuProperty, value);
		}
		public string DecimalPart
		{
			get => (string)GetValue(DecimalPartProperty);
			set => SetValue(DecimalPartProperty, value);
		}
		public string IntegerPart
		{
			get => (string)GetValue(IntegerPartProperty);
			set => SetValue(IntegerPartProperty, value);
		}
		public string Separator
		{
			get => (string)GetValue(SeparatorProperty);
			set => SetValue(SeparatorProperty, value);
		}
		public string DisabledSeparator
		{
			get => (string)GetValue(DisabledSeparatorProperty);
			set => SetValue(DisabledSeparatorProperty, value);
		}

		public double Location
		{
			get => (double)GetValue(LocationProperty);
			set => SetValue(LocationProperty, value);
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
