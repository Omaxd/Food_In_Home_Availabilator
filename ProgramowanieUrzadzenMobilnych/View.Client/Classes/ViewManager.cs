using Logic.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using View.Client.Constants;
using View.Client.Controls;

namespace View.Client.Classes
{
    internal class ViewManager
    {
        public IDictionary<ViewName, UserControl> _centerViews;

        public ViewManager(WebSocketConnector connector)
        {
            _centerViews = new Dictionary<ViewName, UserControl>();

            InitializeViews(connector);
        }
        public void SetView(ViewName viewName)
        {
            throw new Exception();
        }

        private void InitializeViews(WebSocketConnector connector)
        {
            LoginPanel loginPanel = new LoginPanel(connector);
            RegisterPanel registerPanel = new RegisterPanel(connector);

            _centerViews.Add(ViewName.LoginPanel, loginPanel);
            _centerViews.Add(ViewName.RegisterPanel, registerPanel);
        }
    }
}
