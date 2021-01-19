using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.DTOs.ThesisDto
{
    public class ThesisReadDto
    {
        public Guid Id { get; set; }
        public Models.Promoter Promoter { get; set; }
        public string Topic { get; set; }
        public bool Available { get; set; }
    }
}
