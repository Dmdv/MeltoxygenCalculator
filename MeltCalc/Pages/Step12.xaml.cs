using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MeltCalc.Helpers;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for Step12.xaml
	/// </summary>
	public partial class Step12
	{
		public Step12()
		{
			InitializeComponent();
		}

		private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			var checkBoxs = _grid.FindVisualChild<CheckBox>().Where(x=>x.IsChecked.HasValue && x.IsChecked.Value).ToList();
			if (NavigationService != null)
				NavigationService.Navigate(new Uri(@"Pages\Step13.xaml", UriKind.Relative), checkBoxs);
		}

		private void CommandBinding_CanPrevious(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CommandBinding_PreviousPage(object sender, ExecutedRoutedEventArgs e)
		{
			if (NavigationService != null)
				NavigationService.Navigate(new Uri(@"Pages\Step11.xaml", UriKind.Relative));
		}
	}
}
