using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Entities
{
    public class Comment
    {
        [Key]
        [Display(Name = "ID сообщения")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Сообщение")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "ID автора")]
        public int AuthorId { get; set; }
        public virtual Student Author { get; set; }

        [Display(Name = "Дата создания (изменения)")]
        public virtual DateTime Created { get; set; }

        [Required]
        [Display(Name = "ID поста")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}