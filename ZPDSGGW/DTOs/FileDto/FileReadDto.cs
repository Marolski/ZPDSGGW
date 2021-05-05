using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.DTOs.FileDto
{
    public class FileReadDto
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public DocumentKind DocumentKind { get; set; }
        public bool Accepted { get; set; }
        public Guid UserId { get; set; }
        public string FileName { get; set; }
    }
}
