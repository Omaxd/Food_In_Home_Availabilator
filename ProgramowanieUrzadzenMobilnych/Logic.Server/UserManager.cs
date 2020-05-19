using Common.Classes;
using Common.Constants;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Logic.Server
{
    public class UserManager
    {
        private IList<int> _loggedUsers;

        public UserManager()
        {
            _loggedUsers = new List<int>();
        }

        public bool RemoveLoggedUser(int userId)
        {
            // Check if user is active
            if (!_loggedUsers.Contains(userId))
                return false;

            _loggedUsers.Remove(userId);

            return true;
        }

        public bool CheckIfUserIsLogged(int userId)
        {
            if (_loggedUsers.Contains(userId))
                return true;

            AddLoggedUser(userId);

            return false;
        }

        public bool AddLoggedUser(int userId)
        {
            _loggedUsers.Add(userId);

            return true;
        }

        public string GetResponse(SocketMessage socketMessage)
        {
            IDictionary<string, string> content = socketMessage.GetContentPairs();

            switch (socketMessage.Method)
            {
                case RequestMethods.Get:
                    {
                        if (socketMessage.Address.Contains(ServiceMethod.CheckIfUserIsLogged))
                        {
                            string userIdString = content["userId"];
                            int userId = int.Parse(userIdString);
                            bool isLogged = CheckIfUserIsLogged(userId);
                            string response = JsonConvert.SerializeObject(isLogged);

                            return response;
                        }
                    }
                    break;
            }

            throw new Exception();
        }
    }
}
