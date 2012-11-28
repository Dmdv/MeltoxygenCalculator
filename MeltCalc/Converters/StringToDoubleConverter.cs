using System;
using System.Globalization;
using System.Windows.Data;

namespace MeltCalc.Converters
{
	public class StringToDoubleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == null ? string.Empty : value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return 0.0d;
			var val = value as string;
			if (string.IsNullOrEmpty(val)) return 0.0;

			return null;
		}
	}
}