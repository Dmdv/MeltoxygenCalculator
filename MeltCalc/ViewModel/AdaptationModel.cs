using System;
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
		private readonly LooseMdb _looseMdb = new LooseMdb();
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
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void DetectBaseLenth()
		{
			throw new NotImplementedException();
		}

		private void LoadMixAndOstShl()
		{
			throw new NotImplementedException();
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

		private void LoadMeltData()
		{
			throw new NotImplementedException();
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
