namespace MySchool.Web.Results
{
    public class StudentResult
    {
        private readonly bool success;

        public StudentResult(bool success)
        {
            this.success = success;
        }

        public bool Succeeded => this.success;
    }
}