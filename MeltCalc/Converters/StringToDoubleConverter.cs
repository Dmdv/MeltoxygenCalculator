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
			if (value is double) return value;
			if (value is float) return value;
			if (value is int) return ConvertBack(value, targetType);
			if (value is string) return ConvetrFromString(value, targetType);
			return null;
		}

		private static object ConvertBack(object value, Type targetType)
		{
			if (targetType == typeof (Single))
			{
				return System.Convert.ToSingle(value);
			}

			return System.Convert.ToDouble(value);
		}

		private static object ConvetrFromString(object value, Type targetType)
		{
			if (value == null) return 0.0d;
			var val = value as string;
			if (string.IsNullOrEmpty(val)) return 0.0;

			double parsedValue;
			var result = double.TryParse(val, out parsedValue);
			return result ? parsedValue : 0.0;
		}
	}
}