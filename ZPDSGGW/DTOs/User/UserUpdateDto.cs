using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.DTOs.Student
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }
        public Degrees Degrees { get; set; }
        public int Availability { get; set; }
        public string Password { get; set; }
    }
}
