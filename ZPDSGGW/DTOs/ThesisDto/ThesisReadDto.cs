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
        public Guid PromoterId { get; set; }
        public string Topic { get; set; }
        public ThesisTopicStatus Available { get; set; }
    }
}
