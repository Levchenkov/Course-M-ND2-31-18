using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Entities
{
    public class Post
    {
        [Key]
        [Display(Name = "ID поста")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Наименование поста")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "ID автора")]
        public int AuthorId { get; set; }
        public virtual Student Author { get; set; }

        [Required]
        [Display(Name = "Дата создания (изменения)")]
        public DateTime Created { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}