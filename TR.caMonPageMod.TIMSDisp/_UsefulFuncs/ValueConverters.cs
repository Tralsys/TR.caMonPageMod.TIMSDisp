using System;
using System.Globalization;
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

	public class IntToString : IValueConverter
	{
		public string Format { get; set; } = "D";
		public int Padding { get; set; } = 0;
		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> ((int)value).ToString(Format).PadLeft(Padding);

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> string.IsNullOrWhiteSpace(value as string) ? 0 : int.Parse(value as string);
	}

	public class IntToWideString : IntToString
	{
		static private Char_WideNarrowSetting cwns = new Char_WideNarrowSetting();
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> UsefulFuncs.WideNarrowConv(cwns, base.Convert(value, targetType, parameter, culture));
	}

	public class CollapsedWhenIntN : IValueConverter
	{
		public int CollapsedWhen { get; set; } = 0;
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> (int)value == CollapsedWhen ? Visibility.Collapsed : Visibility.Visible;

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
