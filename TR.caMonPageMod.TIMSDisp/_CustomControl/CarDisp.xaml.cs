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
		public static readonly DependencyProperty DoorOpenTextColorProperty = DependencyProperty.Register("DoorOpenTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black));
		public static readonly DependencyProperty DoorCloseTextColorProperty = DependencyProperty.Register("DoorCloseTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));

		public static readonly DependencyProperty DoorBlockWidthProperty = DependencyProperty.Register("DoorBlockWidth", typeof(double), typeof(CarDisp), new PropertyMetadata(45.0));
		public static readonly DependencyProperty DoorBlockHeightProperty = DependencyProperty.Register("DoorBlockHeight", typeof(double), typeof(CarDisp), new PropertyMetadata(22.0));
		#endregion Door Block

		#region Pantograph
		public static readonly DependencyProperty LeftPanRaisedProperty = DependencyProperty.Register("LeftPanRaised", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));
		public static readonly DependencyProperty RightPanRaisedProperty = DependencyProperty.Register("RightPanRaised", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));

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
		public static readonly DependencyProperty CarNumberTextColorProperty = DependencyProperty.Register("CarNumberTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.White));

		public static readonly DependencyProperty IsLeftEdgeHEADProperty = DependencyProperty.Register("IsLeftEdgeHEAD", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));
		public static readonly DependencyProperty IsRightEdgeHEADProperty = DependencyProperty.Register("IsRightEdgeHEAD", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));

		public static readonly DependencyProperty IsLeftEdgeRescueSWTrippedProperty = DependencyProperty.Register("IsLeftEdgeRescueSWTripped", typeof(bool), typeof(CarDisp), new PropertyMetadata(false));
		public static readonly DependencyProperty IsRightEdgeRescueSWTrippedProperty = DependencyProperty.Register("IsRightEdgeRescueSWTripped", typeof(bool), typeof(CarDisp), new PropertyMetadata(false));

		public static readonly DependencyProperty IsDoubleDeckerProperty = DependencyProperty.Register("IsDoubleDecker", typeof(bool), typeof(CarDisp),
			new PropertyMetadata(false, CarBlockReDraw));

		public static readonly DependencyProperty MotorStateProperty = DependencyProperty.Register("MotorState", typeof(MState), typeof(CarDisp), new PropertyMetadata(MState.Accel));

		public static readonly DependencyProperty CarInnerBrushProperty = DependencyProperty.Register("CarInnerBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black));
		public static readonly DependencyProperty CarPowerBrushProperty = DependencyProperty.Register("CarPowerBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black));
		public static readonly DependencyProperty CarBrakeBrushProperty = DependencyProperty.Register("CarBrakeBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black));

		public static readonly DependencyProperty CarBlockMarginProperty = DependencyProperty.Register("CarBlockMargin", typeof(Thickness), typeof(CarDisp), new PropertyMetadata(new Thickness(0)));

		private static void CarBlockReDraw(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is CarDisp cd)
			{
				//CarImgの更新
				cd.SetCarBitmap(cd.CarImg.Source as WriteableBitmap);
			}
		}
		private static void CarNumberPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			int cn = (int)e.NewValue;
			(d as CarDisp).CarNumberStr = (string)UsefulFuncs.WideNarrowConv(new Char_WideNarrowSetting(cn >= 10), cn.ToString());//10以上(2桁)なら半角, 1桁なら全角
		}
		#endregion
		#endregion DependencyProperties


		static CarDisp() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CarDisp), new FrameworkPropertyMetadata(typeof(CarDisp)));

		Image CarImg;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();


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

		public Brush CarNumberTextColor
		{
			get => (Brush)GetValue(CarNumberTextColorProperty);
			set => SetValue(CarNumberTextColorProperty, value);
		}
		public Brush CarInnerBrush
		{
			get => (Brush)GetValue(CarInnerBrushProperty);
			set => SetValue(CarInnerBrushProperty, value);
		}

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

		#endregion Properties
		
		const int BytesPerPixel = 4;
		const int CarHeight_BASE = 27;
		const int CarHeight_Normal = 20;
		const int CarMarginY_Normal = 2;

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
			int w = (int)(Width - 1);
			CarColorTypes[] ccta = new CarColorTypes[CarHeight_BASE * w];

			Parallel.For(0, ccta.Length, (i) => ccta[i] = CarColorTypes.Transparent);//色の初期化

			if (IsDoubleDecker)
			{

			}

			return null;
		}
	}
}
