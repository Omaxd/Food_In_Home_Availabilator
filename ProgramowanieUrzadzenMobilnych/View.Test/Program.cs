using Connector.Server;
using System;

namespace View.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSocketConnector connector = new WebSocketConnector();
            connector.Connect();

            //connector.Send("test");
            Console.ReadLine();
        }
    }
}
