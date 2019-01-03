using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Domain.Contracts.ViewModels
{
    public class TweetViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        [MaxLength(240)]
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string AuthorId { get; set; }
        public UserViewModel Author { get; set; }
    }
}
