using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Logic.Server
{
    public class Connector
    {
        private string _serverIP = "localhost";
        private int _port = 8080;
        private bool _isRunning;

        public void StartConnection()
        {
            IPAddress ipAddress = Dns.GetHostEntry(_serverIP).AddressList[0];
            TcpListener server = new TcpListener(ipAddress, _port);

            try
            {
                server.Start();
                Console.WriteLine("Server start");
                _isRunning = true;
            }
            catch(Exception)
            {

            }

            while (_isRunning)
            {
                TcpClient client = server.AcceptTcpClient();
                byte[] receivedBuffer = new byte[200];
                NetworkStream Stream = client.GetStream();

                Stream.Read(receivedBuffer, 0, receivedBuffer.Length);
                string message = Encoding.ASCII.GetString(receivedBuffer, 0, receivedBuffer.Length);

                Console.WriteLine(message);
            }
        }

        public void Send(string message)
        {
            TcpClient client = new TcpClient(_serverIP, _port);

            int byteCount = Encoding.ASCII.GetByteCount(message);

            byte[] sendData = Encoding.ASCII.GetBytes(message);

            NetworkStream stream = client.GetStream();

            stream.Write(sendData, 0, sendData.Length);

            stream.Close();
        }
    }
}
