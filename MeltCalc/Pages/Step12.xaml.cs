﻿using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MeltCalc.Helpers;
using MeltCalc.Model;

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

		private void NextCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void NextExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var checkBoxs = _grid
				.FindVisualChild<CheckBox>()
				.Where(x=>x.IsChecked.HasValue && x.IsChecked.Value)
				.Select(x=>x.Tag).OfType<Materials>().ToList();

			if (NavigationService != null)
			{
				NavigationService.Navigate(new Step13(checkBoxs));
			}
		}

		private void PrevCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void PrevExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (NavigationService != null)
				NavigationService.Navigate(new Uri(@"Pages\Step11.xaml", UriKind.Relative));
		}
	}
}
