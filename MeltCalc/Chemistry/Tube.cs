namespace MeltCalc.Chemistry
{
	public class Tube
	{
		public Tube()
		{
			Известняк = new Известняк();
			Известь = new Известь();
			Шпат = new Шпат();
			Окалина = new Окалина();
		}

		public static Известняк Известняк { get; set; }
		public static Известь Известь { get; set; }
		public static Шпат Шпат { get; set; }
		public static Окалина Окалина { get; set; }
	}
}