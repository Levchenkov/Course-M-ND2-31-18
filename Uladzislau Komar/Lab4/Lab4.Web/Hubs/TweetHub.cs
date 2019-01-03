using Lab4.Domain.Contracts.Services;
using Lab4.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Web.Hubs
{
    public class TweetHub : Hub
    {
        private readonly ITweetService service;

        public TweetHub(ITweetService service)
        {
            this.service = service;
        }

        public async Task Send(string userId, string user, string content, string created)
        {
            try
            {
                var model = new TweetViewModel
                {
                    AuthorId = int.Parse(userId),
                    Content = content,
                    Created = DateTime.Parse(created)
                };
                service.Create(model);
                await this.Clients.All.SendAsync("Send", userId, user, content, created);
            }
            catch (Exception exception)
            {

            }
        }
    }
}
