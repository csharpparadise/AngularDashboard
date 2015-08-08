using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AngularDashboardBackend.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            Send("ChatBot", "New user has been conntected.");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Send("ChatBot", "A user has been disconnected.");

            return base.OnDisconnected(stopCalled);
        }

        public void Hello(string username)
        {
            Clients.All.hello(username + " has joined the chat");
        }

        public void Send(string user, string message)
        {
            Clients.All.send(DateTime.Now.ToLongTimeString(), user, message);
        }
    }
}