using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZPDSGGW.Models
{
    public class File
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public byte[] Content  { get; set; }
    }
}
