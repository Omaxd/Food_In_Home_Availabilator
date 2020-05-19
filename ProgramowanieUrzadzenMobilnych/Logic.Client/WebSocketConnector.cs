using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;

namespace Logic.Client
{
    /// <summary>
    /// Using project from:
    /// https://github.com/AbleOpus/NetworkingSamples/blob/master/MultiServer
    /// </summary>
    public class WebSocketConnector : IConnector
    {
        private const int _port = 100;

        private Socket _clientSocket;

        /*
        static void Main()
        {
            Console.Title = "Client";
            ConnectToServer();
            RequestLoop();
            Exit();
        }*/

        public void Connect()
        {
            _clientSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            int attempts = 0;

            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Connection attempt " + attempts);
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    _clientSocket.Connect(IPAddress.Loopback, _port);
                }
                catch (SocketException)
                {
                    //Console.Clear();
                }
            }

            //Console.WriteLine("Connected");
        }

        private void RequestLoop()
        {
            Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

            while (true)
            {
                SendRequest();
                ReceiveResponse();
            }
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        private void Disconect()
        {
            Send("exit"); // Tell the server we are exiting
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();
            Environment.Exit(0);
        }

        private void SendRequest()
        {
            Console.Write("Send a request: ");
            string request = Console.ReadLine();
            Send(request);

            if (request.ToLower() == "exit")
            {
                Disconect();
            }            
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        public void Send(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            _clientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        public object ReturnResponse()
        {
            var buffer = new byte[2048];
            int received = _clientSocket.Receive(buffer, SocketFlags.None);

            if (received == 0)
                return "";

            byte[] data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);

            return text;
        }

        public void ReceiveResponse()
        {
            var buffer = new byte[2048];
            int received = _clientSocket.Receive(buffer, SocketFlags.None);

            if (received == 0)
                return;

            byte[] data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            Console.WriteLine(text);
        }
    }
}
