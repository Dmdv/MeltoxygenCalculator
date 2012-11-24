using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MeltCalc.Model;

namespace MeltCalc.Helpers
{
	public static class VisualHelper
	{
		public static T FindButton<T>(IEnumerable<T> coll, Materials material) where T : ContentControl
		{
			return coll.Single(x => x.Tag is Materials && (Materials) x.Tag == material);
		}

		public static Materials Material(this ContentControl button)
		{
			return (Materials) button.Tag;
		}

		public static ContentControl FindByMaterial(this IEnumerable<ContentControl> coll, Materials materials)
		{
			return coll.Single(x => x.Material() == materials);
		}

		public static void UpdateEnabled(IEnumerable<ContentControl> controls, ICollection<Materials> disabledList)
		{
			foreach (var button in controls.Where(control => disabledList.Contains(control.Material())))
			{
				button.IsEnabled = false;
			}
		}

		public static void UpdateVisibility(IEnumerable<ContentControl> controls, ICollection<Materials> visibleList)
		{
			foreach (var control in controls.Where(control => !visibleList.Contains(control.Material())))
			{
				control.Visibility = Visibility.Collapsed;
			}
		}
	}
}