using Microsoft.AspNetCore.SignalR;
using Switter.Web.Crocodile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Switter.Web.Crocodile.Hubs
{
    public class ChatHub : Hub
    {
        private static List<Player> players = new List<Player>();

        public async Task Send(string message, string userName)
        {
            await Clients.All.SendAsync("Send", message, userName);
            if (TheGame.GameStart)
            {
                if (message == TheGame.Word)
                {
                    TheGame.GameStart = false;
                    TheGame.EndGame(players);
                    await Clients.All.SendAsync("Send", $"Congratulations to {userName}! It's " + message, "System");
                }
            }
        }

        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!players.Any(x => x.ConnectionId == id) & !players.Any(x => x.Name == userName))
            {
                players.Add(new Player { ConnectionId = id, Name = userName });
            }
            if (!TheGame.GameStart & players.Count > 1)
            {
                TheGame.GameStart = true;
                TheGame.StartGame(players);
                await Clients.All.SendAsync("Send", $"The Game Begins! Master is {TheGame.Master.Name}", "System");
            }
            await UpdatePlayers(players);
        }

        public async Task UpdatePlayers(List<Player> players)
        {
            await Clients.All.SendAsync("UpdatePlayers", players);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UpdatePlayers", players);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Player currentPlayer = players.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            players.Remove(currentPlayer);
            if (TheGame.GameStart & players.Count <= 1)
            {
                TheGame.GameStart = false;
                TheGame.EndGame(players);
                await Clients.All.SendAsync("Send", "The Game is Over!", "System");
            }
            await Clients.All.SendAsync("UpdatePlayers", players);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task MouseDown(int x, int y)
        {
            await Clients.All.SendAsync("MouseDown", x, y);
        }
        public async Task MouseMove(int x, int y)
        {
            await Clients.All.SendAsync("MouseMove", x, y);
        }
        public async Task MouseUp(int x, int y)
        {
            await Clients.All.SendAsync("MouseUp", x, y);
        }
    }
}
