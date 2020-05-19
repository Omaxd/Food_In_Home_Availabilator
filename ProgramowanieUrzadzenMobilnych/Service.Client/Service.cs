using Logic.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Client
{
    public abstract class Service
    {
        protected WebSocketConnector connector;

        protected abstract string httpAddress { get; }

        public Service(WebSocketConnector connector)
        {
            this.connector = connector;
        }
    }
}
