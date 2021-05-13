using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZPDSGGW.Models
{
    public class Message
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid SendFrom { get; set; }
        [Required]
        public Guid SendTo { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
