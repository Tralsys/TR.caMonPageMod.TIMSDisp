using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using TR.caMonPageMod.TIMSDisp._UsefulFuncs;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	//ref : https://docs.microsoft.com/ja-jp/dotnet/api/system.windows.media.imaging.writeablebitmap?redirectedfrom=MSDN&view=netcore-3.1
	//ref : https://water2litter.net/rye/post/c_graphic_draw_writablebitmap/

	public class BitmapCircle : Control
	{
		static public readonly DependencyProperty IsFilledProperty = DependencyProperty.Register("IsFilled", typeof(bool), typeof(BitmapCircle),
			new PropertyMetadata(false, (s, e) => (s as BitmapCircle)?.UpdateCircleImage()));
		static public readonly DependencyProperty FillWithEdgeProperty = DependencyProperty.Register("FillWithEdge", typeof(bool), typeof(BitmapCircle),
			new PropertyMetadata(true, (s, e) => (s as BitmapCircle)?.UpdateCircleImage()));

		static public readonly DependencyProperty CircleTypeProperty = DependencyProperty.Register("CircleType", typeof(CircleTypes), typeof(BitmapCircle),
			new PropertyMetadata(CircleTypes.None));

		public enum CircleTypes
		{
			None,
			X8Y7,
			X7Y7,
			X7Y8,
			X13Y13,
			X15Y15,
			X50Y50,
		}
		
		private static readonly ReadOnlyDictionary<CircleTypes, Point> Radius;



		static BitmapCircle()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(BitmapCircle), new FrameworkPropertyMetadata(typeof(BitmapCircle)));

			var radius = new Dictionary<CircleTypes, Point>();
			radius.Add(CircleTypes.None, new Point(0, 0));
			radius.Add(CircleTypes.X7Y8, new Point(7, 8));
			radius.Add(CircleTypes.X8Y7, new Point(8, 7));
			radius.Add(CircleTypes.X7Y7, new Point(7, 7));
			radius.Add(CircleTypes.X13Y13, new Point(13, 13));
			radius.Add(CircleTypes.X15Y15, new Point(15, 15));
			radius.Add(CircleTypes.X50Y50, new Point(50, 50));

			Radius = new ReadOnlyDictionary<CircleTypes, Point>(radius);
		}

		Image img;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			img = GetTemplateChild("CircleImg") as Image;

			SizeChanged += (s, e) => UpdateCircleImage();

			UpdateCircleImage();
		}
		const int BytesPerPixel = 4;
		static readonly byte[] Transparent = { 0x00, 0x00, 0x00, 0x00 };
		private void UpdateCircleImage()
		{
			//ref : http://mini09memo.blog.fc2.com/blog-entry-100.html

			if (img == null)
				return;
			Point p = Radius.GetValueOrDefault(CircleType);
			int h = (int)(p.Y);
			int w = (int)(p.X);
			WriteableBitmap wb = new WriteableBitmap(w + 2, h + 2, 96, 96, PixelFormats.Pbgra32, null);//下部消失対策に, 余白を取る

			byte[] pxArr = new byte[h * w * BytesPerPixel];
			Parallel.For(0, pxArr.Length, (i) => pxArr[i] = 0x00);
			
			byte[] Inner = UsefulFuncs.GetPixels(IsFilled ? Foreground : Background) ?? Transparent;
			byte[] Edge = (IsFilled && FillWithEdge) ? Inner//EdgeをInnerと同色に
				: UsefulFuncs.GetPixels(BorderBrush) ?? Transparent;
			byte[][] bits = BitmapTypes.Bits.GetValueOrDefault(CircleType);

			if (bits?.Length <= 0)
			{
				img.Source = null;
				return;
			}


			Parallel.For(0, h, (y) =>
			{
				for (int x = 0; x < w; x++)
					Array.Copy(bits[x][y] switch
					{
						BitmapTypes.Edge => Edge,
						BitmapTypes.Inner => Inner,
						BitmapTypes.None => Transparent,
						_ => Transparent
					}, 0, pxArr, ((y * w) + x) * BytesPerPixel, Transparent.Length);
			});
			wb.Lock();
			try
			{
				wb.WritePixels(new Int32Rect(1, 1, w, h), pxArr, w * BytesPerPixel, 0);//余白分だけ内側に描画
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			finally
			{
				wb.Unlock();
			}

			img.Source = wb;
		}

		public bool IsFilled
		{
			get => (bool)GetValue(IsFilledProperty);
			set => SetValue(IsFilledProperty, value);
		}
		public bool FillWithEdge
		{
			get => (bool)GetValue(FillWithEdgeProperty);
			set => SetValue(FillWithEdgeProperty, value);
		}
		public CircleTypes CircleType
		{
			get => (CircleTypes)GetValue(CircleTypeProperty);
			set => SetValue(CircleTypeProperty, value);
		}


		static public class BitmapTypes
		{
			public const byte None = n;
			public const byte Edge = e;
			public const byte Inner = i;
			const byte n = 0x00;//None
			const byte e = 0x01;//Edge
			const byte i = 0x02;//Inner

			static public ReadOnlyDictionary<CircleTypes, byte[][]> Bits;

			static BitmapTypes()
			{
				Dictionary<CircleTypes, byte[][]> bits = new Dictionary<CircleTypes, byte[][]>();

				bits.Add(CircleTypes.None, new byte[0][]);
				bits.Add(CircleTypes.X7Y7, X7Y7);
				bits.Add(CircleTypes.X8Y7, X8Y7);
				bits.Add(CircleTypes.X7Y8, X7Y8);
				bits.Add(CircleTypes.X13Y13, X13Y13);
				bits.Add(CircleTypes.X15Y15, X15Y15);
				bits.Add(CircleTypes.X50Y50, X50Y50);

				Bits = new ReadOnlyDictionary<CircleTypes, byte[][]>(bits);
			}

			static public readonly byte[][] X7Y7 = new byte[7][]
			{
				new byte[7] { n, n, e, e, e, n, n },
				new byte[7] { n, e, i, i, i, e, n },
				new byte[7] { e, i, i, i, i, i, e },
				new byte[7] { e, i, i, i, i, i, e },
				new byte[7] { e, i, i, i, i, i, e },
				new byte[7] { n, e, i, i, i, e, n },
				new byte[7] { n, n, e, e, e, n, n }
			};

			static public readonly byte[][] X8Y7 = new byte[8][]
			{
				new byte[7] { n, n, e, e, e, n, n },
				new byte[7] { n, e, i, i, i, e, n },
				new byte[7] { e, i, i, i, i, i, e },
				new byte[7] { e, i, i, i, i, i, e },
				new byte[7] { e, i, i, i, i, i, e },
				new byte[7] { e, i, i, i, i, i, e },
				new byte[7] { n, e, i, i, i, e, n },
				new byte[7] { n, n, e, e, e, n, n }
			};

			static public readonly byte[][] X7Y8 = new byte[7][]
			{
				new byte[8] { n, n, e, e, e, e, n, n },
				new byte[8] { n, e, i, i, i, i, e, n },
				new byte[8] { e, i, i, i, i, i, i, e },
				new byte[8] { e, i, i, i, i, i, i, e },
				new byte[8] { e, i, i, i, i, i, i, e },
				new byte[8] { n, e, i, i, i, i, e, n },
				new byte[8] { n, n, e, e, e, e, n, n },
			};

			static public readonly byte[][] X15Y15 = new byte[15][]
			{
				new byte[15] { n, n, n, n, n, e, e, e, e, e, n, n, n, n, n },
				new byte[15] { n, n, n, e, e, i, i, i, i, i, e, e, n, n, n },
				new byte[15] { n, n, e, i, i, i, i, i, i, i, i, i, e, n, n },
				new byte[15] { n, e, i, i, i, i, i, i, i, i, i, i, i, e, n },
				new byte[15] { n, e, i, i, i, i, i, i, i, i, i, i, i, e, n },
				new byte[15] { e, i, i, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[15] { e, i, i, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[15] { e, i, i, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[15] { e, i, i, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[15] { e, i, i, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[15] { n, e, i, i, i, i, i, i, i, i, i, i, i, e, n },
				new byte[15] { n, e, i, i, i, i, i, i, i, i, i, i, i, e, n },
				new byte[15] { n, n, e, i, i, i, i, i, i, i, i, i, e, n, n },
				new byte[15] { n, n, n, e, e, i, i, i, i, i, e, e, n, n, n },
				new byte[15] { n, n, n, n, n, e, e, e, e, e, n, n, n, n, n },
			};

			static public readonly byte[][] X13Y13 = new byte[13][]
			{
				new byte[13] { n, n, n, n, e, e, e, e, e, n, n, n, n },
				new byte[13] { n, n, e, e, i, i, i, i, i, e, e, n, n },
				new byte[13] { n, e, i, i, i, i, i, i, i, i, i, e, n },
				new byte[13] { n, e, i, i, i, i, i, i, i, i, i, e, n },
				new byte[13] { e, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[13] { e, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[13] { e, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[13] { e, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[13] { e, i, i, i, i, i, i, i, i, i, i, i, e },
				new byte[13] { n, e, i, i, i, i, i, i, i, i, i, e, n },
				new byte[13] { n, e, i, i, i, i, i, i, i, i, i, e, n },
				new byte[13] { n, n, e, e, i, i, i, i, i, e, e, n, n },
				new byte[13] { n, n, n, n, e, e, e, e, e, n, n, n, n },
			};

			static public readonly byte[][] X50Y50 = new byte[][]
			{
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, e, e, e, e, e, e, e, e, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, n, n, n, e, e, e, e, e, e, e, i, i, i, i, i, i, i, i, e, e, e, e, e, e, e, n, n, n, n, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, n, n, e, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, e, n, n, n, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, n, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n, n },
				new byte[50] { n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n },
				new byte[50] { n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n },
				new byte[50] { n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n },
				new byte[50] { n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n },
				new byte[50] { n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n },
				new byte[50] { n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n },
				new byte[50] { n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n },
				new byte[50] { n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n },
				new byte[50] { n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n },
				new byte[50] { n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n },
				new byte[50] { e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e },
				new byte[50] { e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e },
				new byte[50] { e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e },
				new byte[50] { e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e },
				new byte[50] { e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e },
				new byte[50] { e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e },
				new byte[50] { e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e },
				new byte[50] { e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e },
				new byte[50] { n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n },
				new byte[50] { n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n },
				new byte[50] { n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n },
				new byte[50] { n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n },
				new byte[50] { n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n },
				new byte[50] { n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n },
				new byte[50] { n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n },
				new byte[50] { n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n },
				new byte[50] { n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n },
				new byte[50] { n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n },
				new byte[50] { n, n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, n, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, n, n, e, e, e, e, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, i, e, e, e, e, n, n, n, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, n, n, n, e, e, e, e, e, e, e, i, i, i, i, i, i, i, i, e, e, e, e, e, e, e, n, n, n, n, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, e, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n },
				new byte[50] { n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, e, e, e, e, e, e, e, e, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n, n }
			};
		}
	}

}
