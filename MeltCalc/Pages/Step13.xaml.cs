using System;
using System.Collections.Generic;
using System.Linq;
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
		private readonly List<Materials> _disabled = new List<Materials>
		                                             	{
		                                             		Materials.Кокс,
		                                             		Materials.Песок,
		                                             		Materials.Окатыши,
		                                             		Materials.Руда,
		                                             		Materials.Окалина,
		                                             		Materials.Агломерат,
		                                             		Materials.ПлавиковыйШпат
		                                             	};

		private readonly List<Materials> _selectedMaterials;

		public Step13()
		{
			InitializeComponent();
		}

		public Step13(List<Materials> selectedMaterials)
		{
			InitializeComponent();
			_selectedMaterials = selectedMaterials;
			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			Loaded -= OnLoaded;

			var controls = _grid.FindVisualChild<RadioButton>().ToList();

			VisualHelper.UpdateVisibility(controls, _selectedMaterials);
			VisualHelper.UpdateEnabled(controls, _disabled);

			DefaultValues(controls);
		}

		private void DefaultValues(IEnumerable<RadioButton> buttons)
		{
			if (_selectedMaterials.Contains(Materials.Известь))
			{
				var checkBox = (RadioButton) buttons.FindByMaterial(Materials.Известь);
				checkBox.IsChecked = true;
			}
			else if (_selectedMaterials.Contains(Materials.Доломит))
			{
				var checkBox = (RadioButton) buttons.FindByMaterial(Materials.Доломит);
				checkBox.IsChecked = true;
			}
		}

		private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (NavigationService != null)
			{
				NavigationService.Navigate(new Step14(_selectedMaterials));
			}
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