using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Lab4.Data.Contracts.Entities
{
    public class TweetEntity
    {
        [Key]
        public int TweetId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(240, ErrorMessage = "Tweet length is too long.")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public int AuthorId { get; set; }
        public PersonEntity Author { get; set; }
    }
}
