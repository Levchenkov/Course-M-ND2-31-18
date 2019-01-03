using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Domain.Contracts.InfoModels;
using Twitter.Domain.Contracts.Services;

namespace Twitter.Web.Hubs
{
    public class ReportsPublisher : Hub
    {
        private readonly ITwitService twitService;
        private ITwitInfo twitInfo;
        
        public ReportsPublisher(ITwitService twitService, ITwitInfo twitInfo)
        {
            this.twitInfo = twitInfo;
            this.twitService = twitService;
        }

        public Task PublishReport(string header, string tags, string content)
        {
            twitInfo.Content = content;
            twitInfo.TagString = tags;
            twitInfo.Header = header;
            twitService.Add(twitInfo);
            return Clients.All.InvokeAsync("OnReportPublished", header, tags, content);
        }

        
    }
}
