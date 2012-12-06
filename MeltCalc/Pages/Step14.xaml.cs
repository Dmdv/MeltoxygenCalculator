using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeltCalc.Chemistry;
using MeltCalc.Controls;
using MeltCalc.Helpers;
using MeltCalc.Model;
using MeltCalc.ViewModel;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for Step14.xaml
	/// </summary>
	public partial class Step14
	{
		private readonly List<Materials> _selectedMaterials;
		private readonly Materials _shlak;
		private List<GroupBox> _controls = new List<GroupBox>();

		public Step14()
		{
			InitializeComponent();
		}

		public Step14(List<Materials> selectedMaterials, Materials shlak)
		{
			_selectedMaterials = selectedMaterials;
			_shlak = shlak;
			InitializeComponent();
			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			Loaded -= OnLoaded;
			// TODO: Сделать инициализацию из диспатчера.
			InitializeGroupContainers();
		}

		private void InitializeGroupContainers()
		{
			_controls.Clear();
			_controls = _grid.FindVisualChild<GroupBox>().ToList();
			RemoveUntaggedGroupBox();
			VisualHelper.UpdateVisibility(_controls, _selectedMaterials);
			InitializePresenter();
		}

		private void RemoveUntaggedGroupBox()
		{
			_controls.Remove(_controls.Single(x => x.Tag == null));
		}

		private void InitializePresenter()
		{
			_controls.ForEach(x => new Step14Model(x));
		}

		private void NextExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			SaveChoice();
			Calculations();
			LooseMass();
			RestoreDefaultSteelMass();
			ShlakDensing();

			if (AdaptationData.DensingUse && AdaptationData.AlloyUse)
			{
				if (NavigationService != null)
				{
					NavigationService.Navigate(new AlloyAndDensingInput());
				}
			}
			else
			{
				// Step 15
			}
		}

		private void ShlakDensing()
		{
			if (useShlakDensing.IsChecked.HasValue && useShlakDensing.IsChecked.Value)
			{
				AdaptationData.DensingUse = true;
			}
			else
			{
				AdaptationData.DensingUse = false;
				AdaptationData.GDensing = 0.0;
			}

			if (useFerroSplav.IsChecked.HasValue && useFerroSplav.IsChecked.Value)
			{
				AdaptationData.AlloyUse = true;
			}
			else
			{
				AdaptationData.AlloyUse = false;
				Tube.Ферросплав.G = 0.0;
			}
		}

		private void RestoreDefaultSteelMass()
		{
			Tube.Сталь.GYield = Tube.Сталь.GYieldmemo;
		}

		private void NextCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void PreviousExecuted(object sender, ExecutedRoutedEventArgs e)
		{
		}

		private void PreviousCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void LooseMass()
		{
			_selectedMaterials.Where(material => material != _shlak).ToList().ForEach(LooseMass);
		}

		private static void LooseMass(Materials material)
		{
			var result = false;
			var mass = 0.0;

			while (!result)
			{
				var dialog = new InputBox
				{
					Caption = string.Format("Введите массу {0} в килограммах:", material.ToGenitive()),
				    ShowInTaskbar = false,
				    Topmost = true
				};

				var showDialog = dialog.ShowDialog();
				if (!showDialog.HasValue || !showDialog.Value) return;
				
				result = double.TryParse(dialog.ResponseText, NumberStyles.Number, CultureInfo.InstalledUICulture, out mass);
			}

			var substance = Tube.FindSubstance<Навеска>(material);
			substance.G = mass;
		}

		private void Calculations()
		{
			// Расчет хс известняка
			Tube.Известняк.CaO = (56.0 / 100.0) * Tube.Известняк.CaCO3;
			Tube.Известняк.CO2 = (56.0 / 100.0) * Tube.Известняк.CaCO3;

			Tube.Окалина.FeO = (72.0 / 232.0) * Tube.Окалина.Fe3O4;
			Tube.Окалина.Fe2O3 = (160.0 / 232.0) * Tube.Окалина.Fe3O4;

			Tube.Шпат.CaO = (56.0 / 72.0) * Tube.Шпат.CaF2;
		}

		/// <summary>
		/// Save variants to Save table in loose.mdb.
		/// </summary>
		private void SaveChoice()
		{
			//TODO: SaveChoice.
		}
	}
}