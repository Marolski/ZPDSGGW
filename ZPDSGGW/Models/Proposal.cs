﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;

namespace ZPDSGGW.Models
{
    public class Proposal
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid StudentId { get; set; }
        public Guid PromoterId { get; set; }
        [Required]
        public ProposalStatus Status { get; set; }
        [Required]
        [MaxLength(250)]
        public string Topic { get; set; }
        [Required]
        public DateTime Date { get; set; }

    }
}
