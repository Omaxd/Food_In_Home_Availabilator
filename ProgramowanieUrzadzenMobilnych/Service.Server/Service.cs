using Common.Classes;
using Common.Interfaces;
using System;

namespace Service.Server
{
    public abstract class Service
    {
        IConnector connector;

        public Service(IConnector connector)
        {
            this.connector = connector;
        }

        public abstract string GetResponse(SocketMessage message);
    }
}
