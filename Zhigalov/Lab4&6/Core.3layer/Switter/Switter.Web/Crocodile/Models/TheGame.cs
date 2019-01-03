using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Switter.Web.Crocodile.Models
{
    public static class TheGame
    {
        public static bool GameStart { get; set; }
        public static string Word { get; set; }
        public static Player Master { get; set; }

        public static void StartGame(List<Player> players)
        {
            Random random = new Random();
            Player masterPlayer = players[random.Next(players.Count)];
            masterPlayer.Master = true;
            masterPlayer.Word = WordGenerator.GetWord();
            Word = masterPlayer.Word;
            Master = masterPlayer;
        }

        public static void EndGame(List<Player> players)
        {
            Player masterPlayer = players.FirstOrDefault(x => x.Master == true);
            masterPlayer.Master = false;
            masterPlayer.Word = null;
            Word = null;
        }
    }
}
