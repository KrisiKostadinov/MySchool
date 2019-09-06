using MySchool.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySchool.Web.Managers
{
    public interface ITeacherManager
    {
        Task<List<Teacher>> GetAll();

        Task<List<Student>> GetAllTeacherStudents(int? id);
    }
}
