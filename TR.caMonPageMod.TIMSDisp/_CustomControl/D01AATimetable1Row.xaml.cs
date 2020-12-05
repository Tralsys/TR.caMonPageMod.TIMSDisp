using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TR.caMonPageMod.TIMSDisp._CustomControl
{
	public class D01AATimetable1Row : Control
	{

		static public readonly DependencyProperty TextColorProperty = DependencyProperty.Register(nameof(TextColor), typeof(Brush), typeof(D01AATimetable1Row), new PropertyMetadata(Brushes.White));
		public Brush TextColor
		{
			get => (Brush)GetValue(TextColorProperty);
			set => SetValue(TextColorProperty, value);
		}

		#region Runtime
		static public readonly DependencyProperty IsRuntimeVisibleProperty = DependencyProperty.Register(nameof(IsRuntimeVisible), typeof(bool), typeof(Control), new PropertyMetadata(true));
		public bool IsRuntimeVisible
		{
			get => (bool)GetValue(IsRuntimeVisibleProperty);
			set => SetValue(IsRuntimeVisibleProperty, value);
		}

		static public readonly DependencyProperty RuntimeMMProperty = DependencyProperty.Register(nameof(RuntimeMM), typeof(int), typeof(Control), new PropertyMetadata(0));
		public int RuntimeMM
		{
			get => (int)GetValue(RuntimeMMProperty);
			set => SetValue(RuntimeMMProperty, value);
		}

		static public readonly DependencyProperty RuntimeSSProperty = DependencyProperty.Register(nameof(RuntimeSS), typeof(int), typeof(Control), new PropertyMetadata(0));
		public int RuntimeSS
		{
			get => (int)GetValue(RuntimeSSProperty);
			set => SetValue(RuntimeSSProperty, value);
		}

		#endregion

		#region Station Name
		static public readonly DependencyProperty StaNameProperty = DependencyProperty.Register(nameof(StaName), typeof(string), typeof(D01AATimetable1Row),
			new PropertyMetadata("初期駅名", null, (d, e) =>
			{
				if(e is string s)
				{
					if (s.Length == 2)
						return (e as string).Insert(1, "\u3000");//駅名が2文字の場合は, 間に全角スペースを挟む
																										 //全角スペース ref : https://ufcpp.net/study/csharp/misc_unicode.html

					if (s.Length > 4)
						return s.Substring(0, 4);//4文字よりも長いなら, 4文字まで短くする
				}

				return e;
			}));
		public string StaName
		{
			get => (string)GetValue(StaNameProperty);
			set => SetValue(StaNameProperty, value);
		}

		static public readonly DependencyProperty IsThisStaOpStopProperty = DependencyProperty.Register(nameof(IsThisStaOpStop), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(false));
		public bool IsThisStaOpStop
		{
			get => (bool)GetValue(IsThisStaOpStopProperty);
			set => SetValue(IsThisStaOpStopProperty, value);
		}
		#endregion

		#region ArrTime
		static public readonly DependencyProperty IsArrTimeVisibleProperty = DependencyProperty.Register(nameof(IsArrTimeVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsArrTimeVisible
		{
			get => (bool)GetValue(IsArrTimeVisibleProperty);
			set => SetValue(IsArrTimeVisibleProperty, value);
		}

		static public readonly DependencyProperty IsArrTimeHHVisibleProperty = DependencyProperty.Register(nameof(IsArrTimeHHVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsArrTimeHHVisible
		{
			get => (bool)GetValue(IsArrTimeHHVisibleProperty);
			set => SetValue(IsArrTimeHHVisibleProperty, value);
		}

		static public readonly DependencyProperty IsArrTimeMMVisibleProperty = DependencyProperty.Register(nameof(IsArrTimeMMVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsArrTimeMMVisible
		{
			get => (bool)GetValue(IsArrTimeMMVisibleProperty);
			set => SetValue(IsArrTimeMMVisibleProperty, value);
		}

		static public readonly DependencyProperty IsArrTimeSSVisibleProperty = DependencyProperty.Register(nameof(IsArrTimeSSVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsArrTimeSSVisible
		{
			get => (bool)GetValue(IsArrTimeSSVisibleProperty);
			set => SetValue(IsArrTimeSSVisibleProperty, value);
		}

		static public readonly DependencyProperty IsArrTimeSepVisibleProperty = DependencyProperty.Register(nameof(IsArrTimeSepVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsArrTimeSepVisible
		{
			get => (bool)GetValue(IsArrTimeSepVisibleProperty);
			set => SetValue(IsArrTimeSepVisibleProperty, value);
		}


		static public readonly DependencyProperty ArrTimeHHProperty = DependencyProperty.Register(nameof(ArrTimeHH), typeof(int), typeof(D01AATimetable1Row), new PropertyMetadata(0));
		public int ArrTimeHH
		{
			get => (int)GetValue(ArrTimeHHProperty);
			set => SetValue(ArrTimeHHProperty, value);
		}

		static public readonly DependencyProperty ArrTimeMMProperty = DependencyProperty.Register(nameof(ArrTimeMM), typeof(int), typeof(D01AATimetable1Row), new PropertyMetadata(0));
		public int ArrTimeMM
		{
			get => (int)GetValue(ArrTimeMMProperty);
			set => SetValue(ArrTimeMMProperty, value);
		}

		static public readonly DependencyProperty ArrTimeSSProperty = DependencyProperty.Register(nameof(ArrTimeSS), typeof(int), typeof(D01AATimetable1Row), new PropertyMetadata(0));
		public int ArrTimeSS
		{
			get => (int)GetValue(ArrTimeSSProperty);
			set => SetValue(ArrTimeSSProperty, value);
		}

		static public readonly DependencyProperty ArrTimeSepProperty = DependencyProperty.Register(nameof(ArrTimeSep), typeof(string), typeof(D01AATimetable1Row), new PropertyMetadata(":"));
		public string ArrTimeSep
		{
			get => (string)GetValue(ArrTimeSepProperty);
			set => SetValue(ArrTimeSepProperty, value);
		}

		#endregion

		#region DepTime
		static public readonly DependencyProperty IsDepTimeVisibleProperty = DependencyProperty.Register(nameof(IsDepTimeVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsDepTimeVisible
		{
			get => (bool)GetValue(IsDepTimeVisibleProperty);
			set => SetValue(IsDepTimeVisibleProperty, value);
		}

		static public readonly DependencyProperty IsDepTimeHHVisibleProperty = DependencyProperty.Register(nameof(IsDepTimeHHVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsDepTimeHHVisible
		{
			get => (bool)GetValue(IsDepTimeHHVisibleProperty);
			set => SetValue(IsDepTimeHHVisibleProperty, value);
		}

		static public readonly DependencyProperty IsDepTimeMMVisibleProperty = DependencyProperty.Register(nameof(IsDepTimeMMVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsDepTimeMMVisible
		{
			get => (bool)GetValue(IsDepTimeMMVisibleProperty);
			set => SetValue(IsDepTimeMMVisibleProperty, value);
		}

		static public readonly DependencyProperty IsDepTimeSSVisibleProperty = DependencyProperty.Register(nameof(IsDepTimeSSVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsDepTimeSSVisible
		{
			get => (bool)GetValue(IsDepTimeSSVisibleProperty);
			set => SetValue(IsDepTimeSSVisibleProperty, value);
		}

		static public readonly DependencyProperty IsDepTimeSepVisibleProperty = DependencyProperty.Register(nameof(IsDepTimeSepVisible), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsDepTimeSepVisible
		{
			get => (bool)GetValue(IsDepTimeSepVisibleProperty);
			set => SetValue(IsDepTimeSepVisibleProperty, value);
		}


		static public readonly DependencyProperty DepTimeHHProperty = DependencyProperty.Register(nameof(DepTimeHH), typeof(int), typeof(D01AATimetable1Row), new PropertyMetadata(0));
		public int DepTimeHH
		{
			get => (int)GetValue(DepTimeHHProperty);
			set => SetValue(DepTimeHHProperty, value);
		}

		static public readonly DependencyProperty DepTimeMMProperty = DependencyProperty.Register(nameof(DepTimeMM), typeof(int), typeof(D01AATimetable1Row), new PropertyMetadata(0));
		public int DepTimeMM
		{
			get => (int)GetValue(DepTimeMMProperty);
			set => SetValue(DepTimeMMProperty, value);
		}

		static public readonly DependencyProperty DepTimeSSProperty = DependencyProperty.Register(nameof(DepTimeSS), typeof(int), typeof(D01AATimetable1Row), new PropertyMetadata(0));
		public int DepTimeSS
		{
			get => (int)GetValue(DepTimeSSProperty);
			set => SetValue(DepTimeSSProperty, value);
		}

		static public readonly DependencyProperty DepTimeSepProperty = DependencyProperty.Register(nameof(DepTimeSep), typeof(string), typeof(D01AATimetable1Row), new PropertyMetadata(":"));
		public string DepTimeSep
		{
			get => (string)GetValue(DepTimeSepProperty);
			set => SetValue(DepTimeSepProperty, value);
		}

		#endregion

		#region Track Number
		static public readonly DependencyProperty TrackNumProperty = DependencyProperty.Register(nameof(TrackNum), typeof(string), typeof(D01AATimetable1Row), new PropertyMetadata("初期２"));
		public string TrackNum
		{
			get => (string)GetValue(TrackNumProperty);
			set => SetValue(TrackNumProperty, value);
		}
		#endregion

		#region Limit
		static public readonly DependencyProperty RunInLimitProperty = DependencyProperty.Register(nameof(RunInLimit), typeof(int), typeof(D01AATimetable1Row), new PropertyMetadata(123));
		public int RunInLimit
		{
			get => (int)GetValue(RunInLimitProperty);
			set => SetValue(RunInLimitProperty, value);
		}

		static public readonly DependencyProperty RunOutLimitProperty = DependencyProperty.Register(nameof(RunOutLimit), typeof(int), typeof(D01AATimetable1Row), new PropertyMetadata(45));
		public int RunOutLimit
		{
			get => (int)GetValue(RunOutLimitProperty);
			set => SetValue(RunOutLimitProperty, value);
		}


		static public readonly DependencyProperty IsRunInLimitEnabledProperty = DependencyProperty.Register(nameof(IsRunInLimitEnabled), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsRunInLimitEnabled
		{
			get => (bool)GetValue(IsRunInLimitEnabledProperty);
			set => SetValue(IsRunInLimitEnabledProperty, value);
		}

		static public readonly DependencyProperty IsRunOutLimitEnabledProperty = DependencyProperty.Register(nameof(IsRunOutLimitEnabled), typeof(bool), typeof(D01AATimetable1Row), new PropertyMetadata(true));
		public bool IsRunOutLimitEnabled
		{
			get => (bool)GetValue(IsRunOutLimitEnabledProperty);
			set => SetValue(IsRunOutLimitEnabledProperty, value);
		}
		#endregion

		static D01AATimetable1Row() => DefaultStyleKeyProperty.OverrideMetadata(typeof(D01AATimetable1Row), new FrameworkPropertyMetadata(typeof(D01AATimetable1Row)));
		

	}
}
