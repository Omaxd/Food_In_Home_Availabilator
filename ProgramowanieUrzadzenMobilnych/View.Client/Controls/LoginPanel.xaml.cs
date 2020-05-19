using Logic.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Client.Constants;
using View.Client.Presenters;

namespace View.Client.Controls
{
    /// <summary>
    /// Interaction logic for LoginPanel.xaml
    /// </summary>
    public partial class LoginPanel : UserControl
    {
        private LoginPanelPresenter _presenter;

        private string _login => tbLogin.Text;
        private string _password => pbPassword.Password;

        public LoginPanel(WebSocketConnector connector)
        {
            InitializeComponent();
            _presenter = new LoginPanelPresenter(connector);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidFields())
                return;

            UserStatus result = _presenter.Login(_login, _password);

            if (result == UserStatus.NotExist)
            {
                MessageBox.Show("Nieprawidłowy login i/lub hasło.");
            }
            else if (result == UserStatus.IsLogged)
            {
                MessageBox.Show("Użytkownik jest już zalogowany.");
            }
            else
            {
                MessageBox.Show($"Witaj {UserSessionInformation.UserName}");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            _presenter.Register();
        }

        private bool ValidFields()
        {
            IList<string> fields = new List<string>()
            {
                _login,
                _password
            };

            if (!_presenter.ValidFields(fields))
            {
                MessageBox.Show("Nie wszystkie pola są uzupełnione.");
                return false;
            }

            return true;
        }
    }
}
