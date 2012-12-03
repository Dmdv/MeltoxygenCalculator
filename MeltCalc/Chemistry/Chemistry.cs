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

	public class Окалина : Навеска
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

	public class Шпат : Навеска
	{
		public double ALFA;
		public double CaF2;
		public double CaO;
		public double SiO2;
	}

	public class Шлак : Навеска
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

	public class ОставленныйШлак : Навеска
	{
		public double Al2O3;
		public double CaO;
		public double Fe2O3;
		public double FeO;
		public double MgO, MnO, P2O5;
		public double SiO2;
	}

	public class МиксерныйШлак : Навеска
	{
		public double CaO, SiO2, MnO, MgO, P2O5, FeO, Fe2O3;
	}

	public class Чугун : Навеска
	{
		public double C, Si, Mn, P, S, T;
	}

	public class Сталь : Навеска
	{
		public double C, V, Si, Mn, P, S, T, Tplav, GYield, GYieldmemo, PMAX, SMax;
	}

	public class Лом : Навеска
	{
		public double C, Si, Mn, P, S, DolyaLegkovesa;
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
		public double GTOTAL, CaO, SiO2, MgO, Al2O3, P2O5, C;
	}

	public class Дутье : Навеска
	{
		public double V, O2, Ar, N2;
	}

	public class Имф : Навеска
	{
		public double CaO, SiO2, MgO, Fe2O3;
	}

	public class Кокс : Навеска
	{
		public double C;
	}

	public class Песок : Навеска
	{
		public double SiO2, H2O;
	}

	public class Руда : Навеска
	{
		public double CaO, SiO2, Fe2O3, Al2O3, P;
	}

	public class Окатыши : Навеска
	{
		public double SiO2, FeO, Fe2O3;
	}

	public class Агломерат : Навеска
	{
		public double CaO, FeO, Fe2O3;
	}

	public class Ферросплав : Навеска
	{
		public double Fe, Mn, Si, C, P, Al, S;
	}

	public class ВлажныйДоломит : Навеска
	{
		public double CaO, SiO2, MgO, Fe2O3, CO2, H2O, Al2O3;
	}

	public class Доломит : Навеска
	{
		public double CaO, SiO2, MgO, Fe2O3, Al2O3;
	}

	public class Params
	{
		public static double alfaFe, L, StAndShlLoss, TAUprost, TAUprostREAL, TeplFutLoss, TAPtime, Lp, Tog;
	}

	public class Packets : Навеска
	{
		public double dH,
		              CaO,
		              SiO2,
		              MnO,
		              MgO,
		              P2O5,
		              FeO,
		              Fe2O3,
		              Al2O3,
		              TiO2p,
		              V2O5p,
		              C,
		              P,
		              S,
		              Fe;
	}
}