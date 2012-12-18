namespace MeltCalc.Helpers
{
	public static class StringExtensions
	{
		public static int ToIntSafe(this string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return 0;
			int outValue;
			return int.TryParse(value, out outValue) ? outValue : 0;
		}

		public static float ToFloatSafe(this string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return 0;
			float outValue;
			return float.TryParse(value, out outValue) ? outValue : 0;
		}
	}
}