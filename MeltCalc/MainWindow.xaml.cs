using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using MeltCalc.Chemistry;
using MeltCalc.Properties;
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

		private void TestDb()
		{
			var write = new TableWriter(Settings.Default.ParamsMdb);

			var tuples = new List<Tuple<string, double>>();
			var tuple1 = new Tuple<string, double>("Угар Fe в дым", 1.0d);
			var tuple2 = new Tuple<string, double>("Масса гот стали (выход)", 1.0d);
			tuples.Add(tuple1);
			tuples.Add(tuple2);

			write.Write("countdata", tuples, 1);
		}
	}
}
