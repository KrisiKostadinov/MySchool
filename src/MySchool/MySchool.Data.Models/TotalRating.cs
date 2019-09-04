using System.ComponentModel.DataAnnotations.Schema;

namespace MySchool.Data.Models
{
    public class TotalRating : Item
    {
        public int FirstRating { get; set; }

        public int SecondRating { get; set; }

        public string StudentId { get; set; }

        [NotMapped]
        public MySchoolUser Student { get; set; }

        public string TeacherId { get; set; }

        [NotMapped]
        public MySchoolUser Teacher { get; set; }
    }
}
