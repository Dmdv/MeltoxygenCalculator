using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeltCalc.Helpers;
using MeltCalc.ViewModel;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for AlloyAndDensingInput.xaml
	/// </summary>
	public partial class AlloyAndDensingInput
	{
		private AlloyAndDensingPresenter _presenter;

		public AlloyAndDensingInput()
		{
			InitializeComponent();
			Loaded += OnLoad;
		}

		private void OnLoad(object sender, RoutedEventArgs e)
		{
			var values = _values.FindVisualChild<TextBox>().ToList();
			_presenter = new AlloyAndDensingPresenter(_alloys, values);
		}

		private void NextCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void NextExecuted(object sender, ExecutedRoutedEventArgs e)
		{
		}
	}
}