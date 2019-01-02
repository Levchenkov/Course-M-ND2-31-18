using Lab6.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Hubs
{
    public class CrocoHub: Hub
    {
        private static readonly List<User> users;
        private static int groupId;
        private static Dictionary<string, string> groupWords;

        static CrocoHub()
        {
            users = new List<User>();
            groupId = 1;
            groupWords = new Dictionary<string, string>();
        }

        private static int GetGroupForUser()
        {
            var countOfUsersInCurrentGroup = users.FindAll(User => User.GroupId == groupId).Count;
            if (countOfUsersInCurrentGroup > 3)
            {
                groupId++;
                GetGroupForUser();
            }
            return groupId;
        }
        private static User GetAdministrator(List<User> users)
        {
            var random = new Random();
            return users.ElementAtOrDefault(random.Next(0, users.Count - 1));
        }

        public async Task Send(string message)
        {
            var sender = users.FirstOrDefault(User => User.ConnectionId == Context.ConnectionId);
            groupWords.TryGetValue(sender.GroupId.ToString(), out var secretWord);
            var isRightWord = false;
            if (secretWord != null)
                isRightWord = secretWord.Contains(message, StringComparison.InvariantCultureIgnoreCase);
            await Clients.Group(sender.GroupId.ToString()).SendAsync("Send", sender.Name, message, isRightWord);
            if (isRightWord) await EndGame(sender.Name);
        }

        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;
            if (users.All(User => User.ConnectionId != id))
            {
                var group = GetGroupForUser();
                var newUser = new User() { ConnectionId = id, Name = userName, GroupId = group };
                users.Add(newUser);
                await Groups.AddToGroupAsync(newUser.ConnectionId, newUser.GroupId.ToString());
                await Send("connected");
            }
            await BeginGame();
        }

        public async Task BeginGame()
        {
            var currentUser = users.FirstOrDefault(User => User.ConnectionId == Context.ConnectionId);
            var currentGroup = users.FindAll(User => User.GroupId == currentUser.GroupId);
            if (currentGroup.Count < 2) return;
            var administrator = GetAdministrator(currentGroup);
            groupWords[currentUser.GroupId.ToString()] = WordRandomizer.GetWord();
            await Clients.Client(administrator.ConnectionId).SendAsync("BeginGame", groupWords[currentUser.GroupId.ToString()]);
            await Send("BeginGame");
        }

        public async Task EndGame(string userName)
        {
            var endUser = users.FirstOrDefault(User => User.Name == userName);
            if (endUser == null) return;
            await Clients.Group(endUser.GroupId.ToString()).SendAsync("EndGame");
            await Connect(userName);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var id = Context.ConnectionId;
            var disconnectedUser = users.FirstOrDefault(User => User.ConnectionId == id);
            if (disconnectedUser == null) return;
            await Send("disconnected");
            await Groups.RemoveFromGroupAsync(disconnectedUser.ConnectionId, disconnectedUser.GroupId.ToString());
            users.Remove(disconnectedUser);
            groupId = disconnectedUser.GroupId;
        }

        public async Task MouseDown(int x, int y)
        {
            var currentUser = users.FirstOrDefault(User => User.ConnectionId == Context.ConnectionId);
            await Clients.Group(currentUser.GroupId.ToString()).SendAsync("MouseDown", x, y);
        }

        public async Task MouseMove(int x, int y)
        {
            var currentUser = users.FirstOrDefault(User => User.ConnectionId == Context.ConnectionId);
            await Clients.Group(currentUser.GroupId.ToString()).SendAsync("MouseMove", x, y);
        }

        public async Task MouseUp(int x, int y)
        {
            var currentUser = users.FirstOrDefault(User => User.ConnectionId == Context.ConnectionId);
            await Clients.Group(currentUser.GroupId.ToString()).SendAsync("MouseUp", x, y);
        }
    }
}
