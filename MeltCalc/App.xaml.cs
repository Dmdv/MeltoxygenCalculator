using System.Windows;

namespace MeltCalc
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		private MainWindow main = new MainWindow();
		private LoginWindow login = new LoginWindow();

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
			Current.MainWindow = login;

			login.LoginSuccessful += main.StartupMainWindow;
			login.Show();
		}
	}
}