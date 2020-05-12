using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Client.Controls;
using View.Client.Presenters;

namespace View.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowPresenter _presenter;

        public MainWindow()
        {
            InitializeComponent();
            _presenter = new MainWindowPresenter();
            gAccount.Children.Add(new LoginPanel(_presenter.Connector));
        }
    }
}
