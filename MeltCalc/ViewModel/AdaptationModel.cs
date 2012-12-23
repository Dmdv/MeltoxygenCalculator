using System;
using System.Linq;
using System.Windows;
using MeltCalc.Chemistry;
using MeltCalc.Model;
using MeltCalc.Helpers;

namespace MeltCalc.ViewModel
{
	public class AdaptationModel : BasePresenter
	{
		private int NumberOfAdaptedMelt, adaptROUND, BaseLenth, GoodMeltsQuant;
		private double[] minL, maxL, minalfaFe, maxalfaFe, minTeplFutLoss, maxTeplFutLoss, stepL, stepalfaFe, stepTeplFutLoss, GshlSAVE, LSAVE, alfaFeSAVE, TeplFutLossSAVE;
		private double CheckNoNull, adaptMistakeTOTAL, adaptCOMPAIR, TempLeft, TempRight;
		private double SummGshl, ExpectGshl, SummL, ExpectL, SummAlfaFe, ExpectAlfaFe, SummTeplFutLoss, ExpectTeplFutLoss;

		private const double PConst = 62.0 / 142.0;

		private readonly ParamsMdb _paramsMdb = new ParamsMdb();
		private readonly MeltMdb _meltMdb = new MeltMdb();

		public void Run(int sypuchIndex)
		{
			GoodMeltsQuant = 0;

			try
			{
				LoadFromCountData(Params.SelectedPlant);
				LoadFromLoose(sypuchIndex);
				LoadMixAndOstShl();
				DetectBaseLenth();
				InternalRun();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void InternalRun()
		{
			for (NumberOfAdaptedMelt = 0; NumberOfAdaptedMelt < BaseLenth; NumberOfAdaptedMelt++)
			{
				LoadMeltData(NumberOfAdaptedMelt);
				Params.Tog = Tube.Сталь.T;
				Tube.Сталь.Si = 0.003;
				Tube.Шлак.FeO = 0.701145 * Tube.Шлак.TOTALFeO - 0.586142;
				Tube.Шлак.Fe2O3 = Tube.Шлак.TOTALFeO - Tube.Шлак.FeO;
				Estimation.CALCULATE_TEPLCONSTANTS();
			}
		}

		private void LoadMixAndOstShl()
		{
			Tube.ОставленныйШлак.Load();
			Tube.МиксерныйШлак.Load();
		}

		private void DetectBaseLenth()
		{
			var table = _meltMdb.Reader.FetchTable("adaptationdata");
			var rowcount = table.RowCount();

			for (var idx = 0; idx < rowcount; idx++)
			{
				var values = table.SelectRowRange(idx).Take(7).Select(value => value.ToDouble()).ToArray();
				if (values[3] * values[4] * values[5] * values[6] > 0)
				{
					BaseLenth = idx + 1;
				}
			}
		}

		private void LoadMeltData(int rowindex)
		{
			var table = _meltMdb.Reader.FetchTable("adaptationdata");
			var values = table.SelectRowRange(rowindex).Take(36).Select(value => value.ToDouble()).ToArray();

			var i = 1;
			Params.TAUprost = values[++i];
			Tube.Чугун.C = values[++i];
			Tube.Чугун.Si = values[++i];
			Tube.Чугун.Mn = values[++i];
			Tube.Чугун.P = values[++i];
			Tube.Чугун.S = values[++i];
			Tube.Чугун.T = values[++i];
			Tube.Чугун.G = values[++i];
			Tube.Лом.G = values[++i];
			Tube.Известь.G = values[++i];
			Tube.Известняк.G = values[++i];
			Tube.Доломит.G = values[++i];
			Tube.ВлажныйДоломит.G = values[++i];
			Tube.Имф.G = values[++i];
			Tube.Песок.G = values[++i];
			Tube.Кокс.G = values[++i];
			Tube.Окатыши.G = values[++i];
			Tube.Руда.G = values[++i];
			Tube.Окалина.G = values[++i];
			Tube.Агломерат.G = values[++i];
			Tube.Шпат.G = values[++i];
			Tube.Дутье.V = values[++i];
			Tube.Сталь.C = values[++i];
			Tube.Сталь.Mn = values[++i];
			Tube.Сталь.P = values[++i];
			Tube.Сталь.S = values[++i];
			Tube.Сталь.T = values[++i];
			Tube.Шлак.CaO = values[++i];
			Tube.Шлак.SiO2 = values[++i];
			Tube.Шлак.TOTALFeO = values[++i];
			Tube.Шлак.MgO = values[++i];
			Tube.Шлак.MnO = values[++i];
			AdaptationData.VArBlow = values[++i];
			AdaptationData.GDensing = values[++i];
		}

		private void LoadFromLoose(int sypuchIndex)
		{
			Tube.Известь.Load(sypuchIndex);
			Tube.Известняк.Load(sypuchIndex);
			Tube.Доломит.Load(sypuchIndex);
			Tube.ВлажныйДоломит.Load(sypuchIndex);
			Tube.Имф.Load(sypuchIndex);
			Tube.Кокс.Load(sypuchIndex);
			Tube.Песок.Load(sypuchIndex);
			Tube.Окатыши.Load(sypuchIndex);
			Tube.Руда.Load(sypuchIndex);
			Tube.Окалина.Load(sypuchIndex);
			Tube.Агломерат.Load(sypuchIndex);
			Tube.Шпат.Load(sypuchIndex);
		}

		private void LoadFromCountData(int selectedPlant)
		{
			try
			{
				var range = _paramsMdb.Reader.SelectRowRange("countdata", selectedPlant);
				Tube.Сталь.GYield = range[2].ToDouble();
				Tube.ОставленныйШлак.G = range[4].ToDouble();
				Params.StAndShlLoss = range[18].ToDouble();
				Tube.Футеровка.G = range[19].ToDouble() / Params.FutDurability;
				Tube.МиксерныйШлак.G = range[20].ToDouble();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void P2O5_calc()
		{
			Tube.Шлак.P2O5 = (
				Tube.Чугун.G * Tube.Чугун.P + Tube.Лом.G * Tube.Лом.P +
				Tube.Известь.ALFA * Tube.Известь.G * Tube.Известь.P2O5 * PConst +
				Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.P2O5 * PConst + 
				Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.P + Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.P +
				Tube.Футеровка.G * Tube.Футеровка.P2O5 * PConst +
				Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.P2O5 * PConst +
				Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.P2O5 * PConst + 
				Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.P - 
				(Tube.Сталь.GYield / (1 - alfaFeSAVE[adaptROUND - 1] - Params.StAndShlLoss)) * Tube.Сталь.P) /
				(GshlSAVE[adaptROUND - 1] * PConst);
		}
	}
}
