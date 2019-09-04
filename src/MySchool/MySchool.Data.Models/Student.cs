using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySchool.Data.Models
{
    public class Student : Person
    {
        public Student()
        {
            this.Teachers = new List<Teacher>();
        }

        [NotMapped]
        public ICollection<Teacher> Teachers { get; set; }

        public int ParentId { get; set; }

        [NotMapped]
        public Parent Parent { get; set; }
    }
}
