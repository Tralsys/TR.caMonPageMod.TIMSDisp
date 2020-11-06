using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TR.caMonPageMod.TIMSDisp._UsefulFuncs
{
	//ref : https://blogs.itmedia.co.jp/mohno/2013/12/xaml15-c9fe.html

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
}
