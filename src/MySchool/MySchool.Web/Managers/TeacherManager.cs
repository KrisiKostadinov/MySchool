using MySchool.Data;
using MySchool.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MySchool.Web.Managers
{
    public class TeacherManager : ITeacherManager
    {
        private MySchoolContext context;

        public TeacherManager(MySchoolContext context)
        {
            this.context = context;
        }

        public List<Teacher> GetAll()
        {
            var allTeachers = this.context.Teachers.ToList();
            return allTeachers;
        }
    }
}
