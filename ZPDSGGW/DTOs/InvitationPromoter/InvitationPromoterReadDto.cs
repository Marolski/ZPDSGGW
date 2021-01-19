using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.DTOs.InvitationPromoter
{
    public class InvitationPromoterReadDto
    {
        public Guid Id { get; set; }
        public Models.Student Student { get; set; }
        public Models.Promoter Promoter { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public bool Accepted { get; set; }
    }
}
