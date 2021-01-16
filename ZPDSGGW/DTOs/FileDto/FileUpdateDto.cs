using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZPDSGGW.DTOs.FileDto
{
    public class FileUpdateDto
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public bool Accepted { get; set; }
    }
}
