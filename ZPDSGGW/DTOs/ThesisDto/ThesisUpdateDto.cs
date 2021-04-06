using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.DTOs.ThesisDto
{
    public class ThesisUpdateDto
    {
        public string Topic { get; set; }
        public ThesisTopicStatus Available { get; set; }
    }
}
