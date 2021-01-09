using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.DTOs
{
    public class ProposalUpdateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PromoterSurname { get; set; }
        public string PromoterName { get; set; }
        public ProposalStatus Status { get; set; }
        public Degrees Degree { get; set; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }
    }
}
