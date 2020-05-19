using Common.Classes;
using Common.Constants;
using Common.Interfaces;
using Controller.Server;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Connector.Server
{
    /// <summary>
    /// Using project from: 
    /// https://github.com/AbleOpus/NetworkingSamples/blob/master/MultiClient
    /// </summary>
    public class WebSocketConnector : IConnector
    {
        private const int BUFFER_SIZE = 2048;
        private const int DEFAULT_PORT = 100;

        private readonly Socket _serverSocket;
        private readonly IList<Client> _clients;
        private int _port;
        private readonly byte[] _buffer;

        private readonly ServerController _controller;

        public WebSocketConnector()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<Client>();
            _buffer = new byte[BUFFER_SIZE];
            _port = DEFAULT_PORT;

            _controller = new ServerController(this);
        }

        public WebSocketConnector(int port)
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<Client>();
            _buffer = new byte[BUFFER_SIZE];
            _port = port;

            _controller = new ServerController(this);
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
            Socket currentSocket = (Socket)asyncResult.AsyncState;
            int received;

            try
            {
                received = currentSocket.EndReceive(asyncResult);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                currentSocket.Close();

                Client removedClient = _clients.FirstOrDefault(c => c.Socket == currentSocket);
                _clients.Remove(removedClient);

                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(_buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Received Text: " + text);

            try
            {
                SocketMessage socketMessage = JsonConvert.DeserializeObject<SocketMessage>(text);

                string message;

                if (socketMessage.Address.Contains(ServiceAddress.Server))
                {
                    message = MakeServerCommand(socketMessage, currentSocket);

                    if (message == null)
                        return;
                }
                else
                {
                    message = _controller.GetRepositoryResponse(socketMessage);
                }
                
                byte[] responseData = Encoding.ASCII.GetBytes(message);
                currentSocket.Send(responseData);
            }
            catch (Exception)
            {
                Console.WriteLine("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                currentSocket.Send(data);
                Console.WriteLine("Warning Sent");
            }

            currentSocket.BeginReceive(_buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, currentSocket);
        }

        private string MakeServerCommand(SocketMessage socketMessage, Socket socket)
        {
            string message = default;

            if (socketMessage.Address.Contains(ServerCommands.Exit))
            {
                DisconnectClient(socket);

                return null;
            }

            message = _controller.GetServerResponse(socketMessage);

            return message;
        }

        private void DisconnectClient(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            Client removedClient = _clients.FirstOrDefault(c => c.Socket == socket);
            int removedClientId = removedClient.User.Id;
            _clients.Remove(removedClient);

            Console.WriteLine($"Client with id='{removedClientId}' has been disconnected");
        }
    }
}
