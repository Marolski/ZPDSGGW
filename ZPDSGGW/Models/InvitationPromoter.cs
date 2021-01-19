﻿using System;
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
        public Student Student { get; set; }
        [Required]
        public Promoter Promoter { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Accepted { get; set; }

    }
}
