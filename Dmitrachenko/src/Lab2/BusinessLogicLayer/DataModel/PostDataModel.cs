using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DataModel
{
    public class PostDataModel
    {
        [Display(Name = "ID поста")]
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Content { get; set; }
        [Display(Name = "ID автора поста")]
        public int AuthorId { get; set; }
        [Display(Name = "Автор")]
        public virtual StudentDataModel Author { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime Created { get; set; }
        public virtual ICollection<CommentDataModel> Comments { get; set; }
        public virtual ICollection<TagDataModel> Tags { get; set; }
    }
}
