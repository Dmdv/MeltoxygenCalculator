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
		private string _mail;
		private string _password;

		internal event EventHandler LoginSuccessful;

		public LoginWindow()
		{
			InitializeComponent();
		}

		private void signInButton_Click(object sender, RoutedEventArgs e)
		{
			_mail = signInMailBox.Text;
			_password = signInPasswordBox.Password;

			if (true /* Appropriate Login Check Here*/)
			{
				LoginSuccessful(this, null);
				Close();
			}
			else
			{
				// Alert the user that login failed
			}

			//Проверяем нажимал ли пользователь кнопку - запомнить меня.
			//if (Authentification.RememberMe)
			//{
			//    //Если нажимал, то записываем все в файл. Это делает процедура класса Authentification
			//    //которая называется rememberLog и принимает в качестве параметров наш мейл и пароль;
			//    Authentification.rememberLog(_mail, _password);
			//}

			//MessageBox.Show(WorkWithDatabase.IsUserInDb(_mail, _password));
			// UnshowAllLogInForm();
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
			// Authentification.RememberMe = true;
		}

		//Нажали на кнопку с галочкой - false
		private void rememberMeSelectButton_Click(object sender, RoutedEventArgs e)
		{
			// Authentification.RememberMe = false;
		}

		//private void UnshowAllLogInForm()
		//{
		//    //Прячем кнопку "Войти"
		//    signInButton.Visibility = Visibility.Hidden;
		//    signInButtonImage.Visibility = Visibility.Hidden;

		//    //Прячем надпись "забыли пароль" и кнопку которая за нее отвечает
		//    forgotPassword.Visibility = Visibility.Hidden;
		//    forgotPasswordButton.Visibility = Visibility.Hidden;

		//    //Прячем окошко и темную штуку которая все делает неактивным
		//    signInBackground.Visibility = Visibility.Hidden;

		//    //Прячем боксы для ввода логина и пароля
		//    signInMailBox.Visibility = Visibility.Hidden;
		//    signInPasswordBox.Visibility = Visibility.Hidden;

		//    //Прячем надпись "запомнить меня"
		//    remeberMeText.Visibility = Visibility.Hidden;

		//    enterMailLabel.Visibility = Visibility.Hidden;
		//}
	}
}