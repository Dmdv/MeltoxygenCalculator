using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeltCalc.Helpers;
using MeltCalc.Model;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for Step12.xaml
	/// </summary>
	public partial class Step13
	{
		private readonly List<Materials> _checkBoxes;

		public Step13()
		{
			InitializeComponent();
			Loaded += OnLoaded;
		}

		public Step13(List<Materials> checkBoxes)
		{
			InitializeComponent();
			_checkBoxes = checkBoxes;
			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			Loaded -= OnLoaded;

			var radioButtons = _grid
				.FindVisualChild<RadioButton>()
				.Where(x => !_checkBoxes.Contains((Materials) x.Tag));

			foreach (var radioButton in radioButtons)
			{
				radioButton.Visibility = Visibility.Collapsed;
			}
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
