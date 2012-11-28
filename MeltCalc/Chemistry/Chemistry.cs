namespace MeltCalc.Chemistry
{
	public class Навеска
	{
		public double G;
		public double ALFA { get; set; }
	}

	public class Известь : Навеска
	{
		public double Al2O3;
		public double CaO;
		public double H2O;
		public double MgO;
		public double P2O5;
		public double SiO2;
	}

	public class Известняк : Навеска
	{
		public double CO2;
		public double CaCO3;
		public double CaO;
		public double H2O;
		public double P2O5;
		public double SiO2;
	}

	public class Окалина
	{
		public double ALFA;
		public double Fe2O3;
		public double Fe3O4;
		public double FeO;
		public double MgO;
		public double MnO;
		public double P;
		public double SiO2;
	}

	public class Шпат
	{
		public double ALFA;
		public double CaF2;
		public double CaO;
		public double SiO2;
	}

	public class Шлак
	{
		public double Al2O3;
		public double B;
		public double Bmax;
		public double Bmin;
		public double CaO;
		public double Fe2O3;
		public double FeO;
		public double MgO;
		public double MnO;
		public double P2O5;
		public double SiO2;
		public double TOTALFeO;
		public double V2O5;
	}

	public class ОставленныйШлак
	{
		public double Al2O3;
		public double CaO;
		public double Fe2O3;
		public double FeO;
		public double MgO, MnO, P2O5;
		public double SiO2;
	}

	public class МиксерныйШлак
	{
		public double CaO, SiO2, MnO, MgO, P2O5, FeO, Fe2O3;
	}

	public class Чугун
	{
		public double C, Si, Mn, P, S, T;
	}

	public class Сталь
	{
		public double C, V, Si, Mn, P, S, T, Tplav, GYield, GYieldmemo, PMAX, SMax;
	}

	public class Лом
	{
		public double C, Si, Mn, P, S, DolyaLegkovesa;
	}

	public class Футеровка
	{
		public double GTOTAL, CaO, SiO2, MgO, Al2O3, P2O5, C;
	}

	public class Дутье
	{
		public double V, O2, Ar, N2;
	}

	public class Имф
	{
		public double CaO, SiO2, MgO, Fe2O3;
	}

	public class Кокс
	{
		public double C;
	}

	public class Песок
	{
		public double SiO2, H2O;
	}

	public class Руда
	{
		public double CaO, SiO2, Fe2O3, Al2O3, P;
	}

	public class Окатыши
	{
		public double SiO2, FeO, Fe2O3;
	}

	public class Агломерат
	{
		public double CaO, FeO, Fe2O3;
	}

	public class Ферросплав
	{
		public double Fe, Mn, Si, C, P, Al, S;
	}

	public class ВлажныйДоломит
	{
		public double CaO, SiO2, MgO, Fe2O3, CO2, H2O, Al2O3;
	}
}