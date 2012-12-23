using System.Threading.Tasks;
using MeltCalc.Chemistry;
using MeltCalc.Providers;

namespace MeltCalc
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeDatabases();
			InitializeComponent();
			Estimation.Prepare1_REGRESSLOAD();
		}

		private static void InitializeDatabases()
		{
			Task.Factory.StartNew(TableCache.Refresh);
		}
	}
}
