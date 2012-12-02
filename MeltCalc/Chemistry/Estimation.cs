namespace MeltCalc.Chemistry
{
	public class Estimation
	{
		static Estimation()
		{
			GizvSAVE = GizkSAVE = GdolSAVE = GvldolSAV = GimfSAVE = new double[6];
			GchugSAVE = GlomSAVE = VdutSAVE = GshlSAVE = MnOshlSAVE = GstYieldSAVE = ALFAizvSAVE = new double[6];
		}

		// Специальные переменные
		public static readonly double[] GizvSAVE, GizkSAVE, GdolSAVE, GvldolSAV, GimfSAVE;
		public static double GshpSAVE;
		public static readonly double[] GchugSAVE, GlomSAVE, VdutSAVE, GshlSAVE, MnOshlSAVE, GstYieldSAVE, ALFAizvSAVE;

		private double NeededLp, IterTimes;
		private string MOVINGSide;

		private void Prepare1_REGRESSLOAD() 
		{ 
			//Загрузка коэффициентов регрессионных уравнений по (FeO), [Mn], Lp
		}

		private double Calculate_P2O5shl_Bal_P()
		{
			return
				(Tube.Чугун.G * Tube.Чугун.P + Tube.Лом.G * Tube.Лом.P
				+ Tube.Известь.ALFA * Tube.Известь.G * Tube.Известь.P2O5 * 62.0 / 142.0 + Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.P2O5 * 62.0 / 142.0
				+ Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.P + Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.P
				+ Tube.Футеровка.P2O5 * 62.0 / 142.0
				+ Tube.ОставленныйШлак.P2O5 * 62.0 / 142.0 + Tube.МиксерныйШлак.P2O5 * 62.0 / 142.0
				+ Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.P
				- (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.P) / (Tube.Шлак.G * 62.0 / 142.0);
		}

		private double Calculate_Pst_Bal_P(int round)
		{
			return
				(GchugSAVE[round - 1] * Tube.Чугун.P + GlomSAVE[round - 1] * Tube.Лом.P
				+ Tube.Известняк.ALFA * GizvSAVE[round - 1] * Tube.Известь.P2O5 * 62.0 / 142.0 + Tube.Известняк.ALFA * Tube.Известняк.P2O5 * 62.0 / 142.0
				+ Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.P + Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.P
				+ Tube.Футеровка.G * Tube.Футеровка.P2O5 * 62.0 / 142.0 + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.P
				+ Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.P2O5 * 62.0 / 142.0 + Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.P2O5 * 62.0 / 142.0
				+ Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.P) / ((Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) + GshlSAVE[round - 1] * Params.Lp * 62.0 / 142.0);
		}

		private void Balances_Calc()
		{
			//Баланс CaO по заданной основности

			double LeftCaO_po_B =
				Tube.Известь.ALFA * Tube.Известь.G * (Tube.Известь.CaO + Tube.Известь.MgO) + Tube.Известняк.ALFA * Tube.Известняк.G * 56.0 / 100.0 * Tube.Известняк.CaCO3
				+ Tube.Доломит.ALFA * Tube.Доломит.G * (Tube.Доломит.CaO + Tube.Доломит.MgO) + Tube.Шпат.ALFA * Tube.Шпат.G * Tube.Шпат.CaO + Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.MgO
				+ Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * (Tube.ВлажныйДоломит.CaO + Tube.ВлажныйДоломит.MgO) + +Tube.Имф.ALFA * Tube.Имф.G * (Tube.Имф.CaO + Tube.Имф.MgO)
				+ Tube.Футеровка.G * (Tube.Футеровка.CaO + Tube.Футеровка.MgO) + Tube.Агломерат.ALFA * Tube.Агломерат.G * Tube.Агломерат.CaO + Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.CaO
				+ Tube.ОставленныйШлак.G * (Tube.ОставленныйШлак.CaO + Tube.ОставленныйШлак.MgO) + Tube.МиксерныйШлак.G * (Tube.МиксерныйШлак.CaO + Tube.МиксерныйШлак.MgO)
				+ Tube.Pack.ALFA * Tube.Pack.G * (Tube.Pack.CaO + Tube.Pack.MgO);

			double RightCaO_po_B =
				Tube.Шлак.B * (60.0 / 28.0) *
				(Tube.Чугун.G * Tube.Чугун.Si + Tube.Лом.G * Tube.Лом.Si +
				 Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.Si -
				 (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Si)
				+ Tube.Шлак.B * Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.SiO2 +
				Tube.Шлак.B * Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.SiO2 +
				Tube.Шлак.B * Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.SiO2
				+
				Tube.Шлак.B *
				(Tube.Доломит.ALFA * Tube.Доломит.G * Tube.Доломит.SiO2 +
				 Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.SiO2 +
				 Tube.Имф.ALFA * Tube.Имф.G * Tube.Имф.SiO2)
				+
				Tube.Шлак.B *
				(Tube.Шпат.ALFA * Tube.Шпат.G * Tube.Шпат.SiO2 + Tube.Известь.ALFA * Tube.Известь.G * Tube.Известь.SiO2 +
				 Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.SiO2 + Tube.Футеровка.G * Tube.Футеровка.SiO2)
				+
				Tube.Шлак.B *
				(Tube.Окатыши.ALFA * Tube.Окатыши.G * Tube.Окатыши.SiO2 + Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.SiO2 +
				 Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.SiO2 + Tube.Песок.ALFA * Tube.Песок.G * Tube.Песок.SiO2);

			//Баланс CaO и MgO по химии шлака, используется во втором варианте адаптации
			
			// Вот тут может заменить эту формулу просто на присваивание?
			// float LeftCaOiMgO = LeftCaO_po_B

			double LeftCaOiMgO =
				Tube.Известь.ALFA * Tube.Известь.G * (Tube.Известь.CaO + Tube.Известь.MgO) + Tube.Известняк.ALFA * Tube.Известняк.G * 56.0 / 100.0 * Tube.Известняк.CaCO3
				+ Tube.Доломит.ALFA * Tube.Доломит.G * (Tube.Доломит.CaO + Tube.Доломит.MgO) + Tube.Шпат.ALFA * Tube.Шпат.G * Tube.Шпат.CaO + Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.MgO
				+ Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * (Tube.ВлажныйДоломит.CaO + Tube.ВлажныйДоломит.MgO) + Tube.Имф.ALFA * Tube.Имф.G * (Tube.Имф.CaO + Tube.Имф.MgO)
				+ Tube.Футеровка.G * (Tube.Футеровка.CaO + Tube.Футеровка.MgO) + Tube.Агломерат.ALFA * Tube.Агломерат.G * Tube.Агломерат.CaO + Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.CaO
				+ Tube.ОставленныйШлак.G * (Tube.ОставленныйШлак.CaO + Tube.ОставленныйШлак.MgO) + Tube.МиксерныйШлак.G * (Tube.МиксерныйШлак.CaO + Tube.МиксерныйШлак.MgO)
				+ Tube.Pack.ALFA * Tube.Pack.G * (Tube.Pack.CaO + Tube.Pack.MgO);

			double RightCaOiMgO = 
				Tube.Шлак.G * (Tube.Шлак.CaO + Tube.Шлак.MgO);

			//Баланс шлака 

			double RightSHL = 
				Tube.Шлак.G + Tube.МиксерныйШлак.G + Tube.ОставленныйШлак.G
				+ (60.0/28.0)*(Tube.Чугун.G*Tube.Чугун.Si + Tube.Лом.G * Tube.Лом.Si + Tube.Ферросплав.ALFA*Tube.Ферросплав.G*Tube.Ферросплав.Si - (Tube.Сталь.GYield/(1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Si) * 1/100
				+ (71.0/55.0)*(Tube.Чугун.G*Tube.Чугун.Mn + Tube.Лом.G * Tube.Лом.Mn + Tube.Ферросплав.ALFA*Tube.Ферросплав.G*Tube.Ферросплав.Mn - (Tube.Сталь.GYield/(1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Mn) * 1/100
				+ (142.0/62.0)*(Tube.Чугун.G*Tube.Чугун.P + Tube.Лом.G * Tube.Лом.P + Tube.Ферросплав.ALFA*Tube.Ферросплав.G*Tube.Ферросплав.P + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.P - (Tube.Сталь.GYield/(1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Mn) * 1/100
				+ (Tube.Шлак.G * Tube.Шлак.FeO - Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.FeO - Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.FeO - Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.FeO
				- Tube.Агломерат.ALFA * Tube.Агломерат.G * Tube.Агломерат.FeO - Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.Fe3O4 * (72.0/232.0) - Tube.Окатыши.ALFA * Tube.Окатыши.G * Tube.Окатыши.FeO) * 1/100
				+ (Tube.Шлак.G * Tube.Шлак.Fe2O3 - Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.Fe2O3 - Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.Fe2O3 - Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.Fe2O3
				- Tube.Доломит.ALFA * Tube.Доломит.G * Tube.Доломит.Fe2O3 - Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.Fe2O3 - Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.Fe2O3
				- Tube.Агломерат.ALFA * Tube.Агломерат.G * Tube.Агломерат.Fe2O3 - Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.Fe3O4 * (160.0/232.0) - Tube.Окатыши.ALFA * Tube.Окатыши.G * Tube.Окатыши.Fe2O3 - Tube.Имф.ALFA * Tube.Имф.G * Tube.Имф.Fe2O3) * 1/100
				+ Tube.Известь.ALFA * Tube.Известь.G * (Tube.Известь.CaO + Tube.Известь.SiO2 + Tube.Известь.MgO + Tube.Известь.P2O5 + Tube.Известь.Al2O3) * 1/100
				+ Tube.Известняк.ALFA * Tube.Известняк.G * ((56.0/100.0)*Tube.Известняк.CaCO3 + Tube.Известняк.SiO2 + Tube.Известняк.P2O5) * 1/100
				+ Tube.Доломит.ALFA * Tube.Доломит.G *(Tube.Доломит.CaO + Tube.Доломит.SiO2 + Tube.Доломит.MgO + Tube.Доломит.Fe2O3 + Tube.Доломит.Al2O3) * 1/100
				+ Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G *(Tube.ВлажныйДоломит.CaO + Tube.ВлажныйДоломит.SiO2 + Tube.ВлажныйДоломит.MgO + Tube.ВлажныйДоломит.Fe2O3 + Tube.ВлажныйДоломит.Al2O3) * 1/100
				+ Tube.Pack.ALFA * Tube.Pack.G * (Tube.Pack.CaO + Tube.Pack.SiO2 + Tube.Pack.MnO + Tube.Pack.MgO + Tube.Pack.P2O5 + Tube.Pack.FeO + Tube.Pack.Fe2O3 + Tube.Pack.Al2O3) * 1 / 100
				+ Tube.Имф.ALFA * Tube.Имф.G * (Tube.Имф.CaO + Tube.Имф.SiO2 + Tube.Имф.MgO + Tube.Имф.Fe2O3) * 1/100
				+ Tube.Окатыши.ALFA * Tube.Окатыши.G * (Tube.Окатыши.SiO2 + Tube.Окатыши.FeO + Tube.Окатыши.Fe2O3) * 1/100
				+ Tube.Руда.ALFA * Tube.Руда.G * (Tube.Руда.CaO + Tube.Руда.SiO2 + Tube.Руда.Al2O3 + Tube.Руда.Fe2O3) * 1/100
				+ Tube.Окалина.ALFA * Tube.Окалина.G * (Tube.Окалина.SiO2 + Tube.Окалина.MnO + Tube.Окалина.MgO + Tube.Окалина.Fe3O4) * 1/100    
				+ Tube.Агломерат.ALFA * Tube.Агломерат.G * (Tube.Агломерат.CaO + Tube.Агломерат.FeO + Tube.Агломерат.Fe2O3) * 1/100  
				+ Tube.Шпат.ALFA * Tube.Шпат.G * ((52.0/72.0) * Tube.Шпат.CaF2 + Tube.Шпат.SiO2) * 1/100
				+ Tube.Песок.ALFA * Tube.Песок.G * Tube.Песок.SiO2 * 1/100 
				+ (102.0/54.0) * Tube.Ферросплав.ALFA * Tube.Ферросплав.G;
	
			//Тепловой баланс
			//Ждем класс для работы с теплоемкостями, энтальпиями и прочей хренью из Модуля CountVars

			//Баланс кислорода

			double LeftO2 = 
				Tube.Дутье.V * Tube.Дутье.O2 / 100 * 1.43 /1000 + Tube.Известь.ALFA * Tube.Известь.G * Tube.Известь.H2O * 16.0/18.0 * 1/100 + Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.H2O * 16.0/18.0 * 1/100
				+ Tube.Доломит.ALFA * Tube.Доломит.G * Tube.Доломит.Fe2O3 * 48.0/160.0 *1/100 + Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * (48.0/160.0 * Tube.ВлажныйДоломит.Fe2O3 + 16.0/18.0 * Tube.ВлажныйДоломит.H2O) *1/100
				+ Tube.Окатыши.ALFA * Tube.Окатыши.G * (16.0/72.0 * Tube.Окатыши.FeO + 48.0/160.0 * Tube.Окатыши.Fe2O3) * 1/100 + Tube.Руда.ALFA * Tube.Руда.G * 48.0/160.0 * Tube.Руда.Fe2O3 * 1/100
				+ Tube.Окалина.ALFA * Tube.Окалина.G * (16.0/72.0 * Tube.Окалина.FeO + 48.0/160.0 * Tube.Окалина.Fe2O3 + 16.0/71.0 * Tube.Окалина.MnO) * 1/100 + Tube.Агломерат.ALFA * Tube.Агломерат.G * (16.0/72.0 * Tube.Агломерат.FeO + 48.0/160.0 * Tube.Агломерат.Fe2O3) * 1 / 100
				+ Tube.МиксерныйШлак.G * (16.0/72.0 * Tube.МиксерныйШлак.FeO + 48.0/160.0 * Tube.МиксерныйШлак.Fe2O3 + 16.0/71.0 * Tube.МиксерныйШлак.MnO) * 1/100 + Tube.Песок.ALFA * Tube.Песок.G * Tube.Песок.H2O * 16.0/18.0 * 1/100
				+ Tube.ОставленныйШлак.G * (16.0/72.0 * Tube.ОставленныйШлак.FeO + 48.0/160.0 * Tube.ОставленныйШлак.Fe2O3 + 16.0/71.0 * Tube.ОставленныйШлак.MnO) * 1/100
				+ Tube.Имф.ALFA * Tube.Имф.G * 48.0/160.0 * Tube.Имф.Fe2O3 / 100
				+ Tube.Pack.ALFA * Tube.Pack.G * (16 / 71 * Tube.Pack.MnO + 16 / 72 * Tube.Pack.FeO + 48 / 160 * Tube.Pack.Fe2O3) * 1 / 100;

			double RightO2 = 
				(Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * (0.0038 * Tube.Сталь.C) * 1 / 100
				+ Tube.Шлак.G * (16.0/72.0 * Tube.Шлак.FeO + 48.0/160.0 * Tube.Шлак.Fe2O3 + 16.0/71.0 * Tube.Шлак.MnO) * 1/100
				+ 32.0 / 28.0 * (Tube.Чугун.G * Tube.Чугун.Si + Tube.Лом.G * Tube.Лом.Si + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.Si - (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Si) * 1/100
				+ 80.0 / 62.0 * (Tube.Чугун.G * Tube.Чугун.P + Tube.Лом.G * Tube.Лом.P + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.P + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.P - (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.P) * 1/100
				+ 16.0 / 12.0 * (Tube.Чугун.G * Tube.Чугун.C + Tube.Лом.C * Tube.Лом.P + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.C + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.C + Tube.Футеровка.G * Tube.Футеровка.C - (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.C) * 1/100
				+ Tube.Дутье.V * Params.L * 1.43 / 1000
				+ Params.alfaFe * 0.7 * 16 / 56 * (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss))
				+ 48.0 / 54.0 * Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.Al;

			//Суммарный материальный баланс

			double LeftMAT =
				Tube.Дутье.V * (Tube.Дутье.O2 * 1.43 / 1000.0 + Tube.Дутье.Ar * 1.784 / 1000.0 + Tube.Дутье.N2 * 1.25 / 1000.0) * 1.0 / 100.0 + Tube.Чугун.G + Tube.Лом.G
				+ Tube.Кокс.ALFA * Tube.Кокс.G + Tube.Известь.ALFA * Tube.Известь.G + Tube.Известняк.ALFA * Tube.Известняк.G + Tube.Доломит.ALFA * Tube.Доломит.G + Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G + Tube.Имф.ALFA * Tube.Имф.G
				+ Tube.Окатыши.ALFA * Tube.Окатыши.G  + Tube.Руда.ALFA * Tube.Руда.G + Tube.Окалина.ALFA * Tube.Окалина.G + Tube.Агломерат.ALFA * Tube.Агломерат.G + Tube.Шпат.ALFA * Tube.Шпат.G + Tube.Песок.ALFA * Tube.Песок.G
				+ Tube.МиксерныйШлак.G + Tube.ОставленныйШлак.G + Tube.Футеровка.G
				+ Tube.Ферросплав.ALFA * Tube.Ферросплав.G
				+ Tube.Pack.ALFA * Tube.Pack.G;

			double RightMAT =
				(Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) + Tube.Шлак.G +
				(28.0 / 12.0) *
				(Tube.Чугун.G * Tube.Чугун.C + Tube.Лом.G * Tube.Лом.C + Tube.Pack.G * Tube.Pack.C +
				 Tube.Кокс.ALFA * Tube.Кокс.G * Tube.Кокс.C + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.C +
				 Tube.Футеровка.G * Tube.Футеровка.C -
				 (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.C * 1.0 / 100.0
				 + Tube.Дутье.V * Params.L * 1.43 / 1000.0 + Tube.Известь.ALFA * Tube.Известь.G * Tube.Известь.H2O * 1.0 / 100.0 +
				 Tube.Песок.ALFA * Tube.Песок.G * Tube.Песок.H2O * 1.0 / 100.0
				 + Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.H2O * 1.0 / 100.0 +
				 Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.H2O * 1.0 / 100.0
				 + Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.CO2 * 1.0 / 100.0 +
				 44.0 / 100.0 * Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.CaCO3 * 1.0 / 100.0
				 + Params.alfaFe * 0.7 * (Tube.Сталь.GYield / (1.0 - Params.alfaFe - Params.StAndShlLoss)) * 16.0 / 56.0);

		   //Баланс Марганца
		   
			double LeftMn =
				Tube.Чугун.G * Tube.Чугун.Mn + Tube.Лом.G * Tube.Лом.Mn + Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.MnO * 55.0 / 71.0
				+ Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.MnO *  55.0 / 71.0 + Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.MnO *  55.0 / 71.0
				+ Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.Mn
				+ 55.0 / 71.0 * Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.MnO;

			double RightMn =
				(Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Mn + Tube.Шлак.G * Tube.Шлак.MnO * 55.0 / 71.0;

			//Баланс Кремния

			double LeftSi =
				Tube.Чугун.G * Tube.Чугун.Si + Tube.Лом.G * Tube.Лом.Si
				+ Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.SiO2 * 28.0 / 60.0 +
				Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.SiO2 * 28.0 / 60.0
				+
				28.0 / 60.0 *
				(Tube.Доломит.ALFA * Tube.Доломит.G * Tube.Доломит.SiO2 + Tube.Имф.ALFA * Tube.Имф.G * Tube.Имф.SiO2 +
				 Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.SiO2 +
				 Tube.Известь.ALFA * Tube.Известь.G * Tube.Известь.SiO2
				 + Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.SiO2 + Tube.Окатыши.ALFA * Tube.Окатыши.G * Tube.Окатыши.SiO2 +
				 Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.SiO2 + Tube.Песок.ALFA * Tube.Песок.G * Tube.Песок.SiO2
				 + Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.SiO2 + Tube.Шпат.ALFA * Tube.Шпат.G * Tube.Шпат.SiO2 +
				 Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.SiO2);

			double RightSi =
				(Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Si + Tube.Шлак.G * Tube.Шлак.SiO2 * 28.0 / 60.0;
		}

	}
}
