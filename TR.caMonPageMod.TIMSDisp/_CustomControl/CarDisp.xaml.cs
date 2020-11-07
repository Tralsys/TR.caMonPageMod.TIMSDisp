using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using TR.caMonPageMod.TIMSDisp._UsefulFuncs;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class CarDisp : Control
	{
		#region DependencyProperties

		#region Door Block
		public static readonly DependencyProperty HasDoorProperty = DependencyProperty.Register("HasDoor", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));
		public static readonly DependencyProperty IsDoorClosedProperty = DependencyProperty.Register("IsDoorClosed", typeof(bool), typeof(CarDisp),
			new PropertyMetadata(true, (s, e)=>
			{
				if(s is CarDisp cd)
				{
					if ((bool)e.NewValue)
					{//NewState : DoorClosed
						cd.SetBinding(CurrentDoorBrushProperty, new Binding(nameof(DoorCloseBrush)) { Source = cd });
						cd.SetBinding(CurrentDoorTextColorProperty, new Binding(nameof(DoorCloseTextColor)) { Source = cd });
					}
					else
					{//NewState : DoorOpen
						cd.SetBinding(CurrentDoorBrushProperty, new Binding(nameof(DoorOpenBrush)) { Source = cd });
						cd.SetBinding(CurrentDoorTextColorProperty, new Binding(nameof(DoorOpenTextColor)) { Source = cd });
					}
				}
			}));

		public static readonly DependencyProperty DoorBlockEdgeBrushProperty = DependencyProperty.Register("DoorBlockEdgeBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty CurrentDoorBrushProperty = DependencyProperty.Register("CurrentDoorBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black));
		public static readonly DependencyProperty CurrentDoorTextColorProperty = DependencyProperty.Register("CurrentDoorTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));

		public static readonly DependencyProperty DoorOpenBrushProperty = DependencyProperty.Register("DoorOpenBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Red));
		public static readonly DependencyProperty DoorCloseBrushProperty = DependencyProperty.Register("DoorCloseBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black));
		public static readonly DependencyProperty DoorOpenTextColorProperty = DependencyProperty.Register("DoorOpenTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty DoorCloseTextColorProperty = DependencyProperty.Register("DoorCloseTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));

		public static readonly DependencyProperty DoorBlockWidthProperty = DependencyProperty.Register("DoorBlockWidth", typeof(double), typeof(CarDisp), new PropertyMetadata(45.0));
		public static readonly DependencyProperty DoorBlockHeightProperty = DependencyProperty.Register("DoorBlockHeight", typeof(double), typeof(CarDisp), new PropertyMetadata(22.0));
		#endregion Door Block

		#region Pantograph
		public static readonly DependencyProperty LeftPanRaisedProperty = DependencyProperty.Register("LeftPanRaised", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));
		public static readonly DependencyProperty RightPanRaisedProperty = DependencyProperty.Register("RightPanRaised", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));

		public static readonly DependencyProperty RaisedPanBrushProperty = DependencyProperty.Register("RaisedPanBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty LowerPanBrushProperty = DependencyProperty.Register("LowerPanBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));

		public static readonly DependencyProperty LeftPanStyleProperty = DependencyProperty.Register("LeftPanStyle", typeof(PantographStyle), typeof(CarDisp), new PropertyMetadata(PantographStyle.SingleArm_LeftJoint));
		public static readonly DependencyProperty RightPanStyleProperty = DependencyProperty.Register("RightPanStyle", typeof(PantographStyle), typeof(CarDisp), new PropertyMetadata(PantographStyle.SingleArm_RightJoint));

		public static readonly DependencyProperty LeftPanMarginProperty = DependencyProperty.Register("LeftPanMargin", typeof(Thickness), typeof(CarDisp), new PropertyMetadata(new Thickness(6, 0, 6, 0)));
		public static readonly DependencyProperty RightPanMarginProperty = DependencyProperty.Register("RightPanMargin", typeof(Thickness), typeof(CarDisp), new PropertyMetadata(new Thickness(6, 0, 6, 0)));
		#endregion

		#region Bogie
		public static readonly DependencyProperty IsLeftBogieMotoredProperty = DependencyProperty.Register("IsLeftBogieMotored", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));
		public static readonly DependencyProperty IsRightBogieMotoredProperty = DependencyProperty.Register("IsRightBogieMotored", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));

		public static readonly DependencyProperty TrailerBogieBrushProperty = DependencyProperty.Register("TrailerBogieBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty MotoredBogieBrushProperty = DependencyProperty.Register("MotoredBogieBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));

		public static readonly DependencyProperty BogieHeightProperty = DependencyProperty.Register("BogieHeight", typeof(double), typeof(CarDisp), new PropertyMetadata(7.0));
		public static readonly DependencyProperty BogieWidthProperty = DependencyProperty.Register("BogieWidth", typeof(double), typeof(CarDisp), new PropertyMetadata(8.0));

		public static readonly DependencyProperty LeftBogieMarginProperty = DependencyProperty.Register("LeftBogieMargin", typeof(Thickness), typeof(CarDisp), new PropertyMetadata(new Thickness(6, 0, 6, 0)));
		public static readonly DependencyProperty RightBogieMarginProperty = DependencyProperty.Register("RightBogieMargin", typeof(Thickness), typeof(CarDisp), new PropertyMetadata(new Thickness(6, 0, 6, 0)));
		#endregion

		#region Car Block
		public static readonly DependencyProperty CarNumberProperty = DependencyProperty.Register("CarNumber", typeof(int), typeof(CarDisp), new PropertyMetadata(1, CarNumberPropChanged));
		public static readonly DependencyProperty CarNumberStrProperty = DependencyProperty.Register("CarNumberStr", typeof(string), typeof(CarDisp), new PropertyMetadata("１"));
		public static readonly DependencyProperty CurrentCarNumberTextColorProperty = DependencyProperty.Register("CurrentCarNumberTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty CarNumberTextColorProperty = DependencyProperty.Register("CarNumberTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));
		public static readonly DependencyProperty CarNumberPowerTextColorProperty = DependencyProperty.Register("CarNumberPowerTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black));
		public static readonly DependencyProperty CarNumberBrakeTextColorProperty = DependencyProperty.Register("CarNumberBrakeTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black));
		public static readonly DependencyProperty CarNumberRescueTextColorProperty = DependencyProperty.Register("CarNumberRescueTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Red));

		public static readonly DependencyProperty CarBlockWidthProperty = DependencyProperty.Register("CarBlockWidth", typeof(double), typeof(CarDisp), new PropertyMetadata(40.0, CarBlockReDraw));
		public static readonly DependencyProperty CarBlockCurrentHeightProperty = DependencyProperty.Register("CarBlockCurrentHeight", typeof(double), typeof(CarDisp), new PropertyMetadata(20.0, CarBlockReDraw));
		public static readonly DependencyProperty CarBlockNormalHeightProperty = DependencyProperty.Register("CarBlockNormalHeight", typeof(double), typeof(CarDisp), new PropertyMetadata(20.0, CarBlockReDraw));
		public static readonly DependencyProperty CarBlockDoubleDeckerHeightProperty = DependencyProperty.Register("CarBlockDoubleDeckerHeight", typeof(double), typeof(CarDisp), new PropertyMetadata(30.0, CarBlockReDraw));

		public static readonly DependencyProperty IsLeftEdgeHEADProperty = DependencyProperty.Register("IsLeftEdgeHEAD", typeof(bool), typeof(CarDisp), new PropertyMetadata(true, CarBlockReDraw));
		public static readonly DependencyProperty IsRightEdgeHEADProperty = DependencyProperty.Register("IsRightEdgeHEAD", typeof(bool), typeof(CarDisp), new PropertyMetadata(true, CarBlockReDraw));

		public static readonly DependencyProperty IsLeftEdgeRescueSWTrippedProperty = DependencyProperty.Register("IsLeftEdgeRescueSWTripped", typeof(bool), typeof(CarDisp), new PropertyMetadata(false, CarBlockReDraw));
		public static readonly DependencyProperty IsRightEdgeRescueSWTrippedProperty = DependencyProperty.Register("IsRightEdgeRescueSWTripped", typeof(bool), typeof(CarDisp), new PropertyMetadata(false, CarBlockReDraw));

		public static readonly DependencyProperty IsDoubleDeckerProperty = DependencyProperty.Register("IsDoubleDecker", typeof(bool), typeof(CarDisp),
			new PropertyMetadata(false, CarBlockReDraw));

		public static readonly DependencyProperty MotorStateProperty = DependencyProperty.Register("MotorState", typeof(MState), typeof(CarDisp),
			new PropertyMetadata(MState.Accel, (s,e)=>
			{
				SetCarNumTextColorBinding(s as CarDisp);
				CarBlockReDraw(s, e);
			}));

		public static readonly DependencyProperty CarEdgeBrushProperty = DependencyProperty.Register("CarEdgeBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White, CarBlockReDraw));
		public static readonly DependencyProperty CarInnerBrushProperty = DependencyProperty.Register("CarInnerBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black, CarBlockReDraw));
		public static readonly DependencyProperty CarPowerBrushProperty = DependencyProperty.Register("CarPowerBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Aqua, CarBlockReDraw));
		public static readonly DependencyProperty CarBrakeBrushProperty = DependencyProperty.Register("CarBrakeBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Yellow, CarBlockReDraw));

		public static readonly DependencyProperty CarBlockMarginProperty = DependencyProperty.Register("CarBlockMargin", typeof(Thickness), typeof(CarDisp), new PropertyMetadata(new Thickness(0)));

		private static void CarBlockReDraw(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is CarDisp cd)
			{
				//CarImgの更新
				if (cd.CarImg != null)
					cd.CarImg.Source = cd.SetCarBitmap(cd.CarImg.Source as WriteableBitmap);

				cd.CarBlockMargin = cd.IsDoubleDecker ? CarBlock_DDMargin : CarBlock_UsualMargin;
				string s = cd.IsDoubleDecker ? nameof(CarBlockDoubleDeckerHeight) : nameof(CarBlockNormalHeight);
				cd.SetBinding(CarBlockCurrentHeightProperty, new Binding(s) { Source = cd });
			}
		}
		private static void CarNumberPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			int cn = (int)e.NewValue;
			(d as CarDisp).CarNumberStr = (string)UsefulFuncs.WideNarrowConv(new Char_WideNarrowSetting(cn >= 10), cn.ToString());//10以上(2桁)なら半角, 1桁なら全角
		}
		
		private static void SetCarNumTextColorBinding(in CarDisp cd)
		{
			if (cd == null)
				return;

			string str = cd.MotorState switch
			{
				MState.Accel => nameof(CarNumberPowerTextColor),
				MState.Brake => nameof(CarNumberBrakeTextColor),
				MState.None => nameof(CarNumberTextColor),
				_ => nameof(CarNumberTextColor)
			};
			cd.SetBinding(CurrentCarNumberTextColorProperty, new Binding(str) { Source = cd });
		}
		#endregion
		#endregion DependencyProperties


		static CarDisp() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CarDisp), new FrameworkPropertyMetadata(typeof(CarDisp)));

		Image CarImg = null;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			CarImg = GetTemplateChild("CarImg") as Image;

			CarImg.Source = SetCarBitmap();
			SetCarNumTextColorBinding(this);
		}

		public enum PantographStyle
		{
			None,
			Diamond,
			Crossed,
			SingleArm_LeftJoint,
			SingleArm_RightJoint
		}

		public enum MState
		{
			None,
			Accel,
			Brake
		}


		#region Properties   

		#region Door Properties
		public bool HasDoor
		{
			get => (bool)GetValue(HasDoorProperty);
			set => SetValue(HasDoorProperty, value);
		}
		public bool IsDoorClosed
		{
			get => (bool)GetValue(IsDoorClosedProperty);
			set => SetValue(IsDoorClosedProperty, value);
		}


		public Brush DoorBlockEdgeBrush
		{
			get => (Brush)GetValue(DoorBlockEdgeBrushProperty);
			set => SetValue(DoorBlockEdgeBrushProperty, value);
		}
		public Brush CurrentDoorBrush
		{
			get => (Brush)GetValue(CurrentDoorBrushProperty);
			set => SetValue(CurrentDoorBrushProperty, value);
		}
		public Brush CurrentDoorTextColor
		{
			get => (Brush)GetValue(CurrentDoorTextColorProperty);
			set => SetValue(CurrentDoorTextColorProperty, value);
		}

		public Brush DoorOpenBrush
		{
			get => (Brush)GetValue(DoorOpenBrushProperty);
			set => SetValue(DoorOpenBrushProperty, value);
		}
		public Brush DoorCloseBrush
		{
			get => (Brush)GetValue(DoorCloseBrushProperty);
			set => SetValue(DoorCloseBrushProperty, value);
		}
		public Brush DoorOpenTextColor
		{
			get => (Brush)GetValue(DoorOpenTextColorProperty);
			set => SetValue(DoorOpenTextColorProperty, value);
		}
		public Brush DoorCloseTextColor
		{
			get => (Brush)GetValue(DoorCloseTextColorProperty);
			set => SetValue(DoorCloseTextColorProperty, value);
		}

		public double DoorBlockWidth
		{
			get => (double)GetValue(DoorBlockWidthProperty);
			set => SetValue(DoorBlockWidthProperty, value);
		}
		public double DoorBlockHeight
		{
			get => (double)GetValue(DoorBlockHeightProperty);
			set => SetValue(DoorBlockHeightProperty, value);
		}
		#endregion

		#region Pantograph
		public bool LeftPanRaised
		{
			get => (bool)GetValue(LeftPanRaisedProperty);
			set => SetValue(LeftPanRaisedProperty, value);
		}
		public bool RightPanRaised
		{
			get => (bool)GetValue(RightPanRaisedProperty);
			set => SetValue(RightPanRaisedProperty, value);
		}
		public PantographStyle LeftPanStyle
		{
			get => (PantographStyle)GetValue(LeftPanStyleProperty);
			set => SetValue(LeftPanStyleProperty, value);
		}
		public PantographStyle RightPanStyle
		{
			get => (PantographStyle)GetValue(RightPanStyleProperty);
			set => SetValue(RightPanStyleProperty, value);
		}

		public Brush RaisedPanBrush
		{
			get => (Brush)GetValue(RaisedPanBrushProperty);
			set => SetValue(RaisedPanBrushProperty, value);
		}
		public Brush LowerPanBrush
		{
			get => (Brush)GetValue(LowerPanBrushProperty);
			set => SetValue(LowerPanBrushProperty, value);
		}


		public Thickness LeftPanMargin
		{
			get => (Thickness)GetValue(LeftPanMarginProperty);
			set => SetValue(LeftPanMarginProperty, value);
		}
		public Thickness RightPanMargin
		{
			get => (Thickness)GetValue(RightPanMarginProperty);
			set => SetValue(RightPanMarginProperty, value);
		}
		#endregion

		#region Bogie
		public bool IsLeftBogieMotored
		{
			get => (bool)GetValue(IsLeftBogieMotoredProperty);
			set => SetValue(IsLeftBogieMotoredProperty, value);
		}
		public bool IsRightBogieMotored
		{
			get => (bool)GetValue(IsRightBogieMotoredProperty);
			set => SetValue(IsRightBogieMotoredProperty, value);
		}

		public Brush TrailerBogieBrush
		{
			get => (Brush)GetValue(TrailerBogieBrushProperty);
			set => SetValue(TrailerBogieBrushProperty, value);
		}
		public Brush MotoredBogieBrush
		{
			get => (Brush)GetValue(MotoredBogieBrushProperty);
			set => SetValue(MotoredBogieBrushProperty, value);
		}
		public Thickness LeftBogieMargin
		{
			get => (Thickness)GetValue(LeftBogieMarginProperty);
			set => SetValue(LeftBogieMarginProperty, value);
		}
		public Thickness RightBogieMargin
		{
			get => (Thickness)GetValue(RightBogieMarginProperty);
			set => SetValue(RightBogieMarginProperty, value);
		}

		public double BogieHeight
		{
			get => (double)GetValue(BogieHeightProperty);
			set => SetValue(BogieHeightProperty, value);
		}
		public double BogieWidth
		{
			get => (double)GetValue(BogieWidthProperty);
			set => SetValue(BogieWidthProperty, value);
		}
		#endregion

		#region Car Block
		public int CarNumber
		{
			get => (int)GetValue(CarNumberProperty);
			set => SetValue(CarNumberProperty, value);
		}
		public string CarNumberStr
		{
			get => (string)GetValue(CarNumberStrProperty);
			set => SetValue(CarNumberStrProperty, value);
		}

		public Brush CurrentCarNumberTextColor
		{
			get => (Brush)GetValue(CurrentCarNumberTextColorProperty);
			set => SetValue(CurrentCarNumberTextColorProperty, value);
		}
		public Brush CarNumberTextColor
		{
			get => (Brush)GetValue(CarNumberTextColorProperty);
			set => SetValue(CarNumberTextColorProperty, value);
		}
		public Brush CarNumberBrakeTextColor
		{
			get => (Brush)GetValue(CarNumberBrakeTextColorProperty);
			set => SetValue(CarNumberBrakeTextColorProperty, value);
		}
		public Brush CarNumberPowerTextColor
		{
			get => (Brush)GetValue(CarNumberPowerTextColorProperty);
			set => SetValue(CarNumberPowerTextColorProperty, value);
		}
		public Brush CarNumberRescueTextColor
		{
			get => (Brush)GetValue(CarNumberRescueTextColorProperty);
			set => SetValue(CarNumberRescueTextColorProperty, value);
		}
		public Brush CarInnerBrush
		{
			get => (Brush)GetValue(CarInnerBrushProperty);
			set => SetValue(CarInnerBrushProperty, value);
		}
		public Brush CarEdgeBrush
		{
			get => (Brush)GetValue(CarEdgeBrushProperty);
			set => SetValue(CarEdgeBrushProperty, value);
		}
		public Brush CarPowerBrush
		{
			get => (Brush)GetValue(CarPowerBrushProperty);
			set => SetValue(CarPowerBrushProperty, value);
		}
		public Brush CarBrakeBrush
		{
			get => (Brush)GetValue(CarBrakeBrushProperty);
			set => SetValue(CarBrakeBrushProperty, value);
		}

		public double CarBlockWidth
		{
			get => (double)GetValue(CarBlockWidthProperty);
			set => SetValue(CarBlockWidthProperty, value);
		}
		public double CarBlockCurrentHeight
		{
			get => (double)GetValue(CarBlockCurrentHeightProperty);
			set => SetValue(CarBlockCurrentHeightProperty, value);
		}
		public double CarBlockNormalHeight
		{
			get => (double)GetValue(CarBlockNormalHeightProperty);
			set => SetValue(CarBlockNormalHeightProperty, value);
		}
		public double CarBlockDoubleDeckerHeight
		{
			get => (double)GetValue(CarBlockDoubleDeckerHeightProperty);
			set => SetValue(CarBlockDoubleDeckerHeightProperty, value);
		}

		public bool IsLeftEdgeHEAD
		{
			get => (bool)GetValue(IsLeftEdgeHEADProperty);
			set => SetValue(IsLeftEdgeHEADProperty, value);
		}
		public bool IsRightEdgeHEAD
		{
			get => (bool)GetValue(IsRightEdgeHEADProperty);
			set => SetValue(IsRightEdgeHEADProperty, value);
		}

		public MState MotorState
		{
			get => (MState)GetValue(MotorStateProperty);
			set => SetValue(MotorStateProperty, value);
		}
		public bool IsLeftEdgeRescueSWTripped
		{
			get => (bool)GetValue(IsLeftEdgeRescueSWTrippedProperty);
			set => SetValue(IsLeftEdgeRescueSWTrippedProperty, value);
		}
		public bool IsRightEdgeRescueSWTripped
		{
			get => (bool)GetValue(IsRightEdgeRescueSWTrippedProperty);
			set => SetValue(IsRightEdgeRescueSWTrippedProperty, value);
		}

		public bool IsDoubleDecker
		{
			get => (bool)GetValue(IsDoubleDeckerProperty);
			set => SetValue(IsDoubleDeckerProperty, value);
		}

		public Thickness CarBlockMargin
		{
			get => (Thickness)GetValue(CarBlockMarginProperty);
			set => SetValue(CarBlockMarginProperty, value);
		}
		#endregion

		#endregion Properties

		const int BytesPerPixel = 4;
		static readonly Thickness CarBlock_UsualMargin = new Thickness(0);
		static readonly Thickness CarBlock_DDMargin = new Thickness(0, 0, 0, -5);
		static readonly byte[] Transparent = { 0x00, 0x00, 0x00, 0x00 };

		enum CarColorTypes
		{
			None,
			Transparent,
			EdgeLine,
			InnerNormal,
			InnerPower,
			InnerBrake,
			White
		};
		private WriteableBitmap SetCarBitmap(WriteableBitmap wb = null)
		{
			int w = (int)CarBlockWidth;
			int h = (int)(IsDoubleDecker ? CarBlockDoubleDeckerHeight : CarBlockNormalHeight);

			byte[,] ColTypes = new byte[h, w];
			#region 事前準備 描画内容の計算

			//最初に描画エリアいっぱいいっぱいに車体描画
			Parallel.For(0, h, (y) =>
			{
				byte? toDraw = ((y == 0) || (y == (h - 1))) ? (byte?)CarBitmaps.Edge : null;//上端と下端は常にEdge
				for (int x = 0; x < w; x++)
					ColTypes[y, x] = toDraw ?? (((x == 0) || (x == (w - 1))) ? CarBitmaps.Edge : CarBitmaps.Inner);//上端下端でなければ, Edge
			});

			//次にダブルデッカーオプション適用
			if (IsDoubleDecker)
			{
				//左上
				CarBitmaps.SetPixels(CarBitmaps.DD_LU, CarBitmaps.DD_U_HEIGHT, CarBitmaps.DD_U_WIDTH, ref ColTypes, 0, 0);

				//右上
				CarBitmaps.SetPixels(CarBitmaps.DD_RU, CarBitmaps.DD_U_HEIGHT, CarBitmaps.DD_U_WIDTH, ref ColTypes, w - CarBitmaps.DD_U_WIDTH, 0);

				//左下
				CarBitmaps.SetPixels(CarBitmaps.DD_LL, CarBitmaps.DD_L_HEIGHT, CarBitmaps.DD_L_WIDTH, ref ColTypes, 0, h - CarBitmaps.DD_L_HEIGHT);

				//右下
				CarBitmaps.SetPixels(CarBitmaps.DD_RL, CarBitmaps.DD_L_HEIGHT, CarBitmaps.DD_L_WIDTH, ref ColTypes, w - CarBitmaps.DD_L_WIDTH, h - CarBitmaps.DD_L_HEIGHT);

			}

			//最後にHEADオプション適用
			if (IsLeftEdgeHEAD)
				CarBitmaps.SetPixels(CarBitmaps.HEAD_LEFT, CarBitmaps.HEAD_HEIGHT, CarBitmaps.HEAD_WIDTH, ref ColTypes, 0, 0);
			if (IsRightEdgeHEAD)
				CarBitmaps.SetPixels(CarBitmaps.HEAD_RIGHT, CarBitmaps.HEAD_HEIGHT, CarBitmaps.HEAD_WIDTH, ref ColTypes, w - CarBitmaps.HEAD_WIDTH, 0);
			#endregion 事前準備 描画内容の計算

			#region 事前準備 描画色の取得
			byte[] Inner = MotorState switch
			{
				MState.None => UsefulFuncs.GetPixels(CarInnerBrush),
				MState.Accel => UsefulFuncs.GetPixels(CarPowerBrush),
				MState.Brake => UsefulFuncs.GetPixels(CarBrakeBrush),
				_ => Transparent
			};
			byte[] Edge = UsefulFuncs.GetPixels(CarEdgeBrush);
			#endregion 事前準備 描画色の取得

			#region 描画内容配列の計算
			byte[] pxArr = new byte[w * h * BytesPerPixel];

			Parallel.For(0, h * w, (i) =>
					Array.Copy(
						ColTypes[i / w, i % w] switch
						{
							CarBitmaps.None => Transparent,
							CarBitmaps.Inner => Inner,
							CarBitmaps.Edge => Edge,
							_ => Transparent
						},
						0, pxArr, i * BytesPerPixel, BytesPerPixel)
					);

			#endregion 描画内容配列の計算

			#region WriteableBitmapへの描画
			if (wb == null || (wb.PixelWidth != (w + 2) || wb.PixelHeight != (h + 2)))//描画先が存在しないか, あるいは描画領域が要求サイズと異なる場合
				wb = new WriteableBitmap(w + 2, h + 2, 96, 96, PixelFormats.Pbgra32, null);//再生成を行う.


			wb.Lock();
			try
			{
				wb.WritePixels(new Int32Rect(1, 1, w, h), pxArr, w * BytesPerPixel, 0);
			}
			finally
			{
				wb.Unlock();
			}
			#endregion
			return wb;
		}

		#region Car Bitmaps
		static public class CarBitmaps
		{
			public const byte None = n;
			public const byte Edge = e;
			public const byte Inner = i;
			public const byte n = 0;
			public const byte e = 1;
			public const byte i = 2;

			#region Array Size
			public const int HEAD_HEIGHT = 5;
			public const int HEAD_WIDTH = 12;

			public const int DD_U_HEIGHT = 2;
			public const int DD_U_WIDTH = 2;

			public const int DD_L_HEIGHT = 4;
			public const int DD_L_WIDTH = 10;
			#endregion Array Size

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			static public byte GetPixelType(in byte[,] ba, in int X, in int Y) => ba[Y, X];

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			static public void SetPixels(in byte[,] srcArr, in int SrcHeight, in int SrcWidth,  ref byte[,] dstArr, in int DstPosX, in int DstPosY)
			{
				for (int y = 0; y < SrcHeight; y++)
					for (int x = 0; x < SrcWidth; x++)
						dstArr[y + DstPosY, x + DstPosX] = srcArr[y, x];
			}

			static public byte[,] HEAD_LEFT = new byte[HEAD_HEIGHT, HEAD_WIDTH]
			{
				{ n, n, n, n, n, n, n, n, n, n, n, n },
				{ n, n, n, n, n, n, n, n, n, e, e, e },
				{ n, n, n, n, n, n, e, e, e, i, i, i },
				{ n, n, n, e, e, e, i, i, i, i, i, i },
				{ e, e, e, i, i, i, i, i, i, i, i, i }
			};

			static public byte[,] HEAD_RIGHT = new byte[HEAD_HEIGHT, HEAD_WIDTH]
			{
				{ n, n, n, n, n, n, n, n, n, n, n, n },
				{ e, e, e, n, n, n, n, n, n, n, n, n },
				{ i, i, i, e, e, e, n, n, n, n, n, n },
				{ i, i, i, i, i, i, e, e, e, n, n, n },
				{ i, i, i, i, i, i, i, i, i, e, e, e }
			};

			static public byte[,] DD_LU = new byte[DD_U_HEIGHT, DD_U_WIDTH]
			{
				{ n, n },
				{ n, e }
			};
			static public byte[,] DD_RU = new byte[DD_U_HEIGHT, DD_U_WIDTH]
			{
				{ n, n },
				{ e, n }
			};

			static public byte[,] DD_LL = new byte[DD_L_HEIGHT, DD_L_WIDTH]
			{
				{ e, e, e, e, e, e, e, e, i, i },
				{ n, n, n, n, n, n, n, n, e, i },
				{ n, n, n, n, n, n, n, n, n, e },
				{ n, n, n, n, n, n, n, n, n, n }
			};
			static public byte[,] DD_RL = new byte[DD_L_HEIGHT, DD_L_WIDTH]
			{
				{ i, i, e, e, e, e, e, e, e, e },
				{ i, e, n, n, n, n, n, n, n, n },
				{ e, n, n, n, n, n, n, n, n, n },
				{ n, n, n, n, n, n, n, n, n, n }
			};
		}
		#endregion
	}
}
