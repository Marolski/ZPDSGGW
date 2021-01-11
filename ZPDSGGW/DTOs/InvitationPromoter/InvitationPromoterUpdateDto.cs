using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZPDSGGW.DTOs.InvitationPromoter
{
    public class InvitationPromoterUpdateDto
    {
        public string Topic { get; set; }
        public string Description { get; set; }
        public bool Accepted { get; set; }
    }
}
