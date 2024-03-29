﻿using MySchool.Data.Models;
using MySchool.Web.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySchool.Web.Managers
{
    public interface ITeacherManager
    {
        Task<List<Teacher>> GetAll();

        Task<List<Student>> GetAllTeacherStudents(int? id);

        void AddTeacher(Teacher teacher);

        Task<TeacherResult> RemoveTeacher(int? id);

        Task<Teacher> GetTeacherById(int? id);

        Task<Teacher> GetTeacher(int? id);

        Task<Teacher> DetailsTeacher(int? id);

        Task<List<Rating>> GetRatingsOfStudentById(int? id);

        Task<double> AverageRatingNumberFromEverySubjectsById(int? id);

        Task<TeacherResult> AddStudentToTeacherById(int? id, int? teacherId);

        Task<StudentResult> AddStudent(Student student);

        Task<List<Student>> GetAllStudents();

        Task<Student> StudentDetailsById(int? id);

        Task<List<int>> GetAddedTeachersToStudent(int? id);

        Task<bool> CheckIfIsExists(int? id, int? teacherId);

        Task<List<Student>> GetAllStudentsOfTeacher(int? id);
    }
}