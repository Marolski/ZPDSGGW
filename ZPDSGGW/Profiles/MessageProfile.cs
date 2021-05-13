using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.MessageDto;
using ZPDSGGW.Models;

namespace ZPDSGGW.Profiles
{
    public class MessageProfile: Profile
    {
        public MessageProfile()
        {
            //source -> target
            CreateMap<Message, MessageCreateDto>();
            CreateMap<MessageCreateDto, Message>();
            CreateMap<Message, MessageReadDto>();
        }
    }
}
