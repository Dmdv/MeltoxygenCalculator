namespace MeltCalc.Chemistry
{
	public class Навеска
	{
		public double G;
		public double ALFA { get; set; }
	}

	public class Известь : Навеска
	{
		public double Al2O3 { get; set; }
		public double CaO { get; set; }
		public double H2O { get; set; }
		public double MgO { get; set; }
		public double P2O5 { get; set; }
		public double SiO2 { get; set; }
	}

	public class Известняк : Навеска
	{
		public double CO2 { get; set; }
		public double CaCO3 { get; set; }
		public double CaO { get; set; }
		public double H2O { get; set; }
		public double P2O5 { get; set; }
		public double SiO2 { get; set; }
	}

	public class Окалина : Навеска
	{
		public double Fe2O3 { get; set; }
		public double Fe3O4 { get; set; }
		public double FeO { get; set; }
		public double MgO { get; set; }
		public double MnO { get; set; }
		public double P { get; set; }
		public double SiO2 { get; set; }
	}

	public class Шпат : Навеска
	{
		public double CaF2 { get; set; }
		public double CaO { get; set; }
		public double SiO2 { get; set; }
	}

	public class Шлак : Навеска
	{
		public double Al2O3 { get; set; }
		public double B { get; set; }
		public double Bmax { get; set; }
		public double Bmin { get; set; }
		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double FeO { get; set; }
		public double MgO { get; set; }
		public double MnO { get; set; }
		public double P2O5 { get; set; }
		public double SiO2 { get; set; }
		public double TOTALFeO { get; set; }
		public double V2O5 { get; set; }
	}

	public class ОставленныйШлак : Навеска
	{
		public double Al2O3 { get; set; }
		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double FeO { get; set; }
		public double MgO { get; set; }
		public double MnO { get; set; }
		public double P2O5 { get; set; }
		public double SiO2 { get; set; }
	}

	public class МиксерныйШлак : Навеска
	{
		public double CaO { get; set; }
		public double SiO2 { get; set; }
		public double MnO { get; set; }
		public double MgO { get; set; }
		public double P2O5 { get; set; }
		public double FeO { get; set; }
		public double Fe2O3 { get; set; }
	}

	public class Чугун : Навеска
	{
		public double C { get; set; }
		public double Mn { get; set; }
		public double P { get; set; }
		public double S { get; set; }
		public double Si { get; set; }
		public double T { get; set; }
	}

	public class Сталь : Навеска
	{
		public double C { get; set; }
		public double GYield { get; set; }
		public double GYieldmemo { get; set; }
		public double Mn { get; set; }
		public double P { get; set; }
		public double PMAX { get; set; }
		public double S { get; set; }
		public double SMax { get; set; }
		public double Si { get; set; }
		public double T { get; set; }
		public double Tplav { get; set; }
		public double V { get; set; }
	}

	public class Лом : Навеска
	{
		public double C { get; set; }
		public double DolyaLegkovesa { get; set; }
		public double Mn { get; set; }
		public double P { get; set; }
		public double S { get; set; }
		public double Si { get; set; }
	}

	public class ЛомНизкий : Лом
	{
		public Лом Low { get; set; }
		public Лом Mid { get; set; }
		public Лом High { get; set; }
	}

	public class ЛомСредний : Лом
	{
		public Лом Low { get; set; }
		public Лом Mid { get; set; }
		public Лом High { get; set; }
	}

	public class ЛомВысокий : Лом
	{
		public Лом Low { get; set; }
		public Лом Mid { get; set; }
		public Лом High { get; set; }
	}

	public class Футеровка : Навеска
	{
		public double Al2O3 { get; set; }
		public double C { get; set; }
		public double CaO { get; set; }
		public double GTOTAL { get; set; }
		public double MgO { get; set; }
		public double P2O5 { get; set; }
		public double SiO2 { get; set; }
	}

	public class Дутье : Навеска
	{
		public double Ar { get; set; }
		public double N2 { get; set; }
		public double O2 { get; set; }
		public double V { get; set; }
	}

	public class Имф : Навеска
	{
		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double MgO { get; set; }
		public double SiO2 { get; set; }
	}

	public class Кокс : Навеска
	{
		public double C { get; set; }
	}

	public class Песок : Навеска
	{
		public double H2O { get; set; }
		public double SiO2 { get; set; }
	}

	public class Руда : Навеска
	{
		public double Al2O3 { get; set; }
		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double P { get; set; }
		public double SiO2 { get; set; }
	}

	public class Окатыши : Навеска
	{
		public double Fe2O3 { get; set; }
		public double FeO { get; set; }
		public double SiO2 { get; set; }
	}

	public class Агломерат : Навеска
	{
		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double FeO { get; set; }
	}

	public class Ферросплав : Навеска
	{
		public double Al { get; set; }
		public double C { get; set; }
		public double Fe { get; set; }
		public double Mn { get; set; }
		public double P { get; set; }
		public double S { get; set; }
		public double Si { get; set; }
	}

	public class ВлажныйДоломит : Навеска
	{
		public double Al2O3 { get; set; }
		public double CO2 { get; set; }
		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double H2O { get; set; }
		public double MgO { get; set; }
		public double SiO2 { get; set; }
	}

	public class Доломит : Навеска
	{
		public double Al2O3 { get; set; }
		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double MgO { get; set; }
		public double SiO2 { get; set; }
	}
}