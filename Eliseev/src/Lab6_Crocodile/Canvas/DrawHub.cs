using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Canvas.Models;
using Microsoft.AspNet.SignalR;

namespace Canvas
{
    public class DrawHub : Hub
    {
        public static List<string> Words = new List<string>() { "Car", "House", "City", "Sun", "Earth" };

        public static List<Player> Players = new List<Player>();

        public static List<Room> Rooms = new List<Room>();
                

        public static Random random = new Random();

        public void CreatePlayer(string name)
        {
            string id = Context.ConnectionId;
            Player player = Players.FirstOrDefault(p => p.ConnectionId == id);
            if (player == null)
            {
                player = new Player();
                player.Name = name;                
                player.ConnectionId = id;
                Players.Add(player);
            }
            else
            {
                return;
            }            
            Clients.Caller.showExistedRooms(Rooms, player);
            Clients.Others.showExistedRooms(Rooms, "");
        }

        public async Task CreateRoom(string playerId, string playerName)
        {
            Room room = new Room() { Id = Guid.NewGuid().ToString() };            
            var player = Players.FirstOrDefault(p => p.ConnectionId == playerId);
            player.Role = "Artist";
            player.RoomId = room.Id;            
            room.Players.Add(player);
            string word = Words[random.Next(0, 4)];
            room.Task = word;
            Rooms.Add(room);

            await Groups.Add(Context.ConnectionId, room.Id);
            Clients.Others.showExistedRooms(Rooms, "");
            Clients.Caller.showTask(word,room.Id,player);
        }

        public async Task Join(string roomId,string playerId)
        {
            var targetRoom = Rooms.FirstOrDefault(r => r.Id == roomId);
            if (targetRoom != null)
            {
                Player newPlayer = Players.FirstOrDefault(p => p.ConnectionId == playerId);
                newPlayer.Role = "Player";
                newPlayer.RoomId = roomId;
                targetRoom.Players.Add(newPlayer);
                await Groups.Add(Context.ConnectionId, roomId);
                Clients.Caller.joinGame(targetRoom,roomId);
                Clients.OthersInGroup(roomId).newPlayerEntered(newPlayer);
                
                Clients.Others.showExistedRooms(Rooms,"");
            }
        }

        public void Send(Data data,string roomId)
        {
            Clients.OthersInGroup(roomId).addLine(data);            
        }


        public void SendMessage(string assumption, string playerId)
        {
            StringBuilder stringBuilder = new StringBuilder(assumption.Trim(' '));
            stringBuilder[0] = char.ToUpper(stringBuilder[0]);
            for(int i = 1; i < stringBuilder.Length; i++)
            {
                if (char.IsUpper(stringBuilder[i]))
                {
                    stringBuilder[i] = char.ToLower(stringBuilder[i]);
                }                
            }

            Player player = Players.FirstOrDefault(p => p.ConnectionId == playerId);
            Room room = Rooms.FirstOrDefault(r => r.Id == player.RoomId);
            string result = " is wrong";
            if (room.Task == stringBuilder.ToString())
            {
                room.Players.FirstOrDefault(p => p.Role == "Artist").Role = "Player";
                player.Role = "Artist";
                string word = Words[random.Next(0, 4)];
                room.Task = word;
                Clients.Client(playerId).congratulation(word, room.Id, player);
                Clients.Group(room.Id).updatePlayerList(room.Players);                
                Clients.Group(room.Id).updateCanvas();
                Clients.Group(room.Id, playerId).updateRole("Player");
                result = " is corect";
            }
            Clients.Group(room.Id).showNewAssumption(assumption, player, result);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var player = Players.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
            var room = Rooms.FirstOrDefault(r => r.Id == player.RoomId);
            
            if(player==null && room == null)
            {
                return base.OnDisconnected(stopCalled);
            }

            if (player.Role == "Artist" && room.Players.Count > 1)
            {
                var newArtist = room.Players.FirstOrDefault(p => p.Role != "Artist");
                newArtist.Role = "Artist";
                string word = Words[random.Next(0, 4)];
                room.Task = word;
                Clients.Client(newArtist.ConnectionId).showNewTask(word, newArtist.RoomId, newArtist);
            }
                        
            room.Players.Remove(player);            
            Groups.Remove(Context.ConnectionId, room.Id);
                
            if (room.Players.Count == 0)
            {
                Rooms.Remove(room);
            }
            else
            {
                Clients.Group(room.Id).updatePlayerList(room.Players);
            }
            Clients.All.showExistedRooms(Rooms, "");
            Players.Remove(player);

            return base.OnDisconnected(stopCalled);
        }
    }
}