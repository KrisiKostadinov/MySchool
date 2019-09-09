using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MySchool.Web.Models
{
    public class AddTeacherViewModel
    {
        public AddTeacherViewModel(DateTime dateTime)
        {
            this.Birthday = DateTime.ParseExact(dateTime.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public AddTeacherViewModel()
        {
            this.Birthday = DateTime.Now;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително!")]
        [Display(Name = "Три имена")]
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        [Display(Name = "Мобилен номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Населеното място е задължително!")]
        [Display(Name = "Населено място")]
        public string City { get; set; }
    }
}