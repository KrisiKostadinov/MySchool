using System;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Data.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BirthYear { get; set; }

        public int BirthMonth { get; set; }

        public int BirthDay { get; set; }

        public string PhoneNumber { get; set; }

        public string  City { get; set; }
    }
}