using Microsoft.EntityFrameworkCore;
using MySchool.Data;
using MySchool.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchool.Web.Managers
{
    public class TeacherManager : ITeacherManager
    {
        /// <summary>
        /// Here we get the db context to get access to the db.
        /// </summary>
        private MySchoolContext context;

        public TeacherManager(MySchoolContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Here we get all teachers models and them display in the view.
        /// </summary>
        /// <returns>TeacherViewModels</returns>
        public async Task<List<Teacher>> GetAll()
        {
            var allTeachers = await this.context.Teachers.ToListAsync();
            return allTeachers;
        }

        /// <summary>
        /// Here we get all students of current teacher by his id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Student>> GetAllTeacherStudents(int? id)
        {
            // If the id is null return null.
            if (id == null)
            {
                return null;
            }
            // Other case we get the students of the teacher.

            var teacherStudentIds = await this.context.StudentsTeachers
                .Where(st => st.TeacherId == id)
                .Select(st => st.StudentId)
                .ToListAsync();
            var students = new List<Student>();

            foreach (var item in teacherStudentIds)
            {
                students.AddRange(this.context.Students.Where(s => s.Id == item));
            }

            return students;
        }
    }
}