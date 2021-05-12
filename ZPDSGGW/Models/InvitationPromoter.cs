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
        public Guid StudentId { get; set; }
        public Guid PromoterId { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        [Required]
        public InvitationStatus Accepted { get; set; }

    }
}
