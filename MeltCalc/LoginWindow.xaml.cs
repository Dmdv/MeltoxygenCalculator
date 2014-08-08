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
		public LoginWindow()
		{
			InitializeComponent();
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

		private void rememberMeSelectButton_Click(object sender, RoutedEventArgs e)
		{
		}

		private void signInButton_Click(object sender, RoutedEventArgs e)
		{
			_login = signInMailBox.Text;
			_password = signInPasswordBox.Password;

			if (Autorization.IsValidUser(_login, _password))
			{
				LoginSuccessful(this, null);
				Close();
			}
			else
			{
				MessageBox.Show("Пользователь с таким именем не существует", "Авторизация");
			}
		}

		private void signInMailBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (signInMailBox.Text == string.Empty)
			{
				enterMailLabel.Visibility = Visibility.Visible;
			}
		}

		private void signInPasswordBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (signInPasswordBox.Password == string.Empty)
			{
				enterPassLabel.Visibility = Visibility.Visible;
			}
		}

		internal event EventHandler LoginSuccessful;

		private string _login;
		private string _password;
	}
}