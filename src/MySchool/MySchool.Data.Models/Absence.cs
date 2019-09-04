using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySchool.Data.Models
{
    public class Absence
    {
        [Key]
        public int Id { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int ExcusedAbsencesCount { get; set; }

        public int NotExcusedAbsencesCount { get; set; }

        public string StudentId { get; set; }

        [NotMapped]
        public MySchoolUser Student { get; set; }

        public string TeacherId { get; set; }

        [NotMapped]
        public MySchoolUser Teacher { get; set; }
    }
}
