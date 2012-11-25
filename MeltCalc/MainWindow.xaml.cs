using System.Data;
using System.Windows.Navigation;
using MeltCalc.Model;
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
			var tables = new TablesSchema(@"h:\Projects\Coursach\Database\Loose.mdb");
			var tableNames = tables.GetTableNames();
			var tableFactory = new TableReader(@"h:\Projects\Coursach\Database\Loose.mdb");
			var executeTable = tableFactory.FetchTable("VlDol");
			var cao = executeTable.Rows[0]["CaO"];

			var mdb = new LooseMdb();
			var table = mdb.Reader.FetchTable("Save");
			var uu = table.Rows[0][Materials.Доломит.ToName()];

			InitializeComponent();
		}
	}
}
