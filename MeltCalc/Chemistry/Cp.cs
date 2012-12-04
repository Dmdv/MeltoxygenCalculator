namespace MeltCalc.Chemistry
{
	// Переменные теплового баланса
	public static class Cp
	{
		public static double H2O { get; set; }
		public static double CO { get; set; }
		public static double CO2 { get; set; }
		public static double N2 { get; set; }
		public static double Ar { get; set; }
		public static double O2 { get; set; }
		public static double Dut { get; set; }
		public static double FeO { get; set; }
		public static double Fe { get; set; }
		public static double IMF { get; set; }

		public static double Alloy { get; set; }
		public static double izv { get; set; }
		public static double izk { get; set; }
		public static double koks { get; set; }
		public static double pes { get; set; }
		public static double ruda { get; set; }
		public static double shp { get; set; }
		public static double okal { get; set; }
		public static double okat { get; set; }
		public static double agl { get; set; }
		public static double dol { get; set; }
		public static double vldol { get; set; }

		//Добавил еще вот эти теплоемкости
		//CpChugLiquid, CpLomSolid, CpMet
		
		public static double ChugLiquid { get; set; }
		public static double LomSolid { get; set; }
		public static double Met { get; set; }

		//Что-то связанное с известью
		public static double Densing { get; set; }
		
		public static double DUT { get; set; }

	}
}