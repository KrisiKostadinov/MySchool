using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MySchool.Data.Models;
using MySchool.Web.Managers;
using MySchool.Web.Models;
using System.Collections.Generic;
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

        private async Task<List<Student>> GetAllTeacherStudents(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await this.teacherManager.GetAllTeacherStudents(id);
        }

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
    }
}