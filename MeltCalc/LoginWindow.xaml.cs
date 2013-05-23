using System;
using System.Windows;
using System.Windows.Input;

namespace MeltCalc
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class LoginWindow
	{
		private string _login;
		private string _password;
		private readonly Autorization _autorization = new Autorization();

		internal event EventHandler LoginSuccessful;

		public LoginWindow()
		{
			InitializeComponent();
		}

		private void signInButton_Click(object sender, RoutedEventArgs e)
		{
			_login = signInMailBox.Text;
			_password = signInPasswordBox.Password;

			if (_autorization.IsValidUser(_login, _password))
			{
				LoginSuccessful(this, null);
				Close();
			}
			else
			{
				MessageBox.Show("Пользователь с таким именем не существует", "Авторизация");
			}
		}

		private void enterMailLabel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			enterMailLabel.Visibility = Visibility.Hidden;
			Keyboard.Focus(signInMailBox);
		}

		private void enterPassLabel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			enterPassLabel.Visibility = Visibility.Hidden;
			Keyboard.Focus(signInPasswordBox);
		}

		private void signInMailBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (signInMailBox.Text == "")
			{
				enterMailLabel.Visibility = Visibility.Visible;
			}
		}

		private void signInPasswordBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (signInPasswordBox.Password == "")
			{
				enterPassLabel.Visibility = Visibility.Visible;
			}
		}

		private void rememberMeDeselectButton_Click(object sender, RoutedEventArgs e)
		{
		}

		private void rememberMeSelectButton_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}