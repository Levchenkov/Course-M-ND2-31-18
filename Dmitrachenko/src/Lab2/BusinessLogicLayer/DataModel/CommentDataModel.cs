using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DataModel
{
    public class CommentDataModel
    {
        [Display(Name = "ID коммента")]
        public int Id { get; set; }
        [Display(Name = "Сообщение")]
        public string Content { get; set; }
        [Display(Name = "ID автора коммента")]
        public int AuthorId { get; set; }
        [Display(Name = "Автор")]
        public virtual StudentDataModel Author { get; set; }
        [Display(Name = "Дата создания")]
        public virtual DateTime Created { get; set; }
        [Display(Name = "ID поста")]
        public int PostId { get; set; }
        public virtual PostDataModel Post { get; set; }
    }
}
