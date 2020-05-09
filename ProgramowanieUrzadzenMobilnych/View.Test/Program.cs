using System;

namespace View.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Logic.Server.Connector connector = new Logic.Server.Connector();
            connector.Send("test");
        }
    }
}
