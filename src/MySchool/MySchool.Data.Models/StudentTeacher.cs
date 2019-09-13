using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySchool.Data.Models
{
    public class StudentTeacher
    {
        [Key]
        public int Id { get; set; }

        public int? StudentId { get; set; }

        [NotMapped]
        public Student Student { get; set; }

        public int? TeacherId { get; set; }

        [NotMapped]
        public Teacher Teacher { get; set; }
    }
}