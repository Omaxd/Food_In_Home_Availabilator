using Logic.Server;
using System;

namespace View.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Connector connector = new Connector();
            connector.Connect();

            //connector.Send("test");
            Console.ReadLine();
        }
    }
}
