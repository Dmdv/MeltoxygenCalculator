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
		public static Шлак Шлак { get; set; }
		public static ОставленныйШлак ОставленныйШлак { get; set; }
		public static МиксерныйШлак МиксерныйШлак { get; set; }
		public static Чугун Чугун { get; set; }
		public static Сталь Сталь { get; set; }
		public static Лом Лом { get; set; }
		public static ЛомНизкий ЛомНизкий { get; set; }
		public static ЛомСредний ЛомСредний { get; set; }
		public static ЛомВысокий ЛомВысокий { get; set; }
		public static Футеровка Футеровка { get; set; }
		public static Дутье Дутье { get; set; }
		public static Имф Имф { get; set; }
		public static Кокс Кокс { get; set; }
		public static Песок Песок { get; set; }
		public static Руда Руда { get; set; }
		public static Окатыши Окатыши { get; set; }
		public static Ферросплав Ферросплав { get; set; }
		public static Агломерат Агломерат { get; set; }
		public static Доломит Доломит { get; set; }
		public static ВлажныйДоломит ВлажныйДоломит { get; set; }
		public static Packets Pack { get; set; }
	}
}