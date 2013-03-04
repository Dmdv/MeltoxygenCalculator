using System;
using System.Windows.Input;
using MeltCalc.Chemistry;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for StartPage.xaml
	/// </summary>
	public partial class StartPage
	{
		public StartPage()
		{
			InitializeComponent();
		}

		private void NextCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void NextExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (NavigationService != null) 
				NavigationService.Navigate(new Uri(@"Pages\Step1.xaml", UriKind.Relative));
		}

		private void SHLcount()
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
