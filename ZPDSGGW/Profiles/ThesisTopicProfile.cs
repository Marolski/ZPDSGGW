using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.ThesisDto;
using ZPDSGGW.Models;

namespace ZPDSGGW.Profiles
{
    public class ThesisTopicProfile : Profile
    {
        public ThesisTopicProfile()
        {
            //Source -> Target
            CreateMap<ThesisTopic, ThesisReadDto>();
            CreateMap<ThesisCreateDto, ThesisTopic>();
            CreateMap<ThesisUpdateDto, ThesisTopic>();
            CreateMap<ThesisTopic, ThesisUpdateDto>();
        }
    }
}
