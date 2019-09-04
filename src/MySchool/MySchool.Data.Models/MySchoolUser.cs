using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MySchool.Data.Models
{
    public class MySchoolUser : IdentityUser<string>
    {
        public List<IdentityRole<string>> Roles { get; set; }
    }
}