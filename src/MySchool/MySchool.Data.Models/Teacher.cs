using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySchool.Data.Models
{
    public class Teacher : Person
    {
        public Teacher()
        {
            this.Students = new List<Student>();
        }

        [NotMapped]
        public ICollection<Student> Students { get; set; }
    }
}
