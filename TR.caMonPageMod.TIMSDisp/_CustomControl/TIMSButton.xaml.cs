using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	//ref : https://blog.okazuki.jp/entry/2014/09/08/221209
	public class TIMSButton : Control
	{
		#region DependencyProperties
		public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(TIMSButton), new PropertyMetadata("TIMSBtn"));

		public static readonly DependencyProperty UsualBackgroundProperty = DependencyProperty.Register("UsualBackground", typeof(Brush), typeof(TIMSButton), new PropertyMetadata(Brushes.Blue));
		public static readonly DependencyProperty PushedBackgroundProperty = DependencyProperty.Register("PushedBackground", typeof(Brush), typeof(TIMSButton), new PropertyMetadata(Brushes.Yellow));
		public static readonly DependencyProperty FlippedBackgroundProperty = DependencyProperty.Register("FlippedBackground", typeof(Brush), typeof(TIMSButton), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty UsualTextColorProperty = DependencyProperty.Register("UsualTextColor", typeof(Brush), typeof(TIMSButton), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty PushedTextColorProperty = DependencyProperty.Register("PushedTextColor", typeof(Brush), typeof(TIMSButton), new PropertyMetadata(Brushes.Black));
		public static readonly DependencyProperty FlippedTextColorProperty = DependencyProperty.Register("FlippedTextColor", typeof(Brush), typeof(TIMSButton), new PropertyMetadata(Brushes.Black));

		public static readonly DependencyProperty IsBlinkingProperty = DependencyProperty.Register("IsBlinking", typeof(bool), typeof(TIMSButton), new PropertyMetadata(false));
		#endregion

		static TIMSButton() => DefaultStyleKeyProperty.OverrideMetadata(typeof(TIMSButton), new FrameworkPropertyMetadata(typeof(TIMSButton)));
		

		public TIMSButton()
		{
			FrontPage.DT400Tick += (s,e)=>
			{
				if (!IsEnabled || IsPushed || !IsBlinking)
					return;

				if (InnerBorder == null || TB == null)
					return;

				if (FrontPage.DT400_TFLoop)
				{
					InnerBorder.SetBinding(BackgroundProperty, new Binding(nameof(FlippedBackground)) { Source = this });
					TB.SetBinding(ForegroundProperty, new Binding(nameof(FlippedTextColor)) { Source = this });
				}
				else
				{
					InnerBorder.SetBinding(BackgroundProperty, new Binding(nameof(UsualBackground)) { Source = this });
					TB.SetBinding(ForegroundProperty, new Binding(nameof(UsualTextColor)) { Source = this });
				}
			};
		}

		#region Constant Setting Values
		const string BaseGrid_Name = "BaseGrid";
		const string BaseBorder_Name = "BaseBorder";
		const string BtnLight1_Name = "BtnLight1RotateTransform";
		const string BtnLight2_Name = "BtnLight2RotateTransform";
		const string LightBorder_Name = "LightBorder";
		const string InnerBorder_Name = "InnerBorder";
		const string TB_Name = "TB";
		const double LightBorder_Padding = 7;
		static readonly Thickness LightBorder_Margin_Usual = new Thickness(0, 0, LightBorder_Padding, LightBorder_Padding);
		static readonly Thickness LightBorder_Margin_Pushed = new Thickness(LightBorder_Padding, LightBorder_Padding, 0, 0);
		#endregion

		#region Elements
		private Grid BaseGrid;
		private Border BaseBorder;
		private RotateTransform BtnLight1;
		private RotateTransform BtnLight2;
		private Border LightBorder;
		private Border InnerBorder;
		private TextBlock TB;
		#endregion

		private bool IsPushed = false;
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (BaseGrid != null)
			{
				//ref : https://www.atmarkit.co.jp/ait/articles/1601/06/news027.html

				BaseGrid.MouseDown -= BaseGrid_MouseDown;
				BaseGrid.MouseUp -= BaseGrid_MouseUp;
			}

			BaseGrid = GetTemplateChild(BaseGrid_Name) as Grid;
			BaseBorder = GetTemplateChild(BaseBorder_Name) as Border;
			BtnLight1 = GetTemplateChild(BtnLight1_Name) as RotateTransform;
			BtnLight2 = GetTemplateChild(BtnLight2_Name) as RotateTransform;
			LightBorder = GetTemplateChild(LightBorder_Name) as Border;
			InnerBorder = GetTemplateChild(InnerBorder_Name) as Border;
			TB = GetTemplateChild(TB_Name) as TextBlock;

			__Pushed += TIMSButton___Pushed;
			__Released += TIMSButton___Released;

			IsEnabledChanged += (s, e) => TB.Visibility = IsEnabled ? Visibility.Visible : Visibility.Collapsed;


			if (BaseGrid != null)
			{
				BaseGrid.MouseDown += BaseGrid_MouseDown;
				BaseGrid.MouseUp += BaseGrid_MouseUp;
			}
		}


		private void TIMSButton___Pushed(object sender, EventArgs e)
		{
			IsPushed = true;

			BaseBorder.Background = Brushes.Black;
			InnerBorder.SetBinding(BackgroundProperty, new Binding(nameof(PushedBackground)) { Source = this });

			TB.SetBinding(ForegroundProperty, new Binding(nameof(PushedTextColor)) { Source = this });

			LightBorder.Margin = LightBorder_Margin_Pushed;
			BtnLight1.Angle = BtnLight2.Angle = 180;

			CommonMethods.ButtonPushed();
		}
		private void TIMSButton___Released(object sender, EventArgs e)
		{
			IsPushed = false;

			InnerBorder.SetBinding(BackgroundProperty, new Binding(nameof(UsualBackground)) { Source = this });
			BaseBorder.SetBinding(BackgroundProperty, new Binding(nameof(UsualBackground)) { Source = this });

			TB.SetBinding(ForegroundProperty, new Binding(nameof(UsualTextColor)) { Source = this });

			LightBorder.Margin = LightBorder_Margin_Usual;

			BtnLight1.Angle = BtnLight2.Angle = 0;
		}

		private void BaseGrid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (IsEnabled)
				__Pushed?.Invoke(this, null);
		}
		private void BaseGrid_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (IsEnabled)
				__Released?.Invoke(this, null);
		}


		#region Properties
		public string ButtonText
		{
			get => (string)GetValue(ButtonTextProperty);
			set => SetValue(ButtonTextProperty, value);
		}

		public Brush UsualBackground
		{
			get => (Brush)GetValue(UsualBackgroundProperty);
			set => SetValue(UsualBackgroundProperty, value);
		}
		public Brush PushedBackground
		{
			get => (Brush)GetValue(PushedBackgroundProperty);
			set => SetValue(PushedBackgroundProperty, value);
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
		public Brush PushedTextColor
		{
			get => (Brush)GetValue(PushedTextColorProperty);
			set => SetValue(PushedTextColorProperty, value);
		}
		public Brush FlippedTextColor
		{
			get => (Brush)GetValue(FlippedTextColorProperty);
			set => SetValue(FlippedTextColorProperty, value);
		}

		public bool IsBlinking
		{
			get => (bool)GetValue(IsBlinkingProperty);
			set => SetValue(IsBlinkingProperty, value);
		}
		#endregion

		#region Events
		private event EventHandler __Pushed;
		public event EventHandler Pushed { add => __Pushed += value; remove => __Pushed -= value; }
		private event EventHandler __Released;
		public event EventHandler Released { add => __Released += value; remove => __Released -= value; }
		#endregion
	}

}
