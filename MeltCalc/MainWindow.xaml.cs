using System.Collections.Generic;
using System.Windows.Navigation;
using MeltCalc.Providers;

namespace MeltCalc
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : NavigationWindow
	{
		public MainWindow()
		{
			var tables = new TablesProvider(@"h:\Projects\Coursach\Database\Loose.mdb");
			var tableNames = tables.GetTableNames();
			var tableFactory = new TableFactory(@"h:\Projects\Coursach\Database\Loose.mdb", "VlDol");
			var executeTable = tableFactory.ExecuteTable();
			var o = executeTable.Rows[0]["CaO"];

			InitializeComponent();
		}
	}
}
