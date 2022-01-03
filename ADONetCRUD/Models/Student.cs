using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADONetCRUD.Models
{
    public class Student
    {
        public int RollNumber { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
} 