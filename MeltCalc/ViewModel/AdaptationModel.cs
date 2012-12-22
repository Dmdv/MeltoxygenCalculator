namespace MeltCalc.ViewModel
{
	public class AdaptationModel : BasePresenter
	{
		private int NumberOfAdaptedMelt, adaptROUND, BaseLenth, GoodMeltsQuant;
		private double[] minL, maxL, minalfaFe, maxalfaFe, minTeplFutLoss, maxTeplFutLoss, stepL, stepalfaFe, stepTeplFutLoss, GshlSAVE, LSAVE, alfaFeSAVE, TeplFutLossSAVE;
		private double CheckNoNull, adaptMistakeTOTAL, adaptCOMPAIR, TempLeft, TempRight;
		private double SummGshl, ExpectGshl, SummL, ExpectL, SummAlfaFe, ExpectAlfaFe, SummTeplFutLoss, ExpectTeplFutLoss;

		public AdaptationModel()
		{
		}
	}
}
