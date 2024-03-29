﻿using AutoMapper;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Mvc;
using MySchool.Data.Models;
using MySchool.Web.Managers;
using MySchool.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MySchool.Web.Controllers
{
    public class TeacherController : Controller
    {
        private ITeacherManager teacherManager;
        private IMapper mapper;

        public TeacherController(ITeacherManager teacherManager, IMapper mapper)
        {
            this.teacherManager = teacherManager;
            this.mapper = mapper;
        }

        /// <summary>
        /// Here we get all teachers.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AllTeachers()
        {
            if (!User.IsInRole("Director"))
            {
                return RedirectToAction("Home", "Error");
            }

            var allTeachersDb = await this.teacherManager.GetAll(); //Get all teachers.
            var allTeachers = new List<TeacherViewModel>();

            foreach (var item in allTeachersDb)
            {
                var allStudentsDb = await GetAllTeacherStudents(item.Id); //We get all students of the current teacher.

                item.Students = allStudentsDb;
                allTeachers.Add(mapper.Map<TeacherViewModel>(item)); //Here we map teacher to teacherViewModel with automapper.
            }
            return View(allTeachers); //We return all teachers and their students
        }

        /// <summary>
        /// Here we remove teacher the db by his id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> RemoveTeacher(int? id)
        {
            if (id == null)
            {
                return new JsonResult("The id not be null.");
            }

            var result = await this.teacherManager.RemoveTeacher(id);
            if (result.Succeeded)
            {
                return new JsonResult(id);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Here we get all students of and teacher by his id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<Student>> GetAllTeacherStudents(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await this.teacherManager.GetAllTeacherStudents(id);
        }

        /// <summary>
        /// Here we add teacher to db.
        /// </summary>
        /// <returns></returns>
        public IActionResult AddTeacher()
        {
            return View();
        }

        /// <summary>
        /// Here we add teacher in the db.
        /// </summary>
        /// <param name="addTeacherViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddTeacher(AddTeacherViewModel addTeacherViewModel)
        {
            if (ModelState.IsValid && User.IsInRole("Director"))
            {
                var teacher = mapper.Map<Teacher>(addTeacherViewModel); // We map addTeacherViewModel to teacher.

                this.teacherManager.AddTeacher(teacher);
                TempData["addedTeacher"] = $"{teacher.Name}"; // Here save for the name of teacher to we show message in the view.

                return RedirectToAction(nameof(AllTeachers));
            }
            return View(addTeacherViewModel);
        }
        public async Task<IActionResult> DetailsTeacher(int? id)
        {
            if (id == null)
            {
                return RedirectToAction();
            }
            var teacher = await this.teacherManager.DetailsTeacher(id);
            var teacherViewModel = mapper.Map<TeacherViewModel>(teacher);
            var studentsOfTeacher = await this.GetAllTeacherStudents(id);

            foreach (var item in studentsOfTeacher)
            {
                var ratingsOfStudent = await this.teacherManager.GetRatingsOfStudentById(item.Id);
                item.AverageRatingNumberFromEverySubjects = await this.teacherManager.AverageRatingNumberFromEverySubjectsById(item.Id);
            }
            teacherViewModel.Students = studentsOfTeacher;
            return View(teacherViewModel);
        }
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentViewModel studentViewModel)
        {
            var student = mapper.Map<Student>(studentViewModel);
            var result = await this.teacherManager.AddStudent(student);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AllStudents));
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Here we get all students.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AllStudents()
        {
            var studentsDb = await this.teacherManager.GetAllStudents();
            var students = new List<StudentViewModel>();
            foreach (var item in studentsDb)
            {
                students.Add(mapper.Map<StudentViewModel>(item));
            }

            return View(students);
        }

        /// <summary>
        /// Here we show details for any student by his id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentViewModel> StudentDetails(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var student = await this.teacherManager.StudentDetailsById(id);

            var studentViewModel = mapper.Map<StudentViewModel>(student);

            return studentViewModel;
        }

        /// <summary>
        /// Here we add student to teacher by their ids.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public async Task<JsonResult> AddStudentToTeacher(int? id, int? teacherId)
        {
            if (id == null || teacherId == null)
            {
                return new JsonResult("The id not be null.");
            }

            var isExists = await this.teacherManager.CheckIfIsExists(id, teacherId);

            if (!isExists)
            {
                var result = await this.teacherManager.AddStudentToTeacherById(id, teacherId);
                if (result.Succeeded)
                {
                    return new JsonResult("Succeeded");
                }
                else
                {
                    return new JsonResult("Error");
                }
            }
            else
            {
                return new JsonResult("exsits");
            }

        }

        /// <summary>
        /// Here we get all teachers for to show in the partial view.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetAllTeachers(int? id)
        {
            var teachers = await this.teacherManager.GetAll();

            var teacherViewModels = new List<TeacherViewModel>();
            ViewData["studentId"] = id;

            ViewData["Student"] = this.teacherManager.StudentDetailsById(id).Result.Name;

            foreach (var item in teachers)
            {
                var teacherViewModel = mapper.Map<TeacherViewModel>(item);
                teacherViewModel.Students = await this.teacherManager.GetAllStudentsOfTeacher(item.Id);

                teacherViewModels.Add(teacherViewModel);
                var result = await this.teacherManager.CheckIfIsExists(id, item.Id);

                if (!result)
                {
                    teacherViewModel.Exists = result;
                }
                else
                {
                    teacherViewModel.Exists = result;
                }
            }

            return PartialView(teacherViewModels);
        }
    }
}
