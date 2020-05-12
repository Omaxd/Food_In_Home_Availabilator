using Logic.Client;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Client.Presenters
{
    internal class MainWindowPresenter
    {
        public Connector Connector { get; private set; }
        public User LoggedUser { get; set; }

        public MainWindowPresenter()
        {
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            Connector = new Connector();
            Connector.Connect();
        }
    }
}
