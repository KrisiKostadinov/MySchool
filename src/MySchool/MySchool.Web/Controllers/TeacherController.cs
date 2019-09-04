using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

        public TeacherController(ITeacherManager teacherManager, UserManager<MySchoolUser> userManager, IMapper mapper)
        {
            this.teacherManager = teacherManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> AllTeachers()
        {
            var allTeachersDb = this.teacherManager.GetAll();
            var allTeachers = new List<TeacherViewModel>();
            foreach (var item in allTeachersDb)
            {
                allTeachers.Add(mapper.Map<TeacherViewModel>(item));
            }

            return View(allTeachers);
        }
    }
}
