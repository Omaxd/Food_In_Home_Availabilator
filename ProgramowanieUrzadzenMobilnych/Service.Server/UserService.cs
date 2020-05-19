using Common.Classes;
using Common.Constants;
using Common.Interfaces;
using Model;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Server
{
    public class UserService : Service
    {
        private readonly UserRepository _userRepository;
        public UserService(IConnector connector, UserRepository userRepository)
            : base(connector)
        {
            _userRepository = userRepository;
        }

        public override string GetResponse(SocketMessage message)
        {
            IDictionary<string, string> content = message.GetContentPairs();

            switch (message.Method)
            {
                case RequestMethods.Get:
                    {
                        if (message.Address.Contains(ServiceMethod.GetUserByLoginAndPassword))
                        {
                            string login = content["login"];
                            string password = content["password"];
                            User user = _userRepository.GetByLoginAndPassword(login, password);
                            string response = JsonConvert.SerializeObject(user);

                            return response;
                        }
                        if (message.Address.Contains(ServiceMethod.CheckIfLoginIsAvailable))
                        {
                            string login = content["login"];
                            bool isAvailable = _userRepository.CheckIfLoginIsAvailable(login);
                            string response = JsonConvert.SerializeObject(isAvailable);

                            return response;
                        }
                    }
                    break;
            }

            throw new Exception();
        }

        public User GetById(int userId)
        {
            User user = _userRepository.GetById(userId);

            return user;
        }

        public User GetByLoginAndPassword(string login, string password)
        {
            User user = _userRepository.GetByLoginAndPassword(login, password);

            return user;
        }

        public bool CheckIfLoginIsAvailable(string login)
        {
            bool isAvailable = _userRepository.CheckIfLoginIsAvailable(login);

            return isAvailable;
        }
    }
}
