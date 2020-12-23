using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class ImgBtn : TIMSButton
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

		public static readonly DependencyProperty IsValueChangeToPositiveDirectionProperty = DependencyProperty.Register("IsValueChangeToPositiveDirection", typeof(bool), typeof(ImgBtn), new PropertyMetadata(false));

		/// <summary>CurrentValueChangedCallback</summary>
		/// <param name="e"></param>
		public void CVChangedCallback(DependencyPropertyChangedEventArgs e)
		{
			int v = (int)e.NewValue;
			UsualImg = GetImgSource(v, UsualSuffix);
			PushedImg = GetImgSource(v, PushedSuffix);
			FlippedImg = null;
			CurrentBI = null;

			__CurrentValueChanged?.Invoke(this, new ValueChangedEventArgs<int>((int)e.OldValue, v));
		}

		static ImgBtn() => DefaultStyleKeyProperty.OverrideMetadata(typeof(ImgBtn), new FrameworkPropertyMetadata(typeof(ImgBtn)));
		
		public ImgBtn()
		{
			FrontPage.DT400Tick += (s, e) =>
			{
				if (img == null)
					return;

				if (!IsEnabled || !IsBlinking)
				{
					//無効状態か, Blinkしないなら, Blink処理からの表示内容指定は行わない.
					CurrentBI = null;

					return;
				}

				FlippedImg ??= GetImgSource(CurrentValue, FlippedSuffix);//Blink開始次第, 必要に応じてFlippedImgへのパスを取得する.

				CurrentBI = FrontPage.DT400_TFLoop ? FlippedImg : UsualImg;

				if (!IsPushed)
					img.Source = CurrentBI;
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
		private Image img = new Image();
		#endregion

		private BitmapImage GetImgSource(in int value, in string suffix)
			=> new BitmapImage(new Uri(new StringBuilder(IMG_SOURCE_PATH).Append(Directory).Append('/').Append(value).Append(suffix).ToString()));

		BitmapImage CurrentBI = null;//Current Blinking Image
		BitmapImage UsualImg = null;
		BitmapImage PushedImg = null;
		BitmapImage FlippedImg = null;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			UsualImg = GetImgSource(CurrentValue, UsualSuffix);

			//img = GetTemplateChild("img") as Image;
			if (img is not null)
			{
				Content = img;
				img.SetBinding(Image.VisibilityProperty, new Binding(nameof(IsEnabled)) { Source = this, Converter = new _UsefulFuncs.BoolToVisibility() });
				img.SetBinding(Image.HeightProperty, new Binding(nameof(ImgHeight)) { Source = this });
				img.SetBinding(Image.WidthProperty, new Binding(nameof(ImgWidth)) { Source = this });
				img.Source = UsualImg;
			}

			IsPushedPropertyChanged += Btn_base_IsPushedPropertyChanged;
		}

		private void Btn_base_IsPushedPropertyChanged(object sender, ValueChangedEventArgs<bool> e)
		{
			img.Source = (bool)e.NewValue ? PushedImg : (CurrentBI ?? UsualImg);
			if (e.NewValue)
			{
				if (IsValueChangeToPositiveDirection)
					CurrentValue++;
				else
					CurrentValue--;
			}
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

		public bool IsValueChangeToPositiveDirection
		{
			get => (bool)GetValue(IsValueChangeToPositiveDirectionProperty);
			set => SetValue(IsValueChangeToPositiveDirectionProperty, value);
		}
		#endregion

		#region Events
		private event EventHandler<ValueChangedEventArgs<int>> __CurrentValueChanged;
		public event EventHandler<ValueChangedEventArgs<int>> CurrentValueChanged { add => __CurrentValueChanged += value; remove => __CurrentValueChanged -= value; }
		#endregion
	}
}
