using AutoMapper;
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
        private IMapper mapper;
        /// <summary>
        /// Here we get the db context to get access to the db.
        /// </summary>
        private MySchoolContext context;

        public TeacherManager(MySchoolContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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

        /// <summary>
        /// Here we get teacher by his id from db.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Teacher> GetTeacherById(int? id)
        {
            return this.context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Here we remove teacher by his id from db.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Here we get teacher by id from db to we show details for it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Teacher> DetailsTeacher(int? id)
        {
            return this.context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Rating>> GetRatingsOfStudentById(int? id)
        {
            var studentRatings = await this.context.Ratings.Where(s => s.StudentId == id).ToListAsync();
            return studentRatings;
        }

        /// <summary>
        /// Here we calculate average rating of student by his id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<double> AverageRatingNumberFromEverySubjectsById(int? id)
        {
            var ratingNumbers = await this.context
                .Ratings
                .Where(s => s.StudentId == id)
                .Select(r => r.RatingNumber)
                .ToListAsync();
            if (ratingNumbers.Count == 0)
            {
                return 0;
            }
            else
            {
                var averageRatingNumber = ratingNumbers.Average();
                return averageRatingNumber;
            }
        }

        /// <summary>
        /// Here we adds student to teacher by his id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TeacherResult> AddStudentToTeacherById(int? id, int? teacherId)
        {
            var studentTeacher = new StudentTeacher();
            studentTeacher.StudentId = id;

            studentTeacher.TeacherId = teacherId;

            await this.context.StudentsTeachers.AddAsync(studentTeacher);
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

        /// <summary>
        /// Here we add student to db.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<StudentResult> AddStudent(Student student)
        {
            await this.context.Students.AddAsync(student);

            var result = await this.context.SaveChangesAsync();

            if (result > 0)
            {
                return new StudentResult(true);
            }
            else
            {
                return new StudentResult(false);
            }
        }

        /// <summary>
        /// Here we get all students from db.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Student>> GetAllStudents()
        {
            var students = await this.context.Students.ToListAsync();
            return students;
        }

        /// <summary>
        /// Here we get student by his id for to we show details for it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Student> StudentDetailsById(int? id)
        {
            var student = await this.context.Students.FirstOrDefaultAsync(s => s.Id == id);
            return student;
        }

        public async Task<List<int>> GetAddedTeachersToStudent(int? id)
        {
            var teacherIds = await this.context.StudentsTeachers.Where(t => t.StudentId == id).Select(t => t.Id).ToListAsync();
            return teacherIds;
        }
    }
}