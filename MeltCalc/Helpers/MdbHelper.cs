using System;
using System.Collections.Generic;
using System.Windows.Controls;
using MeltCalc.Providers;

namespace MeltCalc.Helpers
{
	public static class MdbHelper
	{
		public static void FillBoxes(this MdbReader reader, string tableName, int rowindex, IList<TextBox> boxes,
		                             int shift = 0)
		{
			var values = reader.Reader.SelectRowRange(tableName, rowindex);
			if (boxes.Count + shift > values.Length)
			{
				throw new Exception("shift");
			}
			for (var idx = 0; idx < boxes.Count; idx++)
			{
				boxes[idx].Text = values[idx + shift];
			}
		}
	}
}