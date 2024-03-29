﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.Student;
using ZPDSGGW.Models;

namespace ZPDSGGW.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Source -> Target
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}
