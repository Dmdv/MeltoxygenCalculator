using System;
using System.Windows.Input;

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

		private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (NavigationService != null) 
				NavigationService.Navigate(new Uri(@"Pages\Step1.xaml", UriKind.Relative));
		}
	}
}
