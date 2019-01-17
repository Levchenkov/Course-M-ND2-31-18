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
        

    }
}