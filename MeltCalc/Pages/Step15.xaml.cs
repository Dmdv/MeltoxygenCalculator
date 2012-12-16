using System.Windows.Input;
using MeltCalc.Chemistry;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for Step15.xaml
	/// </summary>
	public partial class Step15
	{
		public Step15()
		{
			InitializeComponent();
			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
		{
			// Во избежание определения INotifyPropertyChanges в каждом компоненте придется инициализировать
			// поля из свойств вручную.

			InitializeFromGlobals();
		}

		private void InitializeFromGlobals()
		{
			_stalMass.Value = Tube.Сталь.GYield;

			if (Params.IsDuplex)
			{
				_shlak.IsEnabled = false;
				_leftShlak.IsChecked = false;
			}

			if (Params.InputForm == "auto")
			{
				_stalMass.IsEnabled = true;
				_stalMass.AllowSpin = true;
			}
			else
			{
				_stalMass.IsEnabled = false;
				_stalMass.AllowSpin = false;
			}
		}

		private void NextCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void PreviousCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
		}

		private void NextExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			double gYield = Tube.Сталь.GYield;
		}

		private void PreviousExecuted(object sender, ExecutedRoutedEventArgs e)
		{
		}
	}
}
