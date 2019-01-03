using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Data.Contracts.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(240)]
        public string Content { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [ForeignKey("Author")]
        public string UserId { get; set; }
        public  User User { get; set; }
    }
}
