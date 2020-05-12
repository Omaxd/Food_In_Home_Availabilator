using Logic.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Client.Presenters
{
    internal class LoginPanelPresenter
    {
        private readonly Connector _connector;
        public LoginPanelPresenter(Connector connector)
        {
            _connector = connector;
        }

        public bool Login(string login, string password)
        {
            _connector.Send("login");
            string response = (string)_connector.ReturnResponse();

            if (response == "true")
                return true;

            return false;
        }

        public bool Register()
        {
            return false;
        }
    }
}
