using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for Step12.xaml
	/// </summary>
	public partial class Step13
	{
		public Step13()
		{
			InitializeComponent();
		}

		private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (NavigationService != null)
				NavigationService.Navigate(new Uri(@"Pages\Step14.xaml", UriKind.Relative));
		}

		private void CommandBinding_CanPrevious(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CommandBinding_PreviousPage(object sender, ExecutedRoutedEventArgs e)
		{
			if (NavigationService != null)
				NavigationService.Navigate(new Uri(@"Pages\Step12.xaml", UriKind.Relative));
		}
	}
}
