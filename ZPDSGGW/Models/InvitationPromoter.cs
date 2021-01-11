using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.Models
{
    public class InvitationPromoter
    {

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PromoterName { get; set; }
        [Required]
        public string PromoterSurname { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public Degrees degrees { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Accepted { get; set; }


    }
}
