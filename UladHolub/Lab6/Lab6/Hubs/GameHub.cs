using Lab6.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Hubs
{
    public class GameHub : Hub
    {
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!Database.Users.Any(x => x.Id == id))
            {
                var user = new User { Id = id, Name = userName };
                Database.Users.Add(user);
                Clients.Caller.onConnected(user, Database.Users, Database.Groups);
                Clients.AllExcept(id).onNewUserConnected(user);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Database.Users.FirstOrDefault(x => x.Id == Context.ConnectionId);
            var group = new Group();

            if (item == null) { return base.OnDisconnected(stopCalled); }

            for (int i = 0; i < Database.Groups.Count; i++)
            {
                if(item == Database.Groups[i].Leader)
                {
                    group.StartGame = false;
                    Clients.Group(Database.Groups[i].Id).AddLeaderMessage("Leader is gone. Game over!");
                    Clients.Group(Database.Groups[i].Id).EndGame(Database.Groups[i]);
                }

                if (Database.Groups[i].Users.Count == 1)
                {
                    group.StartGame = false;
                    Clients.Group(Database.Groups[i].Id).AddLeaderMessage("All players are gone. Game over!");
                    Clients.Group(Database.Groups[i].Id).EndGame(Database.Groups[i]);
                }

                Database.Groups[i].Users.Remove(item);
                Groups.Remove(item.Id, Database.Groups[i].Id);
                group = Database.Groups[i];

                if (Database.Groups[i].Users.Count == 0)
                {
                    var id = Database.Groups[i].Id;
                    Database.Groups.RemoveAt(i);
                    Clients.All.RemoveGroupFromMenu(id);
                    i--;
                }
            }

            Database.Users.Remove(item);
            Clients.All.onUserDisconnected(item);
            Clients.All.onGroupUserDisconnected(item);
            Clients.All.ChangeGroupCounter(group);
            Clients.Group(group.Id).AddConnectMessage(item.Name, "disconnected");
            return base.OnDisconnected(stopCalled);
        }

        public void CreateRoom(string playersNumber)
        {
            var id = Context.ConnectionId;
            var user = Database.Users.FirstOrDefault(x => x.Id == Context.ConnectionId);

            if (user == null) { return; }

            var groupName = Guid.NewGuid().ToString();
            Groups.Add(id, groupName);
            var group = new Group()
            {
                Id = groupName,
                NumberOfPlayers = Convert.ToInt32(playersNumber),
                Users = new List<User>() { user }
            };
            Database.Groups.Add(group);
            Clients.Caller.OnGroupConnected(user, group);
            Clients.All.AddGroupToMenu(group);
            Clients.Group(group.Id).AddConnectMessage(user.Name, "connected");
        }

        public void ConnectToRoom(string groupId)
        {
            var id = Context.ConnectionId;
            var user = Database.Users.FirstOrDefault(x => x.Id == Context.ConnectionId);
            if (user == null) { return; }
            var group = Database.Groups.FirstOrDefault(x => x.Id == groupId);
            if (group == null || group.Users.Count >= group.NumberOfPlayers) { return; }
            group.Users.Add(user);
            Groups.Add(id, group.Id);
            Clients.Caller.OnGroupConnected(user, group);
            Clients.Group(group.Id, Context.ConnectionId).OnNewGroupUserConnected(user);
            Clients.All.ChangeGroupCounter(group);
            Clients.Group(group.Id).AddConnectMessage(user.Name, "connected");

            if (!group.StartGame && group.Users.Count == group.NumberOfPlayers)
            {
                var rand = new Random();
                group.Leader = group.Users[rand.Next(0, group.NumberOfPlayers)];
                group.Key = Database.Keys[rand.Next(0, Database.Keys.Length)];
                group.StartGame = true;
                Clients.Group(groupId).StartGame(group);
                Clients.All.RemoveGroupFromMenu(groupId);
                Clients.Client(group.Leader.Id).AddLeaderMessage("Your word is \""+ group.Key + "\"!");
            }
        }

        public void SendMessage(string message, string name, string groupId)
        {
            var group = Database.Groups.FirstOrDefault(x => x.Id == groupId);
            if (group == null) { return; }
            Clients.Group(groupId).AddMessage(name, message);

            if (group.StartGame && message.ToLower() == group.Key.ToLower())
            {
                group.StartGame = false;
                Clients.Group(groupId).AddLeaderMessage(name + " won!");
                Clients.Group(groupId).EndGame(group);
            }
        }

        public void EndGame(string groupId)
        {
            Groups.Remove(Context.ConnectionId, groupId);
            try
            {
                Database.Groups.Remove(Database.Groups.Where(x => x.Id == groupId).ElementAt(0));
            }
            catch { }
        }

        public void MouseDown(string groupId, int x, int y)
        {
            Clients.Group(groupId, Context.ConnectionId).MouseDown(x, y);
        }

        public void MouseUp(string groupId)
        {
            Clients.Group(groupId, Context.ConnectionId).MouseUp();
        }

        public void MouseMove(string groupId, int x, int y)
        {
            Clients.Group(groupId, Context.ConnectionId).MouseMove(x, y);
        }

        public void MouseLeave(string groupId)
        {
            Clients.Group(groupId, Context.ConnectionId).MouseLeave();
        }
    }
}