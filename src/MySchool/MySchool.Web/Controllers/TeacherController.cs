using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        private async Task<List<Student>> GetAllTeacherStudents(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await this.teacherManager.GetAllTeacherStudents(id);
        }
    }
}