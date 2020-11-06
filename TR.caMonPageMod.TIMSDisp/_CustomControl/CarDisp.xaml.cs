using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using TR.caMonPageMod.TIMSDisp._UsefulFuncs;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class CarDisp : Control
	{
		public static readonly DependencyProperty CarNumberProperty = DependencyProperty.Register("CarNumber", typeof(int), typeof(CarDisp), new PropertyMetadata(1, CarNumberPropChanged));
		public static readonly DependencyProperty CarNumberStrProperty = DependencyProperty.Register("CarNumberStr", typeof(string), typeof(CarDisp), new PropertyMetadata("１"));

		public static readonly DependencyProperty CarInnerBrushProperty = DependencyProperty.Register("CarInnerBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Black));

		#region Door Block
		public static readonly DependencyProperty HasDoorProperty = DependencyProperty.Register("HasDoor", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));
		public static readonly DependencyProperty IsDoorClosedProperty = DependencyProperty.Register("IsDoorClosed", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));

		public static readonly DependencyProperty CurrentDoorBrushProperty = DependencyProperty.Register("CurrentDoorBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Red));
		public static readonly DependencyProperty CurrentDoorTextColorProperty = DependencyProperty.Register("CurrentDoorTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Red));

		public static readonly DependencyProperty DoorOpenBrushProperty = DependencyProperty.Register("DoorOpenBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Red));
		public static readonly DependencyProperty DoorCloseBrushProperty = DependencyProperty.Register("DoorCloseBrush", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Transparent));
		public static readonly DependencyProperty DoorOpenTextColorProperty = DependencyProperty.Register("DoorOpenTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Red));
		public static readonly DependencyProperty DoorCloseTextColorProperty = DependencyProperty.Register("DoorCloseTextColor", typeof(Brush), typeof(CarDisp), new PropertyMetadata(Brushes.Transparent));
		#endregion Door Block

		public static readonly DependencyProperty IsLeftEdgeHEADProperty = DependencyProperty.Register("IsLeftEdgeHEAD", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));
		public static readonly DependencyProperty IsRightEdgeHEADProperty = DependencyProperty.Register("IsRightEdgeHEAD", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));

		public static readonly DependencyProperty LeftPanRaisedProperty = DependencyProperty.Register("LeftPanRaised", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));
		public static readonly DependencyProperty RightPanRaisedProperty = DependencyProperty.Register("RightPanRaised", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));

		public static readonly DependencyProperty LeftPanStyleProperty = DependencyProperty.Register("LeftPanStyle", typeof(PantographStyle), typeof(CarDisp), new PropertyMetadata(PantographStyle.SingleArm_LeftJoint));
		public static readonly DependencyProperty RightPanStyleProperty = DependencyProperty.Register("RightPanStyle", typeof(PantographStyle), typeof(CarDisp), new PropertyMetadata(PantographStyle.SingleArm_RightJoint));

		public static readonly DependencyProperty IsLeftBogieMotoredProperty = DependencyProperty.Register("IsLeftBogieMotored", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));
		public static readonly DependencyProperty IsRightBogieMotoredProperty = DependencyProperty.Register("IsRightBogieMotored", typeof(bool), typeof(CarDisp), new PropertyMetadata(true));

		public static readonly DependencyProperty MotorStateProperty = DependencyProperty.Register("RightBogieMotorState", typeof(MState), typeof(CarDisp), new PropertyMetadata(MState.Accel));

		public static readonly DependencyProperty IsLeftEdgeRescueSWTrippedProperty = DependencyProperty.Register("IsLeftEdgeRescueSWTripped", typeof(bool), typeof(CarDisp), new PropertyMetadata(false));
		public static readonly DependencyProperty IsRightEdgeRescueSWTrippedProperty = DependencyProperty.Register("IsRightEdgeRescueSWTripped", typeof(bool), typeof(CarDisp), new PropertyMetadata(false));

		public static readonly DependencyProperty IsDoubleDeckerProperty = DependencyProperty.Register("IsDoubleDecker", typeof(bool), typeof(CarDisp), new PropertyMetadata(false));

		private static void CarNumberPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			int cn = (int)e.NewValue;
			(d as CarDisp).CarNumberStr = (string)UsefulFuncs.WideNarrowConv(new Char_WideNarrowSetting(cn >= 10), cn.ToString());//10以上(2桁)なら半角, 1桁なら全角
		}

		static CarDisp() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CarDisp), new FrameworkPropertyMetadata(typeof(CarDisp)));

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
		#endregion Properties
	}
}
