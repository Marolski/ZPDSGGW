using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.DTOs
{
    public class ProposalReadDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid PromoterId { get; set; }
        public ProposalStatus Status { get; set; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }
    }
}
