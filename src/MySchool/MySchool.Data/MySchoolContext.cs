using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySchool.Data.Models;

namespace MySchool.Data
{
    public class MySchoolContext : IdentityDbContext<MySchoolUser, IdentityRole<string>, string>
    {
        public MySchoolContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentTeacher> StudentsTeachers { get; set; }
    }
}
