using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MoscaWeb
{
    public class ReplyHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}