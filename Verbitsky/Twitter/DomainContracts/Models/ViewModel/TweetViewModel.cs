using System.ComponentModel.DataAnnotations;

namespace DomainContracts.Models.ViewModel
{
    public class TweetViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Head")]
        public string Head { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        [MaxLength(240, ErrorMessage = "Max lenght 240 symbols")]
        public string Content { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string AuthorId { get; set; }
    }
}
