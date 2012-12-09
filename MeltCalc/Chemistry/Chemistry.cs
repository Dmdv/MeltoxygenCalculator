using System;
using System.Collections.Generic;
using MeltCalc.Model;

namespace MeltCalc.Chemistry
{
	public class Навеска
	{
		protected Навеска()
		{
		}

		protected Навеска(ICollection<Навеска> registry)
		{
			registry.Add(this);
		}

		/// <summary>
		/// Масса.
		/// </summary>
		public double G;

		public double ALFA { get; set; }
		/// <summary>
		/// Сыпучий материал - шлакообразующий элемент?
		/// </summary>
		public Materials Material { get; protected set; }
	}

	public class Известь : Навеска
	{
		public Известь(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.Известь;
		}

		public double Al2O3 { get; set; }
		public double CaO { get; set; }
		public double H2O { get; set; }
		public double MgO { get; set; }
		public double P2O5 { get; set; }
		public double SiO2 { get; set; }
	}

	public class Известняк : Навеска
	{
		public Известняк(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.Известняк;
		}

		public double CO2 { get; set; }
		public double CaCO3 { get; set; }
		public double CaO { get; set; }
		public double H2O { get; set; }
		public double P2O5 { get; set; }
		public double SiO2 { get; set; }
	}

	public class Окалина : Навеска
	{
		public Окалина(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.Окалина;
		}

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
		public Шпат(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.ПлавиковыйШпат;
		}

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
		public Имф(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.ИзвестковоМагнитныйФлюс;
		}

		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double MgO { get; set; }
		public double SiO2 { get; set; }
	}

	public class Кокс : Навеска
	{
		public Кокс(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.Кокс;
		}

		public double C { get; set; }
	}

	public class Песок : Навеска
	{
		public Песок(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.Песок;
		}

		public double H2O { get; set; }
		public double SiO2 { get; set; }
	}

	public class Руда : Навеска
	{
		public Руда(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.Руда;
		}

		public double Al2O3 { get; set; }
		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double P { get; set; }
		public double SiO2 { get; set; }
	}

	public class Окатыши : Навеска
	{
		public Окатыши(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.Окатыши;
		}

		public double Fe2O3 { get; set; }
		public double FeO { get; set; }
		public double SiO2 { get; set; }
	}

	public class Агломерат : Навеска
	{
		public Агломерат(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.Агломерат;
		}

		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double FeO { get; set; }
	}

	public class Ферросплав : Навеска
	{
		public double Al { get; set; }
		public double C { get; set; }
		public double Fe
		{
			get { return 100.0 - Al - C - Mn - P - S - Si; }
		}
		public double Mn { get; set; }
		public double P { get; set; }
		public double S { get; set; }
		public double Si { get; set; }

		public static bool Validate(double galloy)
		{
			return !(Math.Abs(galloy - 0.0) < 0.000001 || (galloy >= 0.01 && galloy <= 10.0));
		}
	}

	public class ВлажныйДоломит : Навеска
	{
		public ВлажныйДоломит(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.СыройДоломит;
		}

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
		public Доломит(ICollection<Навеска> registry)
			: base(registry)
		{
			Material = Materials.Доломит;
		}

		public double Al2O3 { get; set; }
		public double CaO { get; set; }
		public double Fe2O3 { get; set; }
		public double MgO { get; set; }
		public double SiO2 { get; set; }
	}
}