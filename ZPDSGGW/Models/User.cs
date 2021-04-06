using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [AllowNull]
        public string StudentNumber { get; set; }
        [Required]
        [AllowNull]
        public Degrees Degrees { get; set; }
        [Required]
        [AllowNull]
        public int Availability { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Role { get; set; }
        public string Token { get; set; }

    }
}
