using System;
using System.Globalization;
using System.Linq;
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
			Loaded += OnLoaded;
		}

		private int SelectedFuter
		{
			get { return _futType.SelectedIndex; }
		}

		private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
		{
			PlantNames_Load();
			FutType_Load();
			UpdateAir(0.042, 0.226);
			ShowComposition();
			InitNumPlavok();
			InitProduvTime();
		}

		private void InitProduvTime()
		{
			foreach (var idx in Enumerable.Range(15, 11))
			{
				_timeProduv.Items.Add(idx);
			}

			_timeProduv.SelectedValue = 20;
		}

		private void InitNumPlavok()
		{
			for (var idx = 400; idx <= 5000; idx += 200)
			{
				_numPlavok.Items.Add(idx);
			}

			_numPlavok.SelectedValue = 3000;
		}

		private void FutTypeChanged(object sender, SelectionChangedEventArgs e)
		{
			FutChem_Load();
		}

		private void FutChem_Load()
		{
			var table = _params.Reader.FetchTable("futdata");

			Tube.Футеровка.Al2O3	= table.Rows[SelectedFuter]["Al2O3"].ToString().ToDoubleOrZero();
			Tube.Футеровка.C		= table.Rows[SelectedFuter]["C"].ToString().ToDoubleOrZero();
			Tube.Футеровка.CaO		= table.Rows[SelectedFuter]["CaO"].ToString().ToDoubleOrZero();
			Tube.Футеровка.MgO		= table.Rows[SelectedFuter]["MgO"].ToString().ToDoubleOrZero();
			Tube.Футеровка.P2O5		= table.Rows[SelectedFuter]["P2O5"].ToString().ToDoubleOrZero();
			Tube.Футеровка.SiO2		= table.Rows[SelectedFuter]["SiO2"].ToString().ToDoubleOrZero();
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

		private void OnArChanged(object sender, TextChangedEventArgs e)
		{
			var result = _ar.Text.ToDoubleSafe();
			if (!result.Item1) return;
			UpdateAr(result.Item2);
		}

		private void OnN2Changed(object sender, TextChangedEventArgs e)
		{
			var result = _n2.Text.ToDoubleSafe();
			if (!result.Item1) return;
			UpdateN2(result.Item2);
		}

		private void UpdateN2(double n2)
		{
			Tube.Дутье.N2 = n2;
			Tube.Дутье.O2 = 100 - Tube.Дутье.N2 - Tube.Дутье.Ar;
			ShowComposition();
		}

		private void UpdateAr(double ar)
		{
			Tube.Дутье.Ar = ar;
			Tube.Дутье.O2 = 100 - Tube.Дутье.N2 - Tube.Дутье.Ar;
			ShowComposition();
		}

		private static void UpdateAir(double n2, double ar)
		{
			Tube.Дутье.N2 = n2;
			Tube.Дутье.Ar = ar;
			Tube.Дутье.O2 = 100 - Tube.Дутье.N2 - Tube.Дутье.Ar;
		}

		private void ShowComposition()
		{
			if (Tube.Дутье.O2 < 90.0 || Tube.Дутье.O2 > 100.0)
			{
				UpdateAir(0.04, 0.25);
			}

			_ar.Text = string.Format("{0:0.###}", Tube.Дутье.Ar);
			_n2.Text = string.Format("{0:0.###}", Tube.Дутье.N2);
			_o2.Text = string.Format("{0:0.###}", Tube.Дутье.O2);
		}
	}
}
