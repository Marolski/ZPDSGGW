using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.Models
{
    public class ThesisTopic
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid PromoterId { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public bool Available { get; set; }
    }
}
