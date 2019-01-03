using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DataModel
{
    public class CreatePostDataModel
    {
        [Display(Name = "ID поста")]
        public int Id { get; set; }
        [Display(Name = "Наименование поста")]
        public string Content { get; set; }
        [Display(Name = "ID автора поста")]
        public int AuthorId { get; set; }
        [Display(Name = "Автор")]
        public virtual StudentDataModel Author { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime Created { get; set; }
        [Display(Name = "Тэг")]
        public string Tags { get; set; }
    }
}
