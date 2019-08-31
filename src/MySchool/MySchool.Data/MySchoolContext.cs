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
    }
}