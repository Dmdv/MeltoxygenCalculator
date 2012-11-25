using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
			var mdb = new LooseMdb();
			var table = mdb.Reader.FetchTable("Save");
			var uu = table.Rows[0][Materials.Известь.ToName()];

			Loaded -= OnLoaded;
			InitializeGroupContainers();
		}

		private void InitializeGroupContainers()
		{
			_controls.Clear();
			_controls = _grid.FindVisualChild<GroupBox>().ToList();
			VisualHelper.UpdateVisibility(_controls, _selectedMaterials);

			InitializeDataContext();
		}

		private void InitializeDataContext()
		{
			_controls.ForEach(x => x.DataContext = new Step14Model(x));
		}
	}
}