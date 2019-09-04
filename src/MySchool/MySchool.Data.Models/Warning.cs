using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySchool.Data.Models
{
    public class Warning : Item
    {
        public DateTime WritedOn { get; set; }

        public string Content { get; set; }

        public string StudentId { get; set; }

        [NotMapped]
        public MySchoolUser Student { get; set; }

        public string TeacherId { get; set; }

        [NotMapped]
        public MySchoolUser Teacher { get; set; }
    }
}
