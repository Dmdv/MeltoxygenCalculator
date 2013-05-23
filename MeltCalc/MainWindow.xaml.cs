using System;
using System.Threading.Tasks;
using System.Windows;
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
			Estimation.DirtyHack();
		}

		internal void StartupMainWindow(object sender, EventArgs e)
		{
			Application.Current.MainWindow = this;
			Show();
		}

		private static void InitializeDatabases()
		{
			Task.Factory.StartNew(TableCache.Refresh);
		}
	}
}
