using System;
using System.ComponentModel.DataAnnotations;

namespace Switter.Data.Contracts.Entities
{
    public class Tweet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(240)]
        public string Content { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }
    }
}