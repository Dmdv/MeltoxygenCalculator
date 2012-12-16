using System;
using System.Collections.Generic;
using System.Data;
using MeltCalc.Converters;
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

	/// <summary>
	/// This is a row from params.mdb
	/// </summary>
	public class Лом : Навеска
	{
		private const string Lomdata = "lomdata";
		private static readonly ParamsMdb _paramsMdb = new ParamsMdb();
		private static readonly StringToDoubleConverter _converter = new StringToDoubleConverter();

		private readonly RowIndex _index;
		private readonly DataTable _table;

		public Лом()
		{
		}

		public Лом(RowIndex index)
		{
			_index = index;
			_table = _paramsMdb.Reader.FetchTable(Lomdata);

			Mn = ReadValue("Mn");
			Si = ReadValue("Si");
			P = ReadValue("P");
			S = ReadValue("S");
			C = ReadValue("С");
		}

		public float C { get; private set; }
		public float DolyaLegkovesa { get; private set; }
		public float Mn { get; private set; }
		public float P { get; private set; }
		public float S { get; private set; }
		public float Si { get; private set; }

		private float ReadValue(string column)
		{
			var index = (int) _index;
			var value = _table.Rows[index][column];
			return (float)_converter.ConvertBack(value, typeof(double), null, null);
		}

		/// <summary>
		/// Углеродность - Размерность.
		/// </summary>
		public enum RowIndex
		{
			LowSmall,
			LowMed,
			LowBig,
			MidSmall,
			MidMed,
			MidBig,
			HighSmall,
			HighMed,
			HighBig
		}
	}

	public abstract class ЛомРазмерный
	{
		/// <summary>
		/// Низкоуглеродный лом.
		/// </summary>
		protected Лом Low { get; set; }

		/// <summary>
		/// Среднеуглеродный.
		/// </summary>
		protected Лом Mid { get; set; }

		/// <summary>
		/// Высокоуглеродный.
		/// </summary>
		protected Лом High { get; set; }
	}

	public class ЛомМелкий : ЛомРазмерный
	{
		public ЛомМелкий()
		{
			Low = new Лом(Лом.RowIndex.LowSmall);
			Mid = new Лом(Лом.RowIndex.LowMed);
			High = new Лом(Лом.RowIndex.LowBig);
		}
	}

	public class ЛомСредний : ЛомРазмерный
	{
		public ЛомСредний()
		{
			Low = new Лом(Лом.RowIndex.MidSmall);
			Mid = new Лом(Лом.RowIndex.MidMed);
			High = new Лом(Лом.RowIndex.MidBig);
		}
	}

	public class ЛомКрупный : ЛомРазмерный
	{
		public ЛомКрупный()
		{
			Low = new Лом(Лом.RowIndex.HighSmall);
			Mid = new Лом(Лом.RowIndex.HighMed);
			High = new Лом(Лом.RowIndex.HighBig);
		}
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