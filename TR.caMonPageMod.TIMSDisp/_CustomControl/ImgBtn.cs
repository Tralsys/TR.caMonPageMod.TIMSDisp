using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class ImgBtn : Control
	{
		public static readonly DependencyProperty DirectoryProperty = DependencyProperty.Register("Directory", typeof(string), typeof(ImgBtn), new PropertyMetadata("BL"));

		public static readonly DependencyProperty ImgHeightProperty = DependencyProperty.Register("ImgHeight", typeof(double), typeof(ImgBtn), new PropertyMetadata(16.0));
		public static readonly DependencyProperty ImgWidthProperty = DependencyProperty.Register("ImgWidth", typeof(double), typeof(ImgBtn), new PropertyMetadata(16.0));

		public static readonly DependencyProperty UsualSuffixProperty = DependencyProperty.Register("UsualSuffix", typeof(string), typeof(ImgBtn), new PropertyMetadata("w.png"));
		public static readonly DependencyProperty PushedSuffixProperty = DependencyProperty.Register("PushedSuffix", typeof(string), typeof(ImgBtn), new PropertyMetadata("b.png"));
		public static readonly DependencyProperty FlippedSuffixProperty = DependencyProperty.Register("FlippedSuffix", typeof(string), typeof(ImgBtn), new PropertyMetadata("b.png"));

		public static readonly DependencyProperty ValueMinProperty = DependencyProperty.Register("ValueMin", typeof(int), typeof(ImgBtn), new PropertyMetadata(0));
		public static readonly DependencyProperty ValueMaxProperty = DependencyProperty.Register("ValueMax", typeof(int), typeof(ImgBtn), new PropertyMetadata(0));

		public static readonly DependencyProperty CurrentValueProperty = DependencyProperty.Register("CurrentValue", typeof(int), typeof(ImgBtn),
			new PropertyMetadata(0,
				(d, e) => (d as ImgBtn)?.CVChangedCallback(e),
				(d, v) =>
				{
					ImgBtn ib = d as ImgBtn;
					int cv = (int)v;

					if (cv < ib.ValueMin)
						return ib.ValueMax;
					else if (cv > ib.ValueMax)
						return ib.ValueMin;
					else
						return cv;
				}));

		public void CVChangedCallback(DependencyPropertyChangedEventArgs e)
		{
			int v = (int)e.NewValue;
			UsualImg = GetImgSource(v, UsualSuffix);
			PushedImg = GetImgSource(v, PushedSuffix);
			FlippedImg = null;
			CurrentBI = null;
		}

		public static readonly DependencyProperty UsualBackgroundProperty = DependencyProperty.Register("UsualBackground", typeof(Brush), typeof(ImgBtn), new PropertyMetadata(Brushes.Blue));
		public static readonly DependencyProperty PushedBackgroundProperty = DependencyProperty.Register("PushedBackground", typeof(Brush), typeof(ImgBtn), new PropertyMetadata(Brushes.Yellow));
		public static readonly DependencyProperty FlippedBackgroundProperty = DependencyProperty.Register("FlippedBackground", typeof(Brush), typeof(ImgBtn), new PropertyMetadata(Brushes.White));

		public static readonly DependencyProperty IsBlinkingProperty = DependencyProperty.Register("IsBlinking", typeof(bool), typeof(ImgBtn), new PropertyMetadata(false));

		static ImgBtn() => DefaultStyleKeyProperty.OverrideMetadata(typeof(ImgBtn), new FrameworkPropertyMetadata(typeof(ImgBtn)));
		
		public ImgBtn()
		{
			FrontPage.DT400Tick += (s, e) =>
			{
				if (InnerBorder == null || img == null)
					return;

				if (!IsEnabled || !IsBlinking)
				{
					//無効状態か, Blinkしないなら, Blink処理からの表示内容指定は行わない.
					CurrentBI = null;
					CurrentBrush = null;

					return;
				}

				FlippedImg ??= GetImgSource(CurrentValue, FlippedSuffix);//Blink開始次第, 必要に応じてFlippedImgへのパスを取得する.

				if (FrontPage.DT400_TFLoop)
				{
					CurrentBrush = FlippedBackground;
					CurrentBI = FlippedImg;
				}
				else
				{
					CurrentBrush = UsualBackground;
					CurrentBI = UsualImg;
				}

				if (!IsPushed)
				{
					InnerBorder.Background = CurrentBrush;
					img.Source = CurrentBI;
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
		const double LightBorder_Padding = 7;
		static readonly Thickness LightBorder_Margin_Usual = new Thickness(0, 0, LightBorder_Padding, LightBorder_Padding);
		static readonly Thickness LightBorder_Margin_Pushed = new Thickness(LightBorder_Padding, LightBorder_Padding, 0, 0);

		const string IMG_SOURCE_PATH = "pack://application:,,,/TR.caMonPageMod.TIMSDisp;component/";
		#endregion

		#region Elements
		private Grid BaseGrid;
		private Border BaseBorder;
		private RotateTransform BtnLight1;
		private RotateTransform BtnLight2;
		private Border LightBorder;
		private Border InnerBorder;
		private Image img;
		#endregion

		private BitmapImage GetImgSource(in int value, in string suffix)
			=> new BitmapImage(new Uri(new StringBuilder(IMG_SOURCE_PATH).Append(Directory).Append('/').Append(value).Append(suffix).ToString()));

		BitmapImage CurrentBI = null;
		BitmapImage UsualImg = null;
		BitmapImage PushedImg = null;
		BitmapImage FlippedImg = null;
		Brush CurrentBrush = null;

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
			img = GetTemplateChild("img") as Image;

			__Pushed += ImgBtn___Pushed;
			__Released += ImgBtn___Released;

			IsEnabledChanged += (s, e) => img.Visibility = IsEnabled ? Visibility.Visible : Visibility.Collapsed;

			UsualImg = GetImgSource(CurrentValue, UsualSuffix);
			img.Source = UsualImg;

			if (BaseGrid != null)
			{
				BaseGrid.MouseDown += BaseGrid_MouseDown;
				BaseGrid.MouseUp += BaseGrid_MouseUp;
			}

		}

		private bool IsPushed = false;

		private void ImgBtn___Pushed(object sender, EventArgs e)
		{
			CurrentValue--;

			IsPushed = true;
			
			BaseBorder.Background = Brushes.Black;
			InnerBorder.Background = PushedBackground;

			img.Source = PushedImg;

			LightBorder.Margin = LightBorder_Margin_Pushed;
			BtnLight1.Angle = BtnLight2.Angle = 180;
		}
		private void ImgBtn___Released(object sender, EventArgs e)
		{
			IsPushed = false;

			BaseBorder.Background = UsualBackground;

			img.Source = CurrentBI ?? UsualImg;//Blink処理からの表示内容指定がなければ, UsualImgを表示.
			InnerBorder.Background = CurrentBrush ?? UsualBackground;//Blink処理からの表示内容指定がなければ, UsualBackgroundを表示.

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
		public string Directory
		{
			get => (string)GetValue(DirectoryProperty);
			set => SetValue(DirectoryProperty, value);
		}

		public double ImgHeight
		{
			get => (double)GetValue(ImgHeightProperty);
			set => SetValue(ImgHeightProperty, value);
		}
		public double ImgWidth
		{
			get => (double)GetValue(ImgWidthProperty);
			set => SetValue(ImgWidthProperty, value);
		}

		public string UsualSuffix
		{
			get => (string)GetValue(UsualSuffixProperty);
			set => SetValue(UsualSuffixProperty, value);
		}
		public string PushedSuffix
		{
			get => (string)GetValue(PushedSuffixProperty);
			set => SetValue(PushedSuffixProperty, value);
		}
		public string FlippedSuffix
		{
			get => (string)GetValue(FlippedSuffixProperty);
			set => SetValue(FlippedSuffixProperty, value);
		}

		public int ValueMin
		{
			get => (int)GetValue(ValueMinProperty);
			set => SetValue(ValueMinProperty, value);
		}
		public int ValueMax
		{
			get => (int)GetValue(ValueMaxProperty);
			set => SetValue(ValueMaxProperty, value);
		}
		public int CurrentValue
		{
			get => (int)GetValue(CurrentValueProperty);
			set => SetValue(CurrentValueProperty, value);
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

		public bool IsBlinking
		{
			get => (bool)GetValue(IsBlinkingProperty);
			set => SetValue(IsBlinkingProperty, value);
		}

		private event EventHandler __Pushed;
		public event EventHandler Pushed { add => __Pushed += value; remove => __Pushed -= value; }
		private event EventHandler __Released;
		public event EventHandler Released { add => __Released += value; remove => __Released -= value; }
		#endregion
	}
}
