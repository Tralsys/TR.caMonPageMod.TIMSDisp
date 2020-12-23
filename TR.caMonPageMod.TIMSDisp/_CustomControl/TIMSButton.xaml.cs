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
		public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(TIMSButton));

		public static readonly DependencyProperty UsualBackgroundProperty = DependencyProperty.Register("UsualBackground", typeof(Brush), typeof(TIMSButton));
		public static readonly DependencyProperty PushedBackgroundProperty = DependencyProperty.Register("PushedBackground", typeof(Brush), typeof(TIMSButton));
		public static readonly DependencyProperty FlippedBackgroundProperty = DependencyProperty.Register("FlippedBackground", typeof(Brush), typeof(TIMSButton));
		
		public static readonly DependencyProperty UsualTextColorProperty = DependencyProperty.Register("UsualTextColor", typeof(Brush), typeof(TIMSButton));
		public static readonly DependencyProperty PushedTextColorProperty = DependencyProperty.Register("PushedTextColor", typeof(Brush), typeof(TIMSButton));
		public static readonly DependencyProperty FlippedTextColorProperty = DependencyProperty.Register("FlippedTextColor", typeof(Brush), typeof(TIMSButton));

		public static readonly DependencyProperty CurrentTextColorProperty = DependencyProperty.Register("CurrentTextColor", typeof(Brush), typeof(TIMSButton));

		public static readonly DependencyProperty ContentTransformProperty = DependencyProperty.Register("ContentTransform", typeof(Transform), typeof(TIMSButton));

		public static readonly DependencyProperty IsBlinkingProperty = DependencyProperty.Register("IsBlinking", typeof(bool), typeof(TIMSButton));

		public static readonly DependencyProperty HoldableProperty = DependencyProperty.Register("Holdable", typeof(bool), typeof(TIMSButton));
		public static readonly DependencyProperty IsPushedProperty = DependencyProperty.Register("IsPushed", typeof(bool), typeof(TIMSButton), new PropertyMetadata((s,e)=>
		{
			var v = s as TIMSButton;
			if ((bool)e.NewValue)
				v.SetStyleToPushed();
			else
				v.SetStyleToReleased();
			v?.__IsPushedPropertyChanged?.Invoke(v, new ValueChangedEventArgs<bool>((bool)e.OldValue, (bool)e.NewValue));
		}));

		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(UIElement), typeof(TIMSButton), new PropertyMetadata((s,e)=>
		{
			var v = s as TIMSButton;
			if (v?.ContentBorder is not null)
				v.ContentBorder.Child = e.NewValue as UIElement;
		}));
		#endregion

		static TIMSButton() => DefaultStyleKeyProperty.OverrideMetadata(typeof(TIMSButton), new FrameworkPropertyMetadata(typeof(TIMSButton)));
		

		public TIMSButton()
		{
			FrontPage.DT400Tick += (s,e)=>
			{
				if (!IsEnabled || IsPushed || !IsBlinking)
					return;

				if (InnerBorder == null)
					return;

				if (FrontPage.DT400_TFLoop)
				{
					InnerBorder.SetBinding(BackgroundProperty, new Binding(nameof(FlippedBackground)) { Source = this });
					SetBinding(CurrentTextColorProperty, new Binding(nameof(FlippedTextColor)) { Source = this });
				}
				else
				{
					InnerBorder.SetBinding(BackgroundProperty, new Binding(nameof(UsualBackground)) { Source = this });
					SetBinding(CurrentTextColorProperty, new Binding(nameof(UsualTextColor)) { Source = this });
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
		const string ContentBorder_Name = "ContentBorder";
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

		private Border ContentBorder;
		#endregion

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
			ContentBorder = GetTemplateChild(ContentBorder_Name) as Border;

			__Pushed += TIMSButton___Pushed;
			__Released += TIMSButton___Released;

			if (ContentBorder is not null && ContentBorder.Child != Content)
				ContentBorder.Child = Content;

			if (BaseGrid != null)
			{
				BaseGrid.MouseDown += BaseGrid_MouseDown;
				BaseGrid.MouseUp += BaseGrid_MouseUp;
			}

			SetStyleToReleased();
		}


		private void TIMSButton___Pushed(object sender, EventArgs e)
		{
			if (Holdable)
				IsPushed = !IsPushed;
			else
				IsPushed = true;

			CommonMethods.ButtonPushed();
		}
		private void TIMSButton___Released(object sender, EventArgs e)
		{
			if (!Holdable)
				IsPushed = false;
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

		#region Button Style Manager
		private void SetStyleToPushed()
		{
			BaseBorder.Background = Brushes.Black;
			InnerBorder?.SetBinding(BackgroundProperty, new Binding(nameof(PushedBackground)) { Source = this });

			SetBinding(CurrentTextColorProperty, new Binding(nameof(PushedTextColor)) { Source = this });

			LightBorder.Margin = LightBorder_Margin_Pushed;
			BtnLight1.Angle = BtnLight2.Angle = 180;
		}
		private void SetStyleToReleased()
		{
			InnerBorder?.SetBinding(BackgroundProperty, new Binding(nameof(UsualBackground)) { Source = this });
			BaseBorder?.SetBinding(BackgroundProperty, new Binding(nameof(UsualBackground)) { Source = this });

			SetBinding(CurrentTextColorProperty, new Binding(nameof(UsualTextColor)) { Source = this });

			if (LightBorder is not null)
				LightBorder.Margin = LightBorder_Margin_Usual;

			if (BtnLight1 is not null && BtnLight2 is not null)
				BtnLight1.Angle = BtnLight2.Angle = 0;
		}
		#endregion

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

		public Brush CurrentTextColor
		{
			get => (Brush)GetValue(CurrentTextColorProperty);
			private set => SetValue(CurrentTextColorProperty, value);
		}

		public Transform ContentTransform
		{
			get => (Transform)GetValue(ContentTransformProperty);
			private set => SetValue(ContentTransformProperty, value);
		}

		public bool IsBlinking
		{
			get => (bool)GetValue(IsBlinkingProperty);
			set => SetValue(IsBlinkingProperty, value);
		}
		
		public bool Holdable
		{
			get => (bool)GetValue(HoldableProperty);
			set => SetValue(HoldableProperty, value);
		}
		public bool IsPushed
		{
			get => (bool)GetValue(IsPushedProperty);
			set => SetValue(IsPushedProperty, value);
		}

		public UIElement Content
		{
			get => (UIElement)GetValue(ContentProperty);
			set => SetValue(ContentProperty, value);
		}
		#endregion

		#region Events
		protected event EventHandler<ValueChangedEventArgs<bool>> __IsPushedPropertyChanged;
		public event EventHandler<ValueChangedEventArgs<bool>> IsPushedPropertyChanged { add => __IsPushedPropertyChanged += value; remove => __IsPushedPropertyChanged -= value; }
		protected event EventHandler __Pushed;
		public event EventHandler Pushed { add => __Pushed += value; remove => __Pushed -= value; }
		protected event EventHandler __Released;
		public event EventHandler Released { add => __Released += value; remove => __Released -= value; }
		#endregion
	}

}
