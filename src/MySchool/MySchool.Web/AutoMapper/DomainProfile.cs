using AutoMapper;
using MySchool.Data.Models;
using MySchool.Web.Models;

namespace MySchool.Web.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Teacher, TeacherViewModel>().ReverseMap();
            CreateMap<Teacher, AddTeacherViewModel>().ReverseMap();
            CreateMap<Teacher, TeacherViewModel>().ReverseMap();
            CreateMap<Student, StudentViewModel>().ReverseMap();
            CreateMap<Student, StudentTeacher>().ReverseMap();
        }
    }
}