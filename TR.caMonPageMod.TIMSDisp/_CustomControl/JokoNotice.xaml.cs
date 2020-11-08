using System.Windows;
using System.Windows.Controls;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class JokoNotice : Control
	{
		static public readonly DependencyProperty IsJokoEnabledProperty = DependencyProperty.Register(nameof(IsJokoEnabled), typeof(bool), typeof(JokoNotice), new PropertyMetadata(false));
		public bool IsJokoEnabled
		{
			get => (bool)GetValue(IsJokoEnabledProperty);
			set => SetValue(IsJokoEnabledProperty, value);
		}
		static public readonly DependencyProperty IsJoko2EnabledProperty = DependencyProperty.Register(nameof(IsJoko2Enabled), typeof(bool), typeof(JokoNotice), new PropertyMetadata(false));
		public bool IsJoko2Enabled
		{
			get => (bool)GetValue(IsJoko2EnabledProperty);
			set => SetValue(IsJoko2EnabledProperty, value);
		}
		static public readonly DependencyProperty Joko1StartDistanceProperty = DependencyProperty.Register(nameof(Joko1StartDistance), typeof(double), typeof(JokoNotice), new PropertyMetadata(0.0));
		public double Joko1StartDistance
		{
			get => (double)GetValue(Joko1StartDistanceProperty);
			set => SetValue(Joko1StartDistanceProperty, value);
		}
		static public readonly DependencyProperty Joko2StartDistanceProperty = DependencyProperty.Register(nameof(Joko2StartDistance), typeof(double), typeof(JokoNotice), new PropertyMetadata(0.0));
		public double Joko2StartDistance
		{
			get => (double)GetValue(Joko2StartDistanceProperty);
			set => SetValue(Joko2StartDistanceProperty, value);
		}
		static public readonly DependencyProperty Joko1EndDistanceProperty = DependencyProperty.Register(nameof(Joko1EndDistance), typeof(double), typeof(JokoNotice), new PropertyMetadata(0.0));
		public double Joko1EndDistance
		{
			get => (double)GetValue(Joko1EndDistanceProperty);
			set => SetValue(Joko1EndDistanceProperty, value);
		}
		static public readonly DependencyProperty Joko2EndDistanceProperty = DependencyProperty.Register(nameof(Joko2EndDistance), typeof(double), typeof(JokoNotice), new PropertyMetadata(0.0));
		public double Joko2EndDistance
		{
			get => (double)GetValue(Joko2EndDistanceProperty);
			set => SetValue(Joko2EndDistanceProperty, value);
		}
		static public readonly DependencyProperty Joko1LimitSpeedProperty = DependencyProperty.Register(nameof(Joko1LimitSpeed), typeof(int), typeof(JokoNotice), new PropertyMetadata(0));
		public int Joko1LimitSpeed
		{
			get => (int)GetValue(Joko1LimitSpeedProperty);
			set => SetValue(Joko1LimitSpeedProperty, value);
		}
		static public readonly DependencyProperty Joko2LimitSpeedProperty = DependencyProperty.Register(nameof(Joko2LimitSpeed), typeof(int), typeof(JokoNotice), new PropertyMetadata(0));
		public int Joko2LimitSpeed
		{
			get => (int)GetValue(Joko2LimitSpeedProperty);
			set => SetValue(Joko2LimitSpeedProperty, value);
		}



		static JokoNotice() => DefaultStyleKeyProperty.OverrideMetadata(typeof(JokoNotice), new FrameworkPropertyMetadata(typeof(JokoNotice)));
		
	}
}
