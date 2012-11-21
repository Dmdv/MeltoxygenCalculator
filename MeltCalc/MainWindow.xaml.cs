using System.Windows.Navigation;
using MeltCalc.Providers;
using MeltCalc.Tables;

namespace MeltCalc
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : NavigationWindow
	{
		public MainWindow()
		{
			var tables = new TablesSchema(@"h:\Projects\Coursach\Database\Loose.mdb");
			var tableNames = tables.GetTableNames();
			var tableFactory = new TableReader(@"h:\Projects\Coursach\Database\Loose.mdb");
			var executeTable = tableFactory.FetchTable("VlDol");
			var o = executeTable.Rows[0]["CaO"];

			var mdb = new LooseMdb();

			InitializeComponent();
		}
	}
}
