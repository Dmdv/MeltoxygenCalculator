using System;

namespace MeltCalc.Model
{
	public static class Mapper
	{
		public static string ToName(this Materials materials)
		{
			switch (materials)
			{
				case Materials.ИзвестковоМагнитныйФлюс:
					return "ИМФ";
				case Materials.СыройДоломит:
					return "Вл доломит";
				case Materials.ПлавиковыйШпат:
					return "П шпат";
				default:
					return materials.ToString();
			}
		}

		public static string ToDatabaseName(this Materials materials)
		{
			switch (materials)
			{
				case Materials.ИзвестковоМагнитныйФлюс:
					return "IMF";
				case Materials.СыройДоломит:
					return "VlDol";
				case Materials.ПлавиковыйШпат:
					return "Shp";
				case Materials.Кокс:
					return "Koks";
				case Materials.Известь:
					return "Izv";
				case Materials.Известняк:
					return "Izk";
				case Materials.Доломит:
					return "Dol";
				case Materials.Окалина:
					return "Okal";
				case Materials.Агломерат:
					return "Agl";
				case Materials.Окатыши:
					return "Okat";
				case Materials.Песок:
					return "Pes";
				case Materials.Руда:
					return "Ruda";
				default:
					throw new Exception("Not found material");
			}
		}
	}
}