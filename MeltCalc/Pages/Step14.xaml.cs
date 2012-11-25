using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MeltCalc.Helpers;
using MeltCalc.Model;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for Step14.xaml
	/// </summary>
	public partial class Step14
	{
		private readonly List<Materials> _selectedMaterials;

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
			var controls = _grid.FindVisualChild<GroupBox>().ToList();
			VisualHelper.UpdateVisibility(controls, _selectedMaterials);
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
