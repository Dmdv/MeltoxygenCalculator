using System;
using System.IO;
using System.Text;
using System.Windows;
using MeltCalc.Chemistry;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for Results.xaml
	/// </summary>
	public partial class Results
	{
		public Results()
		{
			InitializeComponent();
			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			SHLcount();
			// TODO: Дизейбл тех контролов, которые не являются сыпучими.

			LoadCommonParams();
			LoadSypuch();
			LoadShlakCompund();
			LoadSteelCompound();

			// TODO: Черный шрифт окон.
			// TODO: Проверка на отрицательность

			SaveResults();
		}

		private void SaveResults()
		{
			var text = new StringBuilder("-- New calculation --\r\n")

				.Append(ResultsRow("C чугуна", Tube.Чугун.C))
				.Append(ResultsRow("Si чугуна", Tube.Чугун.Si))
				.Append(ResultsRow("Mn чугуна", Tube.Чугун.Mn))
				.Append(ResultsRow("P чугуна", Tube.Чугун.P))
				.Append(ResultsRow("S чугуна", Tube.Чугун.S))

				.Append(ResultsRow("C лома", Tube.Лом.C))
				.Append(ResultsRow("Si лома", Tube.Лом.Si))
				.Append(ResultsRow("Mn лома", Tube.Лом.Mn))
				.Append(ResultsRow("P лома", Tube.Лом.P))
				.Append(ResultsRow("S лома", Tube.Лом.S))

				.Append(ResultsRow("Масса чугуна", Estimation.GchugSAVE[Estimation.GchugSAVE.Length - 1]))
				.Append(ResultsRow("Масса лома", Estimation.GlomSAVE[Estimation.GlomSAVE.Length - 1]))
				.Append(ResultsRow("Масса ост. шлака", Tube.ОставленныйШлак.G))
				.Append(ResultsRow("Масса микс. шлака", Tube.МиксерныйШлак.G))
				.Append(ResultsRow("Масса шлака", Estimation.GshlSAVE[Estimation.GshlSAVE.Length - 1]))

				.Append(ResultsRow("Выход стали", Tube.Сталь.GYield))
				.Append(ResultsRow("C стали", Tube.Сталь.C))
				.Append(ResultsRow("Si стали", Tube.Сталь.Si))
				.Append(ResultsRow("Mn стали", Tube.Сталь.Mn))
				.Append(ResultsRow("P стали", Tube.Сталь.P))
				.Append(ResultsRow("S стали", Tube.Сталь.S))

				.Append(ResultsRow("FeO шлака всего", Tube.Шлак.TOTALFeO))
				.Append(ResultsRow("Mn шлака", Estimation.MnOshlSAVE[Estimation.MnOshlSAVE.Length - 1]))
				.Append(ResultsRow("P2P5 шлака", Tube.Шлак.P2O5))

				.Append(ResultsRow("Основность", Tube.Шлак.B))

				.Append(ResultsRow("Известь", Estimation.GizvSAVE[Estimation.GizvSAVE.Length - 1]))
				.Append(ResultsRow("Известняк", Estimation.GizkSAVE[Estimation.GizkSAVE.Length - 1]))
				.Append(ResultsRow("Доломит", Estimation.GdolSAVE[Estimation.GdolSAVE.Length - 1]))
				.Append(ResultsRow("Вл. Доломит", Estimation.GvldolSAVE[Estimation.GvldolSAVE.Length - 1]))
				.Append(ResultsRow("ИМФ", Estimation.GimfSAVE[Estimation.GimfSAVE.Length - 1]))

				.Append(ResultsRow("Песок", Tube.Песок.G))
				.Append(ResultsRow("Кокс", Tube.Кокс.G))
				.Append(ResultsRow("Окатыши", Tube.Окатыши.G))
				.Append(ResultsRow("Руда", Tube.Руда.G))
				.Append(ResultsRow("Окалина", Tube.Окалина.G))
				.Append(ResultsRow("Агломерат", Tube.Агломерат.G))

				.Append(ResultsRow("Шпат", Estimation.GshpSAVE[Estimation.GshpSAVE.Length - 1]))
				.Append(ResultsRow("Объём О2", Estimation.VdutSAVE[Estimation.VdutSAVE.Length - 1]))
				.Append(ResultsRow("Темп воздуха", Params.AirTemp))
				.Append(ResultsRow("Темп чугуна", Tube.Чугун.T))
				.Append(ResultsRow("Темп стали", Tube.Сталь.T))

				.Append(ResultsRow("Ar дно, м3/мин", Params.BlowingTime == 0 ? 0 : AdaptationData.VArBlow / Params.BlowingTime))

				.Append(ResultsRow("Футеровка", Tube.Футеровка.G))
				.Append(ResultsRow("О2", Tube.Дутье.O2))
				.Append(ResultsRow("Ar", Tube.Дутье.Ar))
				.Append(ResultsRow("N2", Tube.Дутье.N2))
				.Append(ResultsRow("Доля легковеса", Tube.Лом.DolyaLegkovesa))
				.Append(ResultsRow("Пакеты", Tube.Пакеты.G))

				// Пропущены COMPAIR, FutTypeSelected, SelectedPlant

			.ToString();

			// TODO: В адаптации поправить хардкоженный параметр

			try
			{
				File.AppendAllText("Results.txt", text);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при записи результатов:\r\n" + ex.Message, "Ошибка записи",
				                MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private string ResultsRow(string parameter, double value)
		{
			return string.Format("{0}:\t\t\t{1:0.000}{2}", parameter, value, Environment.NewLine);
		}

		private void LoadCommonParams()
		{
			_massChugun.Text = string.Format("{0:0.00}", Estimation.GchugSAVE[Params.Round - 1]);
			_massLlom.Text = string.Format("{0:0.00}", Estimation.GlomSAVE[Params.Round - 1]);
			_oxygenVolume.Text = string.Format("{0:0.00}", Estimation.VdutSAVE[Params.Round - 1]);
		}

		private void LoadSypuch()
		{
			_izv.Text = string.Format("{0:0.00}", Estimation.GizvSAVE[Params.Round - 1]);
			_izk.Text = string.Format("{0:0.00}", Estimation.GizkSAVE[Params.Round - 1]);
			_dol.Text = string.Format("{0:0.00}", Estimation.GdolSAVE[Params.Round - 1]);
			_vldol.Text = string.Format("{0:0.00}", Estimation.GvldolSAVE[Params.Round - 1]);
			_imf.Text = string.Format("{0:0.00}", Estimation.GimfSAVE[Params.Round - 1]);
			_shp.Text = string.Format("{0:0.00}", Estimation.GshpSAVE[Params.Round - 1]);

			_koks.Text = string.Format("{0:0.00}", Tube.Кокс.G);
			_pesok.Text = string.Format("{0:0.00}", Tube.Песок.G);
			_okat.Text = string.Format("{0:0.00}", Tube.Окатыши.G);
			_ruda.Text = string.Format("{0:0.00}", Tube.Руда.G);
			_okal.Text = string.Format("{0:0.00}", Tube.Окалина.G);
			_agl.Text = string.Format("{0:0.00}", Tube.Агломерат.G);
		}

		private void LoadShlakCompund()
		{
			_osnovnost.Text = string.Format("{0:0.00}", Tube.Шлак.B);
			_massa.Text = string.Format("{0:0.000}", Estimation.GshlSAVE[Params.Round - 1]);
			_feo.Text = string.Format("{0:0.000}", Tube.Шлак.TOTALFeO);
			_cao.Text = string.Format("{0:0.00}", Tube.Шлак.CaO);
			_mgo.Text = string.Format("{0:0.00}", Tube.Шлак.MgO);
			_mno.Text = string.Format("{0:0.00}", Tube.Шлак.MnO);
			_p2o5.Text = string.Format("{0:0.00}", Tube.Шлак.P2O5);
		}

		private void LoadSteelCompound()
		{
			_c.Text = string.Format("{0:0.000}", Tube.Сталь.C);
			_si.Text = string.Format("{0:0.000}", Tube.Сталь.Si);
			_mn.Text = string.Format("{0:0.000}", Tube.Сталь.Mn);
			_p.Text = string.Format("{0:0.000}", Tube.Сталь.P);
			_s.Text = string.Format("{0:0.000}", Tube.Сталь.S);
			_temp.Text = string.Format("{0:0.000}", Tube.Сталь.T);
		}

		private static void SHLcount()
		{
			Tube.Шлак.CaO = (Tube.Известь.ALFA * Tube.Известь.G * (Tube.Известь.CaO) +
							 Tube.Известняк.ALFA * Tube.Известняк.G * 56.0 / 100.0 * Tube.Известняк.CaCO3 +
							 Tube.Доломит.ALFA * Tube.Доломит.G * (Tube.Доломит.CaO) +
							 Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * (Tube.ВлажныйДоломит.CaO) +
							 Tube.Имф.ALFA * Tube.Имф.G * (Tube.Имф.CaO) + Tube.Футеровка.G * (Tube.Футеровка.CaO) +
							 Tube.Агломерат.ALFA * Tube.Агломерат.G * Tube.Агломерат.CaO +
							 Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.CaO + Tube.ОставленныйШлак.G * (Tube.ОставленныйШлак.CaO) +
							 Tube.МиксерныйШлак.G * (Tube.МиксерныйШлак.CaO) +
							 Tube.Пакеты.ALFA * Tube.Пакеты.G * (Tube.Пакеты.CaO)) / Tube.Шлак.G;

			Tube.Шлак.MgO = (Tube.Известь.ALFA * Tube.Известь.G * (Tube.Известь.MgO) +
							 Tube.Доломит.ALFA * Tube.Доломит.G * (Tube.Доломит.MgO) +
							 Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.MgO +
							 Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * (Tube.ВлажныйДоломит.MgO) +
							 Tube.Имф.ALFA * Tube.Имф.G * (Tube.Имф.MgO) + Tube.Футеровка.G * (Tube.Футеровка.MgO) +
							 Tube.ОставленныйШлак.G * (Tube.ОставленныйШлак.MgO) +
							 Tube.МиксерныйШлак.G * (Tube.МиксерныйШлак.MgO) +
							 Tube.Пакеты.ALFA * Tube.Пакеты.G * (Tube.Пакеты.MgO)) / Tube.Шлак.G;
		}
	}
}
