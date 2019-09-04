using MySchool.Data.Models;
using System.Collections.Generic;

namespace MySchool.Web.Managers
{
    public interface ITeacherManager
    {
        List<Teacher> GetAll();
    }
}