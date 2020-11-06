using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class MyImage : Control
	{
		static public DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(BitmapSource), typeof(MyImage), new PropertyMetadata(OnSourceChanged));

		private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
			=> (d as MyImage)?.DrawImage();
		

		public BitmapSource Source
		{
			get => (BitmapSource)GetValue(SourceProperty);
			set => SetValue(SourceProperty, value);
		}
		static MyImage() => DefaultStyleKeyProperty.OverrideMetadata(typeof(MyImage), new FrameworkPropertyMetadata(typeof(MyImage)));
		Image img;
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			img = GetTemplateChild("img") as Image;
			DrawImage();
		}

		const int BytesPerPixel = 4;
		private void DrawImage()
		{
			BitmapSource bs = Source;

			if (Height <= 0 || Width <= 0 || !IsEnabled || bs == null || img == null)
				return;

			int w = bs.PixelWidth;
			int h = bs.PixelHeight;

			byte[] pxArr = new byte[w * h * BytesPerPixel];

			bs.CopyPixels(pxArr, w * BytesPerPixel, 0);
			

			WriteableBitmap wb = new WriteableBitmap(w + 2, h + 2, 96, 96, PixelFormats.Pbgra32, null);
			wb.Lock();
			try
			{
				wb.WritePixels(new Int32Rect(1, 1, w, h), pxArr, w * BytesPerPixel, 0);
			}
			finally
			{
				wb.Unlock();
			}

			img.Source = wb;
		}
	}
}
