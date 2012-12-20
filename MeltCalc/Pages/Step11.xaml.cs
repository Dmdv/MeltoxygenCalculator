using System;
using System.Windows.Controls;
using System.Windows.Input;
using MeltCalc.Chemistry;
using MeltCalc.Helpers;
using MeltCalc.Model;

namespace MeltCalc.Pages
{
	/// <summary>
	/// Interaction logic for Step11.xaml
	/// </summary>
	public partial class Step11
	{
		private readonly ParamsMdb _params = new ParamsMdb();

		public Step11()
		{
			InitializeComponent();
			Loaded += Step11_Loaded;
		}

		void Step11_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			PlantNames_Load();
			FutType_Load();

			Tube.Дутье.N2 = 0.042;
			Tube.Дутье.Ar = 0.226;
			Tube.Дутье.O2 = 100 - Tube.Дутье.N2 - Tube.Дутье.Ar;

			_o2.Text = string.Format("{0:0.###}", Tube.Дутье.O2);
			_ar.Text = string.Format("{0:0.###}", Tube.Дутье.Ar);
			_n2.Text = string.Format("{0:0.###}", Tube.Дутье.N2);
		}

		private void FutTypeChanged(object sender, SelectionChangedEventArgs e)
		{
			FutChem_Load();
		}

		public int SelectedFuter
		{
			get { return _futType.SelectedIndex; }
		}

		private void FutChem_Load()
		{
			var table = _params.Reader.FetchTable("futdata");

			Tube.Футеровка.Al2O3	= table.Rows[SelectedFuter]["Al2O3"].ToString().ToDoubleSafe();
			Tube.Футеровка.C		= table.Rows[SelectedFuter]["C"].ToString().ToDoubleSafe();
			Tube.Футеровка.CaO		= table.Rows[SelectedFuter]["CaO"].ToString().ToDoubleSafe();
			Tube.Футеровка.MgO		= table.Rows[SelectedFuter]["MgO"].ToString().ToDoubleSafe();
			Tube.Футеровка.P2O5		= table.Rows[SelectedFuter]["P2O5"].ToString().ToDoubleSafe();
			Tube.Футеровка.SiO2		= table.Rows[SelectedFuter]["SiO2"].ToString().ToDoubleSafe();

			//TODO:
			// Изменение полей газов и next_click.
		}

		private void FutType_Load()
		{
			_params.FillComboBox("futdata", "Наименование футеровки", _futType);
		}

		private void PlantNames_Load()
		{
			_params.FillComboBox("countdata", "Производитель", _names);
		}

		private void NextCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void NextExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (NavigationService != null)
			{
				NavigationService.Navigate(new Uri(@"Pages\Step12.xaml", UriKind.Relative));
			}
		}

		private void PrevCanPrevious(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void PrevExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (NavigationService != null)
			{
				NavigationService.Navigate(new Uri(@"Pages\Step1.xaml", UriKind.Relative));
			}
		}
	}
}
