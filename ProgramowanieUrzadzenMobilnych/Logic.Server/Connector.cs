using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Logic.Server
{
    /// <summary>
    /// Using project from: 
    /// https://github.com/AbleOpus/NetworkingSamples/blob/master/MultiClient
    /// </summary>
    public class Connector
    {
        private const int BUFFER_SIZE = 2048;
        private const int DEFAULT_PORT = 100;

        private readonly Socket _serverSocket;
        private readonly IList<Client> _clients;
        private int _port;
        private readonly byte[] _buffer;

        public Connector()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<Client>();
            _buffer = new byte[BUFFER_SIZE];
            _port = DEFAULT_PORT;
        }

        public Connector(int port)
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<Client>();
            _buffer = new byte[BUFFER_SIZE];
            _port = port;
        }

        void Main()
        {
            Console.Title = "Server";
            Connect();
            Console.ReadLine(); // When we press enter close everything
            DisconectAll();
        }

        public void Connect()
        {
            Console.WriteLine("Setting up server...");
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
            _serverSocket.Listen(0);
            _serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        public void DisconectAll()
        {
            foreach (Client client in _clients)
            {
                client.Socket.Shutdown(SocketShutdown.Both);
                client.Socket.Close();
            }

            _clients.Clear();
            _serverSocket.Close();
        }

        private void AcceptCallback(IAsyncResult asyncResult)
        {
            Socket socket;

            try
            {
                socket = _serverSocket.EndAccept(asyncResult);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            Client newClient = new Client(socket);

            _clients.Add(newClient);
            socket.BeginReceive(_buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");
            _serverSocket.BeginAccept(AcceptCallback, null);
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            Socket current = (Socket)asyncResult.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(asyncResult);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();

                Client removedClient = _clients.FirstOrDefault(c => c.Socket == current);
                _clients.Remove(removedClient);

                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(_buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Received Text: " + text);

            if (text.ToLower() == "get time") // Client requested time
            {
                Console.WriteLine("Text is a get time request");
                byte[] data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
                current.Send(data);
                Console.WriteLine("Time sent to client");
            }
            if (text.ToLower() == "login") // Client requested time
            {
                Console.WriteLine("Text is a get time request");
                byte[] data = Encoding.ASCII.GetBytes("true");
                current.Send(data);
                Console.WriteLine("Time sent to client");
            }
            else if (text.ToLower() == "exit") // Client wants to exit gracefully
            {
                // Always Shutdown before closing
                current.Shutdown(SocketShutdown.Both);
                current.Close();

                Client removedClient = _clients.FirstOrDefault(c => c.Socket == current);
                _clients.Remove(removedClient);

                Console.WriteLine("Client disconnected");

                return;
            }
            else
            {
                Console.WriteLine("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                Console.WriteLine("Warning Sent");
            }

            current.BeginReceive(_buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
    }
}
