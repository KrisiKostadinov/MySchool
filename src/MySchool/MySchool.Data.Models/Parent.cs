namespace MySchool.Data.Models
{
    public class Parent : Person
    {
        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
