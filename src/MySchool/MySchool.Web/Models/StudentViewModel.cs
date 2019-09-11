using MySchool.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Web.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Година")]
        public int BirthYear { get; set; }

        [Display(Name = "Месец")]
        public int BirthMonth { get; set; }

        [Display(Name = "Ден")]
        public int BirthDay { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        public ICollection<Teacher> Teachers { get; set; }

        [Display(Name = "Учители")]
        public int TeachersCount => this.Teachers.Count;

        public int ParentId { get; set; }

        public Parent Parent { get; set; }

        public List<Rating> Ratings { get; set; }

        [Display(Name = "Средна оценка")]
        public double AverageRatingNumberFromEverySubjects { get; set; }
    }
}
