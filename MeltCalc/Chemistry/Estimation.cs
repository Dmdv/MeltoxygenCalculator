﻿using System;
using System.Windows;
using MeltCalc.Model;

namespace MeltCalc.Chemistry
{
	public class Estimation
	{
		static Estimation()
		{
			GizvSAVE = GizkSAVE = GdolSAVE = GvldolSAVE = GimfSAVE = new double[6];
			GchugSAVE = GlomSAVE = VdutSAVE = GshlSAVE = MnOshlSAVE = GstYieldSAVE = ALFAizvSAVE = new double[6];

			//Минимальные и максимальные пределы по рассчитываемым переменным
			minimumGchug = maximumGchug = minimumGlom = maximumGlom = minimumVdut = maximumVdut = minimumGshl = maximumGshl = minimumMnOshl = maximumMnOshl = minimumPst = maximumPst = new double();
			minimumGizv = maximumGizv = minimumGizk = maximumGizk = minimumGdol = maximumGdol = minimumGvldol = maximumGvldol = minimumGimf = maximumGimf = minimumGpes = maximumGpes = minimumGkoks = maximumGkoks = minimumGokal = maximumGokal = minimumGruda = maximumGruda = minimumGokat = maximumGokat = minimumGagl = maximumGagl = minimumGshp = maximumGshp = new double();
			minimumGstYield = maximumGstYield = minimumAlfaIzv = new double();
			
			//Концентрация кремния в стали
			Tube.Сталь.Si = 0.005;

			Tube.Шлак.G = (minimumGshl + maximumGshl) / 2.0;
			Tube.Чугун.G = (minimumGchug + maximumGchug) / 2.0; 
			Tube.Лом.G = (minimumGlom + maximumGlom) / 2.0; 
			
			Tube.Сталь.P = Tube.Сталь.PMAX;
			Params.Tog = Tube.Сталь.T;

			DisbalCaO = DisbalSHL = DisbalTepl = DisbalMat = DisbalO2 = DisbalMnO = DisbalSiO2 = new double[6];

			double NeededLp = Tube.Шлак.P2O5 / Tube.Сталь.P;

			
			//(FeO)шл = a0 + a1 * B + a2 / [C] + a3 / [Mn] + a4 / [P] + a5 * T + a6 * VarBlow
			double TOTALFeOshl = a0 + a1 * Tube.Шлак.B + a2 / Tube.Сталь.C + a4 / Tube.Сталь.P + a5 * Tube.Сталь.T + a6 * AdaptationData.VArBlow;

		}

		// Специальные переменные
		public static readonly double[] GizvSAVE, GizkSAVE, GdolSAVE, GvldolSAVE, GimfSAVE, GshpSAVE;
		public static readonly double[] GchugSAVE, GlomSAVE, VdutSAVE, GshlSAVE, MnOshlSAVE, GstYieldSAVE, ALFAizvSAVE;

		//Задаваемые
		public static double minimumGchug, maximumGchug, minimumGlom, maximumGlom, minimumVdut, maximumVdut, minimumGshl, maximumGshl, minimumMnOshl, maximumMnOshl, minimumPst, maximumPst;
		public static double minimumGizv, maximumGizv, minimumGizk, maximumGizk, minimumGdol, maximumGdol, minimumGvldol, maximumGvldol, minimumGimf, maximumGimf, minimumGpes, maximumGpes, minimumGkoks, maximumGkoks, minimumGokal, maximumGokal, minimumGruda, maximumGruda, minimumGokat, maximumGokat, minimumGagl, maximumGagl, minimumGshp, maximumGshp;
		public static double minimumGstYield, maximumGstYield, minimumAlfaIzv, maximumAlfaIzv;

		// Рассчитываемые, на 1 больше, чем РАУНДов- для расчета шагов. для расчета шихты и адаптации
		public double[] minGchug, maxGchug, minGlom, maxGlom, minVdut, maxVdut, minGshl, maxGshl, minMnOshl, maxMnOshl;
		public double[] minGizv, maxGizv, minGizk, maxGizk, minGdol, maxGdol, minGvldol, maxGvldol, minGimf, maxGimf, minGpes, maxGpes, minGkoks, maxGkoks, minGokal, maxGokal, minGruda, maxGruda, minGokat, maxGokat, minGagl, maxGagl, minGshp, maxGshp;

		// То же для адаптации по второму варианту
		public double[] minGstYield, maxGstYield, minALFAizv, maxALFAizv;
	
		// Шаги переменных
		public static double[] stepGizv, stepGizk, stepGdol, stepGvldol, stepGimf, stepGshp;
		public static double[] stepGchug, stepGlom, stepVdut, stepGshl, stepMnOshl;
		public static double[] stepGstYield, stepALFAizv;

		// Дисбалансы уравнений.
		public static double[] DisbalCaO, DisbalSHL, DisbalTepl, DisbalMat, DisbalO2, DisbalMnO, DisbalSiO2;
		
		// Временные переменные для лома.
		// Низкоуглеродистый лом.
		public static double Clowsmall, Silowsmall, Mnlowsmall, Plowsmall, Slowsmall;
		public static double Clowmed, Silowmed, Mnlowmed, Plowmed, Slowmed;
		public static double Clowbig, Silowbig, Mnlowbig, Plowbig, Slowbig;

		// Среднеуглеродистый лом.
		public static double Cmidsmall, Simidsmall, Mnmidsmall, Pmidsmall, Smidsmall;
		public static double Cmidmed, Simidmed, Mnmidmed, Pmidmed, Smidmed;
		public static double Cmidbig, Simidbig, Mnmidbig, Pmidbig, Smidbig;

		// Высокоуглеродистый лом.
		public static double Chighsmall, Sihighsmall, Mnhighsmall, Phighsmall, Shighsmall;
		public static double Chighmed, Sihighmed, Mnhighmed, Phighmed, Shighmed;
		public static double Chighbig, Sihighbig, Mnhighbig, Phighbig, Shighbig;

		//Переменные для хранения коэффициентов регрессионных уравнений

		//FeO шлака 
		public static readonly double a0, a1, a2, a3, a4, a5, a6;

		//Mn стали
		public static readonly double c0, c1, c2, c3, c4, c5;

		//Lp фосфора 
		public static readonly double b0, b1, b2, b3, b4;

		private double NeededLp;
		private int IterTimes;
		private string MOVINGSide;

		// Переменная, характеризующая рассогласование балансовых уравнений и буферная переменная.
		public static double MistakeTOTAL, Compare, SumDisbal;

		private void Run()
		{
			Tube.Сталь.Si = 0.005;
			Prepare1_REGRESSLOAD();

			if (Tube.Шлакообразующий == Materials.Известь)
			{
				Tube.Известь.G = maximumGizv;
			}
			if (Tube.Шлакообразующий == Materials.Известняк)
			{
				Tube.Известь.G = maximumGizk;
			}

			Tube.Шлак.G = (minimumGshl + maximumGshl) / 2.0;
			Tube.Чугун.G = (minimumGchug + maximumGchug) / 2.0;
			Tube.Лом.G = (minimumGlom + maximumGlom) / 2.0;
			Tube.Сталь.P = Tube.Сталь.PMAX;
			Params.Tog = Tube.Сталь.T;

			Calculate_P2O5shl_Bal_P();

			NeededLp = Tube.Шлак.P2O5 / Tube.Сталь.P;

			for (Tube.Шлак.B = Tube.Шлак.Bmin; Tube.Шлак.B < Tube.Шлак.Bmax; Tube.Шлак.B += 0.05)
			{
				CALCULATE_REGRESSFeOMnO();
				CALCULATE_RegressLp();
				if (Params.Lp > NeededLp)
					break;
			}

			CALCULATE_TEPLCONSTANTS();

			IterTimes = 0;

			do
			{
				CALCULATE_REGRESSFeOMnO();
				CALCULATE_RegressLp();
				Estimate();

			} while (++IterTimes < 2);

			//Рассчет концентрации серы в стали
			Tube.Сталь.S = 0.8 *
						   (Tube.Чугун.G * Tube.Чугун.S + Tube.Лом.G * Tube.Лом.S + Tube.Pack.ALFA * Tube.Pack.S * Tube.Pack.G) /
						   (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss));

			SumDisbal = DisbalCaO[Params.Round - 1] + DisbalMat[Params.Round - 1] + DisbalSHL[Params.Round - 1] +
			            DisbalTepl[Params.Round - 1] + DisbalO2[Params.Round - 1] + DisbalMnO[Params.Round - 1];

			if (SumDisbal * 100 > 5)
			{
				const string Msg = "Возможно, результаты данного расчета некорректны.\r\nСуммарное расхождение балансовых уравнений составляет ";
				MessageBox.Show(string.Format("{0} '{1}'", Msg, SumDisbal * 100), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
			}

			if (Tube.Сталь.P > Tube.Сталь.PMAX)
			{
				const string Msg = "При заданных условиях требуемая дефосфорация не может быть достигнута.\r\n" + 
				                   "Попробуйте увеличить допустимую основность шлака";
				MessageBox.Show(Msg, "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
			}

			if (Tube.Известь.G < 0 || Tube.Известняк.G < 0 || Tube.ВлажныйДоломит.G < 0 || Tube.Доломит.G < 0 || Tube.Имф.G < 0 || Tube.Шпат.G < 0)
			{
				Params.LessThanZero = true;
			}
			else
			{
				Params.LessThanZero = false;
			}

			// TODO: Перейти на результаты.
		}

		private void Estimate()
		{
			Params.OkPst = false;
			do
			{
				Compare = 1;
				if (IterTimes == 0)
				{
					minGizv[0] = minimumGizv;
					maxGizv[0] = maximumGizv;

					minGizk[0] = minimumGizk;
					maxGizk[0] = maximumGizk;

					minGdol[0] = minimumGdol;
					maxGdol[0] = maximumGdol;

					minGvldol[0] = minimumGvldol;
					maxGvldol[0] = maximumGvldol;

					minGimf[0] = minimumGimf;
					maxGimf[0] = maximumGimf;

					minGshp[0] = minimumGshp;
					maxGshp[0] = maximumGshp;

					minGizv[0] = minimumGizv;
					maxGizv[0] = maximumGizv;

					minGchug[0] = minimumGchug;
					maxGchug[0] = maximumGchug;

					minGlom[0] = minimumGlom;
					maxGlom[0] = maximumGlom;

					minVdut[0] = minimumVdut;
					maxVdut[0] = maximumVdut;

					minGshl[0] = minimumGshl;
					maxGshl[0] = maximumGshl;

					minMnOshl[0] = minimumMnOshl;
					maxMnOshl[0] = maximumMnOshl;
				}

				if (IterTimes >= 1)
				{
					minGizv[0] = GizvSAVE[5] - stepGizv[1];
					minGizk[0] = GizkSAVE[5] - stepGizk[1];
					minGdol[0] = GdolSAVE[5] - stepGdol[1];

					minGvldol[0] = GvldolSAVE[5] - stepGvldol[1];
					minGimf[0] = GimfSAVE[5] - stepGimf[1];
					minGshp[0] = GshpSAVE[5] - stepGshp[0];

					minGchug[0] = GchugSAVE[5] - stepGchug[1];
					minGlom[0] = GlomSAVE[5] - stepGlom[1];
					minVdut[0] = VdutSAVE[5] - stepVdut[1];
					minGshl[0] = GshlSAVE[5] - stepGshl[1];
					minMnOshl[0] = MnOshlSAVE[5] - stepMnOshl[0];

					maxGizv[0] = GizvSAVE[5] + stepGizv[1];
					maxGizk[0] = GizkSAVE[5] + stepGizk[1];
					maxGdol[0] = GdolSAVE[5] + stepGdol[1];
					maxGvldol[0] = GvldolSAVE[5] + stepGvldol[1];
					maxGimf[0] = GimfSAVE[5] + stepGimf[1];
					maxGshp[0] = GshpSAVE[5] + stepGshp[0];

					maxGchug[0] = GchugSAVE[5] + stepGchug[1];
					maxGlom[0] = GlomSAVE[5] + stepGlom[1];
					maxVdut[0] = VdutSAVE[5] + stepVdut[1];
					maxGshl[0] = GshlSAVE[5] + stepGshl[1];
					maxMnOshl[0] = MnOshlSAVE[5] + stepMnOshl[0];
				}

				// Расчет шагов сканирования для всех уточнений.

				// Для 1 шага.

				stepGizv[0] = (maxGizv[0] - minGizv[0]) / 4.0;
				stepGizk[0] = (maxGizk[0] - minGizk[0]) / 4.0;
				stepGdol[0] = (maxGdol[0] - minGdol[0]) / 4.0;
				stepGvldol[0] = (maxGvldol[0] - minGvldol[0]) / 4.0;
				stepGimf[0] = (maxGimf[0] - minGimf[0]) / 4.0;
				stepGshp[0] = (maxGshp[0] - minGshp[0]) / 4.0;
				stepGchug[0] = (maxGchug[0] - minGchug[0]) / 4.0;
				stepGlom[0] = (maxGlom[0] - minGlom[0]) / 4.0;
				stepVdut[0] = (maxVdut[0] - minVdut[0]) / 4.0;
				stepGshl[0] = (maxGshl[0] - minGshl[0]) / 4.0;
				stepMnOshl[0] = (maxMnOshl[0] - minMnOshl[0]) / 4.0;

				// Для 2 - 6 шагов.
				for (var i = 1; i < 6; i++)
				{
					stepGizv[i] = stepGizv[i - 1] / 2.0;
					stepGizk[i] = stepGizk[i - 1] / 2.0;
					stepGdol[i] = stepGdol[i - 1] / 2.0;
					stepGvldol[i] = stepGvldol[i - 1] / 2.0;
					stepGimf[i] = stepGimf[i - 1] / 2.0;
					stepGshp[i] = stepGshp[i - 1] / 2.0;
					stepGchug[i] = stepGchug[i - 1] / 2.0;
					stepGlom[i] = stepGlom[i - 1] / 2.0;
					stepVdut[i] = stepVdut[i - 1] / 2.0;
					stepGshl[i] = stepGshl[i - 1] / 2.0;
					stepMnOshl[i] = stepMnOshl[i - 1] / 2.0;
				}

				for (Params.Round = 0; Params.Round < 6; Params.Round++)
				{
					Tube.Чугун.G = minGchug[Params.Round];
					Tube.Лом.G = minGlom[Params.Round];
					Tube.Дутье.V = minVdut[Params.Round];
					Tube.Шлак.G = minGshl[Params.Round];
					Tube.Шлак.MnO = minMnOshl[Params.Round];

					if (Tube.Шлакообразующий == Materials.Известь)
					{
						Tube.Известь.G = minGizv[Params.Round];
						do
						{
							Other6Circles();
							Tube.Известь.G += stepGizv[Params.Round];

						} while (Tube.Известь.G <= maxGizv[Params.Round]);
					}

					if (Tube.Шлакообразующий == Materials.Известняк)
					{
						Tube.Известняк.G = minGizk[Params.Round];
						do
						{
							Other6Circles();
							Tube.Известняк.G += stepGizk[Params.Round];

						} while (Tube.Известняк.G <= maxGizk[Params.Round]);
					}

					if (Tube.Шлакообразующий == Materials.Доломит)
					{
						Tube.Доломит.G = minGdol[Params.Round];
						do
						{
							Other6Circles();
							Tube.Доломит.G += stepGdol[Params.Round];

						} while (Tube.Доломит.G <= maxGdol[Params.Round]);
					}

					if (Tube.Шлакообразующий == Materials.ВлажныйДоломит)
					{
						Tube.ВлажныйДоломит.G = minGvldol[Params.Round];
						do
						{
							Other6Circles();
							Tube.ВлажныйДоломит.G += stepGvldol[Params.Round];

						} while (Tube.ВлажныйДоломит.G <= maxGvldol[Params.Round]);
					}

					if (Tube.Шлакообразующий == Materials.ПлавиковыйШпат)
					{
						Tube.Шпат.G = minGshp[Params.Round];
						do
						{
							Other6Circles();
							Tube.Шпат.G += stepGshp[Params.Round];

						} while (Tube.Шпат.G <= maxGshp[Params.Round]);
					}

					if (Tube.Шлакообразующий == Materials.ИзвестковоМагнитныйФлюс)
					{
						Tube.Имф.G = minGimf[Params.Round];
						do
						{
							Other6Circles();
							Tube.Имф.G += stepGimf[Params.Round];

						} while (Tube.Имф.G <= maxGimf[Params.Round]);
					}

					// TODO:

				}

			} while (!Params.OkPst);
		}

		private void Other6Circles()
		{
			throw new NotImplementedException();
		}

		private void Prepare1_REGRESSLOAD()
		{ 
			//Загрузка коэффициентов регрессионных уравнений по (FeO), [Mn], Lp
		}

		public static void CALCULATE_TEPLCONSTANTS()
		{
			Cp.izv = 4.1868 * 1000 * (11.86 + 0.00108 * (Params.AirTemp + 273) - 166000.0 / Math.Pow((Params.AirTemp + 273), 2) / 44.0);
			Cp.izk = 4.1868 * 1000 * (24.98 + 0.00524 * (Params.AirTemp + 273) - 620000.0 / Math.Pow((Params.AirTemp + 273),2) / 100.0);
			Cp.ruda = 4.1868 * 1000 * (31.7 + 0.00176 * (Params.AirTemp + 273)) * 1 / 160;
			Cp.shp = 4.1868 * 1000 * (25.81 + 0.0025 * (Params.AirTemp + 273)) * 1 / 78;
			Cp.okal = 4.1868 * 1000 * 48 * 1 / 232;
			Cp.okat = 4.1868 * 1000 * 48 * 1 / 232;
			Cp.agl = 4.1868 * 1000 * (31.7 + 0.00176 * (Params.AirTemp + 273)) * 1 / 160;
			Cp.koks = 930;
			Cp.pes = 930;
			Cp.dol = 930; 
			Cp.vldol = 930;
			Cp.IMF = 930;
			Cp.Alloy = 930;
			Cp.Densing = 930;

			Cp.H2O = 4.1868 * 1000 * (-32.344 + 10.96279 * Math.Log(Params.Tog + 273)) / 18;
			Cp.CO = 1000 * (0.0000823 * (Params.Tog + 273) + 1.0277021);
			Cp.CO2 = 1000 * (0.0001254 * (Params.Tog + 273) + 0.9673658);
			Cp.N2 = 1000 * (0.0000829 * (Params.Tog + 273) + 1.0136983); 
			Cp.Ar = 521;
			Cp.FeO = 951;
			Cp.Fe = 770;

			//Теплоемкость дутья (продувка металла воздухом, содержащим Ar, N2 и O2)
			Cp.DUT = (Cp.Ar * Tube.Дутье.Ar + (1000 * (0.0000829 * (-150 + 273) + 1.0136983)) * Tube.Дутье.N2 + (1000 * (0.0000698 * (-150 + 273) + 0.9478846)) * Tube.Дутье.O2) * 1 / 100;

			Hp.dHshl = 4.1868 * 1000 * (0.289 * Tube.Сталь.T + 50);
			Hp.dHmshl = (0.117 * Tube.Чугун.T + 1.015 * Math.Pow(Tube.Чугун.T, 2) / 1000) * 1000;
			Hp.dHostshl = Math.Abs(4.1868 * 1000 * (0.289 * (Tube.Сталь.T - 20 * Params.TAUprostREAL) + 50));

			Hp.dHchug = 4.1868 * 1000 * (14.8 + 0.21 * Tube.Чугун.T);
			Hp.dHlom = (0.0003 * Params.AirTemp + 0.4288) * (Params.AirTemp + 273) * 1000.0;
		}

		private void CALCULATE_REGRESSFeOMnO()
		{
			Tube.Шлак.FeO = 0.701145 * Tube.Шлак.TOTALFeO - 0.586142;
			Tube.Шлак.Fe2O3 = Tube.Шлак.TOTALFeO - Tube.Шлак.FeO;

			//[Mn]ст = c0 + c1 * B + c2 * (FeO) + c3 / [C] + c4 * T + c5 * VarBlow
			Tube.Сталь.Mn = c0 + c1 * Tube.Шлак.B + c2 * Tube.Шлак.TOTALFeO + c3 / Tube.Сталь.C + c4 * Tube.Сталь.T + c5 * AdaptationData.VArBlow;
		}

		private void CALCULATE_RegressLp()
		{
			//Lp = b0 + b1 * B + b2 * (FeO) + b3 * T + b4 *VarBlow
			Params.Lp = b0 + b1 * Tube.Шлак.B + b2 * Tube.Шлак.TOTALFeO + b3 * Tube.Сталь.T + b4 * AdaptationData.VArBlow;
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

			double LeftSHL = Tube.Шлак.G;

			//Тепловой баланс

			double LeftTEPL =
			Cp.ChugLiquid * 1000 * (Tube.Чугун.T + 273) * Tube.Чугун.G + Cp.LomSolid * 1000 * (Params.AirTemp + 273) * Tube.Лом.G + Tube.Pack.dH * Tube.Pack.ALFA * Tube.Pack.G + Hp.dHmshl * Tube.МиксерныйШлак.G + Hp.dHostshl * Tube.ОставленныйШлак.G
			+ Tube.Дутье.V * (-150.0 + 273.0) * (1.43 / 1000 * Cp.DUT) + AdaptationData.VArBlow * 1.784 / 1000 * Cp.Ar * (Params.AirTemp + 273)
			+ Tube.Известь.ALFA * Tube.Известь.G * Cp.izv * (Params.AirTemp + 273) + Tube.Известняк.ALFA * Tube.Известняк.G * Cp.izk + Tube.Доломит.ALFA * Tube.Доломит.G * Cp.dol * (Params.AirTemp + 273) + Tube.Кокс.ALFA * Tube.Кокс.G * Cp.koks * (Params.AirTemp + 273)
			+ Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Cp.vldol * (Params.AirTemp + 273) + Tube.Имф.ALFA * Tube.Имф.G * Cp.IMF * (Params.AirTemp + 273)
			+ Tube.Окатыши.ALFA * Tube.Окатыши.G * Cp.okat * (Params.AirTemp + 273) + Tube.Руда.ALFA * Tube.Руда.G * Cp.ruda * (Params.AirTemp + 273) + Tube.Окалина.ALFA * Tube.Окалина.G * Cp.okal * (Params.AirTemp + 273)
			+ Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Cp.Alloy * (Params.AirTemp + 273) + Tube.Известь.ALFA * AdaptationData.GDensing * Cp.Densing * (Params.AirTemp + 273)
			+ Tube.Агломерат.ALFA * Tube.Агломерат.G * Cp.agl * (Params.AirTemp + 273) + Tube.Шпат.ALFA * Tube.Шпат.G * Cp.shp * (Params.AirTemp + 273) + Tube.Песок.ALFA * Tube.Песок.G * Cp.pes * (Params.AirTemp + 273)
			+ -Hp.dHsio2_2caosio2 * 1000 * 60.0 / 28.0 * (Tube.Чугун.G * Tube.Чугун.Si + Tube.Лом.G * Tube.Лом.Si + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.Si - (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Si) * 1 / 100
			+ -Hp.dHsio2_2caosio2 * 1000 * (Tube.Известь.ALFA * Tube.Известь.G * Tube.Известь.SiO2 + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.SiO2 + Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.SiO2 + Tube.Доломит.ALFA * Tube.Доломит.G * Tube.Доломит.SiO2
			+ Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.SiO2 + Tube.Имф.ALFA * Tube.Имф.G * Tube.Имф.SiO2 + Tube.Окатыши.ALFA * Tube.Окатыши.G * Tube.Окатыши.SiO2
			+ Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.SiO2 + Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.SiO2 + Tube.Шпат.ALFA * Tube.Шпат.G * Tube.Шпат.SiO2 + Tube.Песок.ALFA * Tube.Песок.G * Tube.Песок.SiO2) * 1 / 100
			+ -Hp.dHp2o5_3caop2o5 * 1000 * (142 / 62 * Tube.Чугун.G * Tube.Чугун.P + 142 / 62 * Tube.Лом.G * Tube.Лом.P + 142 / 62 * Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.P + 142 / 62 * Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.P + Tube.Футеровка.G * Tube.Футеровка.P2O5
			+ 142 / 62 * (Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.P + Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.P) + Tube.Известь.ALFA * Tube.Известь.G * Tube.Известь.P2O5 + Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.P2O5
			+ Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.P2O5 + Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.P2O5 - 142 / 62 * (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.P) * 1 / 100
			+ -Hp.dHsi_O2_mol / 28 * 1000 * (Tube.Чугун.G * Tube.Чугун.Si + Tube.Лом.G * Tube.Лом.Si + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.Si - (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Si) * 1 / 100
			+ -Hp.dHmn_O2_mol / 55 * 1000 * (Tube.Чугун.G * Tube.Чугун.Mn + Tube.Лом.G * Tube.Лом.Mn + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.Mn - (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.Mn) * 1 / 100
			+ -Hp.dHmn_O2_mol / 31 * 1000 * (Tube.Чугун.G * Tube.Чугун.P + Tube.Лом.G * Tube.Лом.P + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.P + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.P - (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.P) * 1 / 100
			+ -Hp.dHfe_O2_mol / 56 * 1000 * 56 / 72 * (Tube.Шлак.G * Tube.Шлак.FeO - Tube.Окатыши.ALFA * Tube.Окатыши.G * Tube.Окатыши.FeO - Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.FeO
			- Tube.Агломерат.ALFA * Tube.Агломерат.G * Tube.Агломерат.FeO - Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.FeO - Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.FeO - Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.FeO) * 1 / 100
			+ -Hp.dHfe_fe2o3_o2_mol / 56 * 1000 * 112 / 160 * (Tube.Шлак.G * Tube.Шлак.Fe2O3 - Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.Fe2O3 - Tube.Окатыши.ALFA * Tube.Окатыши.G * Tube.Окатыши.Fe2O3
			- Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.Fe2O3 - Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.Fe2O3 - Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.Fe2O3 - Tube.Агломерат.ALFA * Tube.Агломерат.G * Tube.Агломерат.Fe2O3
			- Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.Fe2O3 - Tube.Доломит.ALFA * Tube.Доломит.G * Tube.Доломит.Fe2O3 - Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.Fe2O3 - Tube.Имф.ALFA * Tube.Имф.G * Tube.Имф.Fe2O3) * 1 / 100
			+ -Hp.dHc_O2_mol / 12 * 1000 * (Tube.Чугун.G * Tube.Чугун.C + Tube.Лом.G * Tube.Лом.C + Tube.Футеровка.G * Tube.Футеровка.C + Tube.Кокс.ALFA * Tube.Кокс.G * Tube.Кокс.C + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.C + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.C - (Tube.Сталь.GYield / (1 - Params.alfaFe - Params.StAndShlLoss)) * Tube.Сталь.C) * 1 / 100
			+ -Hp.dHco_co2_mol / 16 * Tube.Дутье.V * Params.L * 1.43;

			double RightTEPL = 
				Cp.Met * 1000 * (Tube.Сталь.T + 273) * (Tube.Сталь.GYield/(1 - Params.alfaFe - Params.StAndShlLoss)) + Hp.dHshl * Tube.Шлак.G + Hp.dHizkPlavl * 1000 * (Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.CaCO3 + 100 / 56 * Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.CaO) * 1 / 100 
				+ -Hp.dHfe_O2_mol * 1000 / 72 * (Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.FeO + Tube.Окатыши.ALFA * Tube.Окатыши.G * Tube.Окатыши.FeO + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.FeO
				+ Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.FeO + Tube.Агломерат.ALFA * Tube.Агломерат.G * Tube.Агломерат.FeO + Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.FeO) * 1 / 100
				+ -2 * Hp.dHfe_fe2o3_o2_mol * 1000 / 160 * (Tube.Доломит.ALFA * Tube.Доломит.G * Tube.Доломит.Fe2O3 + Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.Fe2O3 + Tube.Окатыши.ALFA * Tube.Окатыши.G * Tube.Окатыши.Fe2O3
				+ Tube.Руда.ALFA * Tube.Руда.G * Tube.Руда.Fe2O3 + Tube.Окалина.ALFA * Tube.Окалина.G * Tube.Окалина.Fe2O3 + Tube.Агломерат.ALFA * Tube.Агломерат.G * Tube.Агломерат.Fe2O3
				+ Tube.МиксерныйШлак.G * Tube.МиксерныйШлак.Fe2O3 + Tube.ОставленныйШлак.G * Tube.ОставленныйШлак.Fe2O3 + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.Fe2O3 + Tube.Имф.ALFA * Tube.Имф.G * Tube.Имф.Fe2O3) * 1 / 100
				+ Tube.Известь.ALFA * AdaptationData.GDensing * Cp.Densing * (Tube.Сталь.T + 273)
				+ Cp.CO * (Params.Tog + 273) * 28 / 12 * (Tube.Чугун.G * Tube.Чугун.C + Tube.Лом.G * Tube.Лом.C + Tube.Кокс.ALFA * Tube.Кокс.G * Tube.Кокс.C + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.C + Tube.Ферросплав.ALFA * Tube.Ферросплав.G * Tube.Ферросплав.C + Tube.Футеровка.G * Tube.Футеровка.C - (Tube.Сталь.GYield/(1 - Params.alfaFe - Params.StAndShlLoss)) * (Tube.Сталь.C) - 12 / 16 * Tube.Дутье.V  * Params.L * 1.43 / 1000 * 100) * 1 / 100 
				+ Cp.CO2 * (Params.Tog + 273) * 44 / 16 * Tube.Дутье.V  * Params.L * 1.43 / 1000 + AdaptationData.VArBlow * 1.784 / 1000 * Cp.Ar * (Params.Tog + 273) 
				+ Cp.H2O * (Params.Tog + 273) * (Tube.Известь.ALFA * Tube.Известь.G * Tube.Известь.H2O + Tube.Известняк.ALFA * Tube.Известняк.G * Tube.Известняк.H2O +  Tube.Песок.ALFA * Tube.Песок.G * Tube.Песок.H2O + Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.H2O) * 1 / 100 
				+ Cp.CO2 * (Params.Tog + 273) * (Tube.Известняк.ALFA * Tube.Известняк.G * 44 / 100 * Tube.Известняк.CaCO3 + Tube.ВлажныйДоломит.ALFA * Tube.ВлажныйДоломит.G * Tube.ВлажныйДоломит.CO2) * 1 / 100 
				+ Params.TeplFutLoss * (Params.TAUprostREAL + Params.TAPtime + Params.BlowingTime) / (Params.TAUprost + Params.TAPtime + Params.BlowingTime) 
				+ Hp.dHlomPlavl * 1000 * Tube.Лом.G 
				+ Params.alfaFe * (Tube.Чугун.G * (100 - Tube.Чугун.C - Tube.Чугун.Si - Tube.Чугун.Mn - Tube.Чугун.P - Tube.Чугун.S) + Tube.Лом.G  * (100 - Tube.Лом.C - Tube.Лом.Si - Tube.Лом.Mn - Tube.Лом.P - Tube.Лом.S) + Tube.Pack.ALFA * Tube.Pack.G * Tube.Pack.Fe) 
				* (0.7 * 72 / 56 * Cp.FeO * (Params.Tog + 273) + 0.3 * Cp.Fe * (Params.Tog + 273)) * 1 / 100;

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
