using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZPDSGGW.DTOs.Student
{
    public class StudentCreateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }
    }
}
