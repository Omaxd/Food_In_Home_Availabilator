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
using View.Client.Presenters;

namespace View.Client.Controls
{
    /// <summary>
    /// Interaction logic for RegisterPanel.xaml
    /// </summary>
    public partial class RegisterPanel : UserControl
    {
        private RegisterPanelPresenter _presenter;

        private string _login => tbLogin.Text;
        private string _name=> tbName.Text;
        private string _password => pbPassword.Password;
        private string _repeatPassword => pbRepeatPassword.Password;

        public RegisterPanel(WebSocketConnector connector)
        {
            InitializeComponent();
            _presenter = new RegisterPanelPresenter(connector);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            IList<string> fields = new List<string>()
            {
                _login,
                _name,
                _password,
                _repeatPassword
            };

            if (_presenter.ValidFields(fields))
            {

                return;
            }

            if (_presenter.ValidPassword(_password, _repeatPassword))
            {

                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
