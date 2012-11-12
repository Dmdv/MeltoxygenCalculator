using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;
using MeltCalc.Model;

namespace MeltCalc.Helpers
{
	public static class ButtonHelper
	{
		public static T FindButton<T>(IEnumerable<T> coll, Materials material) where T : ToggleButton
		{
			return coll.Single(x => x.Tag is Materials && (Materials)x.Tag == material);
		}

		public static Materials? Material(this ToggleButton button)
		{
			return button.Tag is Materials ? (Materials) button.Tag : (Materials?) null;
		}

		public static ToggleButton FindByMaterial(this IEnumerable<ToggleButton> coll, Materials materials)
		{
			return coll.Single(x => x.Material() == materials);
		}
	}
}
