using Logic.Client;
using Model;
using Service.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using View.Client.Constants;

namespace View.Client.Presenters
{
    internal class LoginPanelPresenter
    {
        private readonly WebSocketConnector _connector;
        private readonly UserService _userService;

        public LoginPanelPresenter(WebSocketConnector connector)
        {
            _connector = connector;
            _userService = new UserService(_connector);
        }

        public UserStatus Login(string login, string password)
        {
            User user = _userService.GetByLoginAndPassword(login, password);

            if (user == null)
                return UserStatus.NotExist;

            bool isLogged = _userService.CheckIfUserIsLogged(user.Id);

            if (isLogged)
                return UserStatus.IsLogged;

            UserSessionInformation.LoggedUserId = user.Id;
            UserSessionInformation.UserName = user.Name;

            return UserStatus.Valid;
        }

        public bool Register()
        {
            return false;
        }
    }
}
