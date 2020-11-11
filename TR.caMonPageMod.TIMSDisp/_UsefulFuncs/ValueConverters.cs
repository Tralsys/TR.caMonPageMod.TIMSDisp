using System;
using System.CodeDom;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace TR.caMonPageMod.TIMSDisp._UsefulFuncs
{
	//ref : https://blogs.itmedia.co.jp/mohno/2013/12/xaml15-c9fe.html
	#region Simple Convert
	public class BoolToVisibility : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> ((bool?)value ?? false) ? Visibility.Visible : Visibility.Collapsed;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> (Visibility)value == Visibility.Visible;
	}

	public class IsDoorClosedToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> (bool?)value switch
			{
				true => "閉",
				false => "開",
				_ => "―"
			};

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> value as string switch
			{
				"開" => false,
				"閉" => true,
				_ => false
			};
	}

	public class ValueToString<T> : IValueConverter where T : struct
	{
		public string Format { get; set; } = string.Empty;
		public int PaddingL { get; set; } = 0;
		public int PaddingR { get; set; } = 0;

		protected Func<object, string, string> toString { get; } = null;
		protected Func<string, object> parse { get; } = null;
		public ValueToString()
		{
			//ref : https://qiita.com/Zuishin/items/61fc8807d027d5cea329
			//ref : https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/switch-expression
			toString = default(T) switch
			{
				int => (v, f) => ((int)v).ToString(f),
				uint => (v, f) => ((uint)v).ToString(f),

				short => (v, f) => ((short)v).ToString(f),
				ushort => (v, f) => ((ushort)v).ToString(f),

				long => (v, f) => ((long)v).ToString(f),
				ulong => (v, f) => ((ulong)v).ToString(f),

				byte => (v, f) => ((byte)v).ToString(f),

				float => (v, f) => ((float)v).ToString(f),
				double => (v, f) => ((double)v).ToString(f),
				decimal => (v, f) => ((decimal)v).ToString(f),

				_ => throw new NotSupportedException("The Type \"" + typeof(T).ToString() + "\" is not supported")
			};

			parse = default(T) switch
			{
				int => (s) => int.Parse(s),
				uint => (s) => uint.Parse(s),

				short => (s) => short.Parse(s),
				ushort => (s) => ushort.Parse(s),

				long => (s) => long.Parse(s),
				ulong => (s) => ulong.Parse(s),

				byte => (s) => byte.Parse(s),

				float => (s) => float.Parse(s),
				double => (s) => double.Parse(s),
				decimal => (s) => decimal.Parse(s),

				_ => throw new NotSupportedException("The Type \"" + typeof(T).ToString() + "\" is not supported")
			};


		}
		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> toString?.Invoke(value, Format).PadLeft(PaddingL).PadRight(PaddingR);//左に空白を開けてから右に空白を開ける

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> parse?.Invoke(value as string);
	}

	public class IntToString : ValueToString<int> { }

	public class IntToWideString : ValueToString<int>
	{
		static private Char_WideNarrowSetting cwns = new Char_WideNarrowSetting();
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> UsefulFuncs.WideNarrowConv(cwns, base.Convert(value, targetType, parameter, culture));
	}

	public class DoubleToString : ValueToString<double> { }

	public class CollapsedWhenIntN : IValueConverter
	{
		public int CollapsedWhen { get; set; } = 0;
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> (int)value == CollapsedWhen ? Visibility.Collapsed : Visibility.Visible;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
		
	}
	public class CollapsedWhenNULL : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> value == null ? Visibility.Collapsed : Visibility.Visible;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
		
	}
	#endregion

	#region Value Plus/Minus
	public class DoublePlusN : IValueConverter
	{
		public double ValueToPlus { get; set; } = 0;
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> (double)(value ?? 0.0) + ValueToPlus;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> (double)(value ?? ValueToPlus) - ValueToPlus;
	}
	public class DoubleMinusN : IValueConverter
	{
		public double ValueToMinus { get; set; } = 0;
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> (double)(value ?? ValueToMinus) - ValueToMinus;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> (double)(value ?? 0.0) + ValueToMinus;
	}
	#endregion Value Plus/Minus
}
