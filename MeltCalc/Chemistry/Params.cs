namespace MeltCalc.Chemistry
{
	public abstract class Ћом–азмерный
	{
		/// <summary>
		/// Ќизкоуглеродный лом.
		/// </summary>
		public Ћом Ќизко”глеродный { get; set; }

		/// <summary>
		/// —реднеуглеродный.
		/// </summary>
		public Ћом —редне”глеродный { get; set; }

		/// <summary>
		/// ¬ысокоуглеродный.
		/// </summary>
		public Ћом ¬ысоко”глеродный { get; set; }
	}

	public class Ћомћелкий : Ћом–азмерный
	{
		public Ћомћелкий()
		{
			Ќизко”глеродный = new Ћом(Ћом.RowIndex.LowSmall);
			—редне”глеродный = new Ћом(Ћом.RowIndex.MidSmall);
			¬ысоко”глеродный = new Ћом(Ћом.RowIndex.HighSmall);
		}
	}

	public class Ћом—редний : Ћом–азмерный
	{
		public Ћом—редний()
		{
			Ќизко”глеродный = new Ћом(Ћом.RowIndex.LowMed);
			—редне”глеродный = new Ћом(Ћом.RowIndex.MidMed);
			¬ысоко”глеродный = new Ћом(Ћом.RowIndex.HighMed);
		}
	}

	public class Ћом рупный : Ћом–азмерный
	{
		public Ћом рупный()
		{
			Ќизко”глеродный = new Ћом(Ћом.RowIndex.LowBig);
			—редне”глеродный = new Ћом(Ћом.RowIndex.MidBig);
			¬ысоко”глеродный = new Ћом(Ћом.RowIndex.HighBig);
		}
	}

	public static class Params
	{
		public static double alfaFe, L, StAndShlLoss, TAUprost, TAUprostREAL, TeplFutLoss, TAPtime, Lp, Tog;

		public static int SelectedPlant,
		                  SelectedAdaptedPlant,
		                  FutTypeSelected,
		                  LomTypeSelected,
		                  AirTemp,
		                  FutDurability,
		                  BlowingTime;

		// TODO:
		public static int Round;
		// TODO:
		public static string InputForm;
		// TODO:
		public static bool IsDuplex;

		static Params()
		{
			Ћомћелкий = new Ћомћелкий();
			Ћом—редний = new Ћом—редний();
			Ћом рупный = new Ћом рупный();
		}

		// cmdLastEstimate не реализован.

		public static Ћомћелкий Ћомћелкий { get; set; }
		public static Ћом—редний Ћом—редний { get; set; }
		public static Ћом рупный Ћом рупный { get; set; }
	}
}