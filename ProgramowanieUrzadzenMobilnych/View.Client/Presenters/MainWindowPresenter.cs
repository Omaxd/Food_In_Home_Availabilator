using Logic.Client;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Client.Presenters
{
    internal class MainWindowPresenter
    {
        public WebSocketConnector Connector { get; private set; }

        public MainWindowPresenter()
        {
            ConnectToServer();
        }

        public void SetView()
        {

        }

        private void ConnectToServer()
        {
            Connector = new WebSocketConnector();
            Connector.Connect();
        }
    }
}
