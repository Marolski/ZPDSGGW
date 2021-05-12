using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.DTOs.InvitationPromoter
{
    public class InvitationPromoterUpdateDto
    {
        public Guid PromoterId { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public InvitationStatus Accepted { get; set; }
    }
}
