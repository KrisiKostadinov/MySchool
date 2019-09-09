using System.Collections.Generic;

namespace MySchool.Web.Results
{
    public class TeacherResult
    {
        private readonly bool success;

        public TeacherResult(bool success)
        {
            this.success = success;
        }

        public bool Succeeded => this.success;
    }
}