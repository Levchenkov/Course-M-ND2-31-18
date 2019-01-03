using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crocodile.Helpers;
using Crocodile.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Crocodile.Hubs
{
    public class CrocodileHub : Hub
    {
       private static List<User> Users = new List<User>();
       private static int idGroup = 1;
       private static Dictionary<string, string> _GroupAndSecretWord =
            new Dictionary<string, string>();
        private static int GetGroupForUser()
        {
            var countOfUsersInCurrentGroup = Users.FindAll(user => user.IdGroup == idGroup).Count;
            if (countOfUsersInCurrentGroup > 3)
            {
                idGroup++;
                GetGroupForUser();
            }
            return idGroup;
        }
       private static User GetAdministrator(List<User> users)
        {
            var random = new Random();
            return users.ElementAtOrDefault(random.Next(0, users.Count - 1));
        }

        public async Task Send(string message)
        {
            var sender = Users.FirstOrDefault(user => user.ConnectionId == Context.ConnectionId);
            _GroupAndSecretWord.TryGetValue(sender.IdGroup.ToString(),out var secretWord);
            var isRightWord = false;
            if (secretWord != null)
                isRightWord = secretWord.Contains(message, StringComparison.InvariantCultureIgnoreCase);
            await Clients.Group(sender.IdGroup.ToString()).SendAsync("Send", sender.Name, message, isRightWord);
            if (isRightWord) await EndGame(sender.Name);
        }
        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;
            if (Users.All(user => user.ConnectionId != id))
            {
                var group = GetGroupForUser();
                var newUser = new User() { ConnectionId = id, Name = userName, IdGroup = group };
                Users.Add(newUser);
                Groups.AddToGroupAsync(newUser.ConnectionId, newUser.IdGroup.ToString());
                await Send("connected");
            }
            await BeginGame();
        }

        public async Task BeginGame()
        {
            var currentUser = Users.FirstOrDefault(user => user.ConnectionId == Context.ConnectionId);
            var currentGroup = Users.FindAll(user => user.IdGroup == currentUser.IdGroup);
            if(currentGroup.Count<4) return;
            var administrator = GetAdministrator(currentGroup);
            _GroupAndSecretWord[currentUser.IdGroup.ToString()] = Words.GetRandomWord();
            await Clients.Client(administrator.ConnectionId).SendAsync("BeginGame", _GroupAndSecretWord[currentUser.IdGroup.ToString()]);
            await Send("Begin Game");
        }

        public async Task EndGame(string userName)
        {
            var endUser = Users.FirstOrDefault(user => user.Name == userName);
            if (endUser == null) return;
            await Clients.Group(endUser.IdGroup.ToString()).SendAsync("EndGame");
            await Connect(userName);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var id = Context.ConnectionId;
            var disconnectedUser = Users.FirstOrDefault(user => user.ConnectionId == id);
            if (disconnectedUser == null) return;
            await Send("disconnected");
            Groups.RemoveFromGroupAsync(disconnectedUser.ConnectionId, disconnectedUser.IdGroup.ToString());
            Users.Remove(disconnectedUser);
            idGroup = disconnectedUser.IdGroup;
        }
        public async Task MouseDown(int x, int y)
        {
            var currentUser = Users.FirstOrDefault(user => user.ConnectionId == Context.ConnectionId);
            await Clients.Group(currentUser.IdGroup.ToString()).SendAsync("MouseDown", x, y);
        }
        public async Task MouseMove(int x, int y)
        {
            var currentUser = Users.FirstOrDefault(user => user.ConnectionId == Context.ConnectionId);
            await Clients.Group(currentUser.IdGroup.ToString()).SendAsync("MouseMove", x, y);
        }
        public async Task MouseUp(int x, int y)
        {
            var currentUser = Users.FirstOrDefault(user => user.ConnectionId == Context.ConnectionId);
            await Clients.Group(currentUser.IdGroup.ToString()).SendAsync("MouseUp", x, y);
        }
    }
}
