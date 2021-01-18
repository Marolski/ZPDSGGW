using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.Promoter;
using ZPDSGGW.Models;

namespace ZPDSGGW.Profiles
{
    public class PromoterProfile : Profile
    {
        public PromoterProfile()
        {
            //Source -> Target
            CreateMap<Promoter, PromoterReadDto>();
            CreateMap<PromoterCreateDto, Promoter>();
            CreateMap<PromoterUpdateDto, Promoter>();
            CreateMap<Promoter, PromoterUpdateDto>();
        }
    }
}
