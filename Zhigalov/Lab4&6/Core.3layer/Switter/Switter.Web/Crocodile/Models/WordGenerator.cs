using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Switter.Web.Crocodile.Models
{
    public static class WordGenerator
    {
        private static string[] words = new string[]
        {
            "Melofon",
            "Gravicapa",
            "Sinhrofazatron",
            "Multipasport",
            "Griffonage",
            "Boredom",
            "Chewbacca",
            "Kryptonite",
            "Benedict Cumberbatch",
            "Flageolet"
        };

        public static string GetWord()
        {
            Random random = new Random();
            return words[random.Next(words.Length)];
        }
    }
}
