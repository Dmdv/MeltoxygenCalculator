using System.Windows;

namespace MeltCalc
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
			Current.MainWindow = _login;

			_login.LoginSuccessful += _main.StartupMainWindow;
			_login.Show();
		}

		private readonly LoginWindow _login = new LoginWindow();
		private readonly MainWindow _main = new MainWindow();
	}
}