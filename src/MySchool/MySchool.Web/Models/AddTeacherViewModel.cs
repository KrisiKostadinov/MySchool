using System.ComponentModel.DataAnnotations;

namespace MySchool.Web.Models
{
    public class AddTeacherViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително!")]
        [Display(Name = "Три имена")]
        public string Name { get; set; }
    }
}
