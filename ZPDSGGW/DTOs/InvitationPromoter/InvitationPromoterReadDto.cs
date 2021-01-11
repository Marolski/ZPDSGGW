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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PromoterName { get; set; }
        public string PromoterSurname { get; set; }
        public string Topic { get; set; }
        public Degrees degrees { get; set; }
        public string Description { get; set; }
        public bool Accepted { get; set; }
    }
}
