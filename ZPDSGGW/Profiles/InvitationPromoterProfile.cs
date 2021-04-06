using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.InvitationPromoter;
using ZPDSGGW.Models;

namespace ZPDSGGW.Profiles
{
    public class InvitationPromoterProfile : Profile
    {
        public InvitationPromoterProfile()
        {
            //source -> target
            CreateMap<InvitationPromoter, InvitationPromoterCreateDto>();
            CreateMap<InvitationPromoterCreateDto, InvitationPromoter>();
            CreateMap<InvitationPromoter, InvitationPromoterUpdateDto>();
            CreateMap<InvitationPromoterUpdateDto, InvitationPromoter>();
            CreateMap<InvitationPromoter, InvitationPromoterReadDto>();
        }
    }
}
