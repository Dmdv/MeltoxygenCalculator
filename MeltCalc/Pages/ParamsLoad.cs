using System.Linq;
using MeltCalc.Helpers;
using MeltCalc.Chemistry;
using MeltCalc.Model;

namespace MeltCalc.Pages
{
	public class ParamsLoad
	{
		private readonly ParamsMdb _paramsMdb = new ParamsMdb();

		public void Run()
		{
			Load_LOOSERANGE();
			Load_ChemOfMixShl();
			Load_ChemOfOstShl();

			if (Params.InputForm == "auto")
			{
				Load_COUNTDATA();
				// TODO: Step 12
			}
			else
			{
				// TODO: Load ParamsInput
			}
		}

		private void Load_COUNTDATA()
		{
			var range = _paramsMdb.Reader
				.SelectRowRange("countdata", Params.SelectedPlant)
				.Select(x => x.ToDouble())
				.ToArray();

			Tube.Сталь.GYield = range[2];
			Tube.Сталь.GYieldmemo = range[2];
			Tube.Ферросплав.ALFA = range[3];
			Params.L = range[5];
			Estimation.minimumGchug = range[6];
			Estimation.maximumGchug = range[7];
			Estimation.minimumGlom = range[8];
			Estimation.maximumGlom = range[9];
			Estimation.minimumVdut = range[10];
			Estimation.maximumVdut = range[11];
			Estimation.minimumGshl = range[12];
			Estimation.maximumGshl = range[13];
			Estimation.minimumMnOshl = range[14];
			Estimation.maximumMnOshl = range[15];
			Estimation.minimumPst = range[16];
			Estimation.maximumPst = range[17];
			Params.StAndShlLoss = range[18];
			Tube.Футеровка.GTotal = range[19];
			Tube.МиксерныйШлак.G = range[20];
			Params.TAUprost = range[21];
			Params.TeplFutLoss = range[22];

			// TODO: Строка Gfut = GfutTOTAL / FutDurability не выполняется никогда.

			if (Params.IsDuplex)
			{
				Estimation.maximumGokat = range[39];
			}
		}

		private void Load_ChemOfOstShl()
		{
			// TODO: Complete
		}

		private void Load_ChemOfMixShl()
		{
			// TODO: Complete
		}

		private void Load_LOOSERANGE()
		{
			// TODO: Complete
		}
	}
}
