using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.FileDto;
using ZPDSGGW.Models;

namespace ZPDSGGW.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileReadDto>();
            CreateMap<FileReadDto, File>();
            CreateMap<FileCreateDto, File>();
            CreateMap<File, FileUpdateDto>();
            CreateMap<FileUpdateDto, File>();
        }
    }
}
