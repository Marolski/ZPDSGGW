﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.Student;
using ZPDSGGW.Models;

namespace ZPDSGGW.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            //Source -> Target
            CreateMap<Student, StudentReadDto>();
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentUpdateDto, Student>();
            CreateMap<Student, StudentUpdateDto>();
        }
    }
}