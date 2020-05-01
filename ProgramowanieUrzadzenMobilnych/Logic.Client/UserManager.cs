using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Client
{
    public class UserManager
    {

        public bool LogIn(string login, string password)
        {
            return false;

            User loggedUser = new User()
            {
                Login = login,
                Password = password
            };
        }
    }
}
