namespace MeltCalc.Chemistry
{
	public abstract class ������������
	{
		/// <summary>
		/// ��������������� ���.
		/// </summary>
		public ��� ��������������� { get; set; }

		/// <summary>
		/// ����������������.
		/// </summary>
		public ��� ���������������� { get; set; }

		/// <summary>
		/// ����������������.
		/// </summary>
		public ��� ���������������� { get; set; }
	}

	public class ��������� : ������������
	{
		public ���������()
		{
			��������������� = new ���(���.RowIndex.LowSmall);
			���������������� = new ���(���.RowIndex.MidSmall);
			���������������� = new ���(���.RowIndex.HighSmall);
		}
	}

	public class ���������� : ������������
	{
		public ����������()
		{
			��������������� = new ���(���.RowIndex.LowMed);
			���������������� = new ���(���.RowIndex.MidMed);
			���������������� = new ���(���.RowIndex.HighMed);
		}
	}

	public class ���������� : ������������
	{
		public ����������()
		{
			��������������� = new ���(���.RowIndex.LowBig);
			���������������� = new ���(���.RowIndex.MidBig);
			���������������� = new ���(���.RowIndex.HighBig);
		}
	}

	public static class Params
	{
		public static double alfaFe, L, StAndShlLoss, TAUprost, TAUprostREAL, TeplFutLoss, TAPtime, Lp, Tog;

		public static int SelectedPlant,
		                  SelectedAdaptedPlant,
		                  FutTypeSelected,
		                  LomTypeSelected,
		                  AirTemp,
		                  FutDurability,
		                  BlowingTime;

		// TODO:
		public static int Round;
		// TODO:
		public static string InputForm;
		// TODO:
		public static bool IsDuplex;

		static Params()
		{
			��������� = new ���������();
			���������� = new ����������();
			���������� = new ����������();
		}

		// cmdLastEstimate �� ����������.

		public static ��������� ��������� { get; set; }
		public static ���������� ���������� { get; set; }
		public static ���������� ���������� { get; set; }
	}
}