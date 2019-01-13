using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Domain.Contracts.Interfaces;
using Domain.Contracts.ViewModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Web.Script.Serialization;
using Web.Models;

namespace Web.Hubs
{
    public class TwitHub : Hub
    {
        //[HubMethodName("Send")]
        //public static void NotifyCurrentUserInformationToAllClients()
        //{

        //    //var twitInfo = new JavaScriptSerializer().Deserialize<TwitInfo>(content);


        //    //var twit = new TwitViewModel()
        //    //{
        //    //    Id = Guid.NewGuid().ToString(),
        //    //    Content = twitInfo.Content,
        //    //    Created = DateTime.Now,
        //    //    UserId = twitInfo.UserId
        //    //};
        //    //twitService.CreateAsynk(twit);

        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TwitHub>();
            
        //    context.Clients.All.addNewTwit("Refresh");

        //}








        //private ITwitService twitService;

        //public TwitHub(ITwitService twitService)
        //{
        //    this.twitService = twitService;
        //}

    }
}