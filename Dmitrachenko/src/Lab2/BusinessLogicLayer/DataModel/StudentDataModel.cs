using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DataModel
{
    public class StudentDataModel
    {
        [Display(Name = "ID студента")]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}
