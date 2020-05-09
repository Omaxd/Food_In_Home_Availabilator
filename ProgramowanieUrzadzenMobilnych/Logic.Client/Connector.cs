using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;

namespace Logic.Client
{
    public class Connector
    {
        public void StartConnection()
        {
            string ipServerAddress = "127.0.0.1";
            int portNumber = 80;
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipServerAddress), portNumber);
            tcpListener.Start();

            Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection...", Environment.NewLine);

            TcpClient client = tcpListener.AcceptTcpClient();

            Console.WriteLine("A client connected.");

            NetworkStream stream = client.GetStream();

            //enter to an infinite cycle to be able to handle every change in stream
            while (true)
            {
                while (!stream.DataAvailable) ;

                Byte[] bytes = new Byte[client.Available];

                stream.Read(bytes, 0, bytes.Length);
            }
        }
    }
}
