using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Web.Hubs
{
    public class TweetHub : Hub
    {
        public async Task Send(string user, string content, string created)
        {
            await this.Clients.All.SendAsync("Send", user, content, created);
        }
    }
}
