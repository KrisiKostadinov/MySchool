using System.ComponentModel.DataAnnotations;

namespace MySchool.Web.Models
{
    public class AddTeacherViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително!")]
        [Display(Name = "Име:")]
        public string Name { get; set; }

        [Display(Name = "Година")]
        public int BirthYear { get; set; }

        [Display(Name = "Месец")]
        public int BirthMonth { get; set; }

        [Display(Name = "Ден")]
        public int BirthDay { get; set; }

        [Display(Name = "Мобилен номер:")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Населеното място е задължително!")]
        [Display(Name = "Населено място:")]
        public string City { get; set; }
    }
}