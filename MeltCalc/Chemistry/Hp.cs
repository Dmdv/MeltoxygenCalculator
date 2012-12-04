namespace MeltCalc.Chemistry
{
	//Стандартные изменения энтальпий реакций окисляющегося компонента
	public static class Hp
	{
		public static double dHmshl { get; set; }
		public static double dHshl { get; set; }
		public static double dHst { get; set; }
		public static double dHchug { get; set; }
		public static double dHlom { get; set; }
		public static double dHostshl { get; set; }

		//Добавил еще вот эти энтальпии

		public static double dHizkPlavl { get; set; }
		public static double dHlomPlavl { get; set; }
		public static double dHchugPlavl { get; set; }

		public static double dHsio2_2caosio2 { get; set; }
		public static double dHp2o5_3caop2o5 { get; set; }

		public static double dHsi_O2_mol { get; set; }
		public static double dHmn_O2_mol { get; set; }
		public static double dHp_O2_mol { get; set; }
		public static double dHfe_O2_mol { get; set; }
		public static double dHfe_fe2o3_o2_mol { get; set; }
		public static double dHc_O2_mol { get; set; }
		public static double dHco_co2_mol { get; set; }
	}
}