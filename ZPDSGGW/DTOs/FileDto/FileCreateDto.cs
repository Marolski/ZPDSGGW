using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.DTOs.FileDto
{
    public class FileCreateDto
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
  
        public DocumentKind DocumentKind { get; set; }
        public bool Accepted { get; set; }
    }
}
