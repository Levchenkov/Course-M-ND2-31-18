using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Models
{
    public class WordRandomizer
    {
        private static readonly List<string> words;
        private static readonly Random random;

        static WordRandomizer()
        {
            words = new List<string>()
            {
                "House",
                "Stool",
                "Sky",
                "Asp.NET MVC Core",
                "Programmer"
            };
            random = new Random((int)DateTime.Now.Ticks);
        }

        public static string GetWord()
        {
            var index = random.Next(0, words.Count - 1);
            var output = words[index];
            return output;
        }
    }
}
