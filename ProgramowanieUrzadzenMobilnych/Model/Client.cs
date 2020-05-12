using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Model
{
    public class Client
    {
        public Client() { }

        public Client(Socket socket)
        {
            Socket = socket;
        }

        public User User { get; set; }
        public Socket Socket { get; set; }

        public bool IsLogged
        {
            get
            {
                bool isLogged = User != null;

                return isLogged;
            }
        }
    }
}
