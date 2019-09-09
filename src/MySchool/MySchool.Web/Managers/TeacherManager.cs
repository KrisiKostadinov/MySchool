using Microsoft.EntityFrameworkCore;
using MySchool.Data;
using MySchool.Data.Models;
using MySchool.Web.Results;
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
        /// Here we add teacher to db.
        /// </summary>
        /// <param name="teacher"></param>
        public void AddTeacher(Teacher teacher)
        {
            this.context.Teachers.Add(teacher);
            var result = this.context.SaveChanges();
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

        public Task<Teacher> GetTeacherById(int? id)
        {
            return this.context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<Teacher> GetTeacher(int? id)
        {
            return this.context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Remove teacher.
        /// </summary>
        /// <param name="teacher"></param>
        public async Task<TeacherResult> RemoveTeacher(int? id)
        {
            var teacher = await this.context.Teachers.FirstOrDefaultAsync();

            this.context.Remove(teacher);
            var result = await this.context.SaveChangesAsync();

            if (result > 0)
            {
                return new TeacherResult(true);
            }
            else
            {
                return new TeacherResult(false);
            }
        }
    }
}