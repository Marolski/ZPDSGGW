using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZPDSGGW.DTOs.MessageDto
{
    public class MessageCreateDto
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public Guid SendFrom { get; set; }
        public Guid SendTo { get; set; }
        public DateTime Date { get; set; }
    }
}
