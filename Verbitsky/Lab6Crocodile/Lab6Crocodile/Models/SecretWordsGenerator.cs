using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6Crocodile.Models
{
    public static class SecretWordsGenerator
    {
        private static string[] words = new string[]
        {
            "Дом",
            "Пол",
            "Вентилятор",
            "Диван",
            "Телевизор",
            "Поле",
            "Ветер"
        };
        public static IEnumerable<string> GetSecretWord()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            foreach (string item in words.OrderBy(a => rand.Next()))
            {
                yield return item;
            }
        }
    }
}
