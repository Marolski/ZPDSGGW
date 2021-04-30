using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.Models
{
    public class File
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public DocumentKind DocumentKind {get; set;}
        [Required]
        public bool Accepted { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
