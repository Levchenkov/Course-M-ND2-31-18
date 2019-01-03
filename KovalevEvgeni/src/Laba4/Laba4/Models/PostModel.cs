using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laba4.Models
{
    public class PostModel
    {
        [Display(Name = "Заголовок")]
        [Required]
        public string TitlePost { get; set; }
        [Display(Name = "Содержимое")]
        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 240)]
        [Required]
        public string Content { get; set; }
    }
}