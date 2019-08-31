using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySchool.Data.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public DateTime WritedOn { get; set; }

        public string Subject { get; set; }

        public int RatingNumber { get; set; }

        public string TeacherId { get; set; }

        [NotMapped]
        public MySchoolUser Teacher { get; set; }
    }
}
