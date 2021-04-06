using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs;
using ZPDSGGW.Models;

namespace ZPDSGGW.Profiles
{
    public class ProposalProfile : Profile
    {
        public ProposalProfile()
        {
            //Source -> Target
            CreateMap<Proposal, ProposalReadDto>();
            CreateMap<ProposalCreateDto, Proposal>();
            CreateMap<ProposalUpdateDto, Proposal>();
            CreateMap<Proposal, ProposalUpdateDto>();
        }
    }
}
