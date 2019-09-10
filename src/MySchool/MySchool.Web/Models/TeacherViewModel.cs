using MySchool.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Web.Models
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Рожденна дата")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Мобилен номер")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        public ICollection<Student> Students { get; set; }

        [Display(Name = "Ученици")]
        public int StudentsCount => this.Students.Count;
    }
}
