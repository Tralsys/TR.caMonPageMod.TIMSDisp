using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class PageIDName : Control
	{
		public static readonly DependencyProperty PageIDProperty = DependencyProperty.Register("PageID", typeof(string), typeof(PageIDName), new PropertyMetadata("X00XX"));
		public static readonly DependencyProperty PageNameProperty = DependencyProperty.Register("PageName", typeof(string), typeof(PageIDName), new PropertyMetadata("サンプル"));

		public static readonly DependencyProperty PageIDBrushProperty = DependencyProperty.Register("PageIDBrush", typeof(Brush), typeof(PageIDName), new PropertyMetadata(Brushes.Lime));
		public static readonly DependencyProperty PageNameBrushProperty = DependencyProperty.Register("PageNameBrush", typeof(Brush), typeof(PageIDName), new PropertyMetadata(Brushes.Lime));

		public static readonly DependencyProperty PageIDVisibilityProperty = DependencyProperty.Register("PageIDVisibility", typeof(Visibility), typeof(PageIDName), new PropertyMetadata(Visibility.Visible));
		public static readonly DependencyProperty PageNameVisibilityProperty = DependencyProperty.Register("PageNameVisibility", typeof(Visibility), typeof(PageIDName), new PropertyMetadata(Visibility.Visible));

		static PageIDName() => DefaultStyleKeyProperty.OverrideMetadata(typeof(PageIDName), new FrameworkPropertyMetadata(typeof(PageIDName)));
		

		public string PageID
		{
			get => (string)GetValue(PageIDProperty);
			set => SetValue(PageIDProperty, value);
		}
		public string PageName
		{
			get => (string)GetValue(PageNameProperty);
			set => SetValue(PageNameProperty, value);
		}

		public Brush PageIDBrush
		{
			get => (Brush)GetValue(PageIDBrushProperty);
			set => SetValue(PageIDBrushProperty, value);
		}
		public Brush PageNameBrush
		{
			get => (Brush)GetValue(PageNameBrushProperty);
			set => SetValue(PageNameBrushProperty, value);
		}
		public Visibility PageIDVisibility
		{
			get => (Visibility)GetValue(PageIDVisibilityProperty);
			set => SetValue(PageIDVisibilityProperty, value);
		}
		public Visibility PageNameVisibility
		{
			get => (Visibility)GetValue(PageNameVisibilityProperty);
			set => SetValue(PageNameVisibilityProperty, value);
		}

	}
}
