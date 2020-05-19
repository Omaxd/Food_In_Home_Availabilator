using Common.Classes;
using Common.Constants;
using Common.Interfaces;
using Logic.Server;
using Repository;
using Service.Server;
using System;

namespace Controller.Server
{
    public class ServerController
    {
        private Database _database;

        #region Server logic containers
        private Fridge _fridge;
        private UserManager _userManager;
        #endregion

        #region Repositories
        private UserRepository _userRepository;
        #endregion

        #region Services
        private UserService _userService;
        #endregion

        public ServerController(IConnector connector)
        {
            _database = new Database();

            _fridge = new Fridge();
            _userManager = new UserManager();

            _userRepository = new UserRepository(_database);

            _userService = new UserService(connector, _userRepository);
        }

        public string GetRepositoryResponse(SocketMessage message)
        {
            if (message.Address.Contains(ServiceAddress.UserService))
                return _userService.GetResponse(message);

            throw new Exception();
        }

        public string GetServerResponse(SocketMessage message)
        {
            if (message.Address.Contains(ServiceMethod.CheckIfUserIsLogged))
                return _userManager.GetResponse(message);

            throw new Exception();
        }
    }
}
