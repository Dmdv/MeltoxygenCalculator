using System.Windows.Input;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for AlloyAndDensingInput.xaml
	/// </summary>
	public partial class AlloyAndDensingInput
	{
		public AlloyAndDensingInput()
		{
			InitializeComponent();
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