using Common.Classes;
using Common.Constants;
using Logic.Client;
using Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Service.Client
{
    public class UserService : Service
    {
        protected override string httpAddress => ServiceAddress.UserService;

        public UserService(WebSocketConnector connector)
            : base(connector)
        { 
        }

        public User GetById(int userId)
        {
            string content = $"userId={userId}";

            HttpWebRequest request = WebRequest.CreateHttp(httpAddress);
            request.ContentType = "text/json";
            request.Method = "GET";

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(content);
            }

            var response = request.GetResponse() as HttpWebResponse;

            User user = null;

            return user;
        }

        public User GetByLoginAndPassword(string login, string password)
        {
            string content = $"login={login}&password={password}";
            string address = $"{httpAddress}\\{ServiceMethod.GetUserByLoginAndPassword}";
            User user = null;

            SocketMessage message = new SocketMessage()
            {
                Address = address,
                Content = content,
                ContentType = "application/json",
                Method = RequestMethods.Get,
            };
            string jsonData = JsonConvert.SerializeObject(message);

            try
            {
                connector.Send(jsonData);
                string serverResponse = (string)connector.ReturnResponse();
                user = JsonConvert.DeserializeObject<User>(serverResponse);
            }
            catch(Exception)
            {

            }

            return user;
        }

        public bool CheckIfLoginIsAvailable(string login)
        {
            string content = $"login={login}";
            string address = $"{httpAddress}\\{ServiceMethod.CheckIfLoginIsAvailable}";

            bool isAvailable = false;

            SocketMessage message = new SocketMessage()
            {
                Address = address,
                Content = content,
                ContentType = "application/json",
                Method = RequestMethods.Get,
            };

            string jsonData = JsonConvert.SerializeObject(message);

            try
            {
                connector.Send(jsonData);
                string serverResponse = (string)connector.ReturnResponse();
                isAvailable = JsonConvert.DeserializeObject<bool>(serverResponse);
            }
            catch (Exception)
            {

            }

            return isAvailable;
        }

        public bool CheckIfUserIsLogged(int userId)
        {
            string content = $"userId={userId}";
            string address = $"{ServiceAddress.Server}\\{ServiceMethod.CheckIfUserIsLogged}";

            bool isLogged = false;

            SocketMessage message = new SocketMessage()
            {
                Address = address,
                Content = content,
                ContentType = "application/json",
                Method = RequestMethods.Get,
            };

            string jsonData = JsonConvert.SerializeObject(message);

            try
            {
                connector.Send(jsonData);
                string serverResponse = (string)connector.ReturnResponse();
                isLogged = JsonConvert.DeserializeObject<bool>(serverResponse);
            }
            catch (Exception)
            {

            }

            return isLogged;
        }
    }
}
