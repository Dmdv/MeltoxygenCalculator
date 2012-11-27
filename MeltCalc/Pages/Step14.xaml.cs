using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
		private List<GroupBox> _controls = new List<GroupBox>();

		public Step14()
		{
			InitializeComponent();
		}

		public Step14(List<Materials> selectedMaterials)
		{
			_selectedMaterials = selectedMaterials;
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
			UpdateNavigationGroupBox();
			VisualHelper.UpdateVisibility(_controls, _selectedMaterials);
			InitializeDataContext();
		}

		private void UpdateNavigationGroupBox()
		{
			_controls.Remove(_controls.Single(x => x.Tag == null));
		}

		private void InitializeDataContext()
		{
			_controls.ForEach(x => x.DataContext = new Step14Model(x));
		}

		private void CommandBindingExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			SaveChoice();
			РассчётИзвестняка();
		}

		private void CommandBindingCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
		}

		private void CommandBindingPreviousPage(object sender, ExecutedRoutedEventArgs e)
		{
		}

		private void CommandBindingCanPrevious(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void РассчётИзвестняка()
		{
		}

		/// <summary>
		/// Save variants to Save table in loose.mdb.
		/// </summary>
		private void SaveChoice()
		{
		}
	}
}