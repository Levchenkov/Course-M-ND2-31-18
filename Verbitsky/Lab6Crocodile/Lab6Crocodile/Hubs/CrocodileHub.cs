using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6Crocodile.Hubs
{
    public class CrocodileHub : Hub
    {
        public async Task Register(string Name, string dropDownList)
        {
            var groupName = (dropDownList != Models.GroupManager.DefaultName) ? dropDownList : Name;
            Models.GroupManager.UserAndGroup.Add(Context.ConnectionId, groupName);
            if (dropDownList == Models.GroupManager.DefaultName)
            {
                string SecretWord = Models.SecretWordsGenerator.GetSecretWord().Take(1).Single();
                Models.GroupManager.Groups.Add(Name);
                await this.Clients.Client(Context.ConnectionId).SendAsync("GetSecretWord", SecretWord);
            }
            await this.Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task Send(string Name, string message)
        {
            string group = Models.GroupManager.UserAndGroup.Where(a => a.Key == Context.ConnectionId).Single().Value;
            await this.Clients.Group(group).SendAsync("Send", Name, message);
        }
        public async Task MouseDown(int x, int y)
        {
            string group = Models.GroupManager.UserAndGroup.Where(a => a.Key == Context.ConnectionId).Single().Value;
            await this.Clients.Group(group).SendAsync("MouseDown", x, y);
        }
        public async Task MouseMove(int x, int y)
        {
            string group = Models.GroupManager.UserAndGroup.Where(a => a.Key == Context.ConnectionId).Single().Value;
            await this.Clients.Group(group).SendAsync("MouseMove", x, y);
        }
        public async Task MouseUp(int x, int y)
        {
            string group = Models.GroupManager.UserAndGroup.Where(a => a.Key == Context.ConnectionId).Single().Value;
            await this.Clients.Group(group).SendAsync("MouseUp", x, y);
        }
    }
}
