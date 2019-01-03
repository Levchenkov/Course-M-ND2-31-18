using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crocodile.Helpers
{
    public static class Words
    {
        private static readonly IEnumerable<string> ListWords = new List<string>()
        {
            "Собака",
            "Кошка",
            "Холодильник",
            "Чайник",
            "Кровать",
            "Стол"
        };

        public static string GetRandomWord()
        {
            var random = new Random();
            var randomElement = ListWords.ElementAtOrDefault(random.Next(0, ListWords.Count() - 1));
            return randomElement;
        }
    }
    

}
