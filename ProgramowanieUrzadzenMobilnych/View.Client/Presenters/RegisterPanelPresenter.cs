using Logic.Client;
using Service.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Client.Presenters
{
    public class RegisterPanelPresenter
    {
        private readonly WebSocketConnector _connector;
        private readonly UserService _userService;

        public RegisterPanelPresenter(WebSocketConnector connector)
        {
            _connector = connector;
            _userService = new UserService(_connector);
        }

        public bool ValidFields(IList<string> fields)
        {
            foreach(string field in fields)
            {
                if (string.IsNullOrEmpty(field) || string.IsNullOrWhiteSpace(field) || field?.Length <= 0)
                    return false;
            }

            return true;
        }

        public bool ValidPassword(string password, string repeatPassword)
        {
            if (password == repeatPassword)
                return true;

            return false;
        }

        public bool CheckIfLoginIsAvailable(string login)
        {
            bool isAvailable = _userService.CheckIfLoginIsAvailable(login);

            return isAvailable;
        }
    }
}
