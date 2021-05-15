using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.MessageDto;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;
using ZPDSGGW.Services;

namespace ZPDSGGW.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IRepositoryMessage _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public MessageController(ILogger<MessageController> logger, IWebHostEnvironment environment, IRepositoryMessage repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
        }

        [HttpGet("userId")]
        public ActionResult<IEnumerable<MessageReadDto>> GetRecivierMessage(Guid id)
        {
            var messages = _repository.GetMessagesByUserId(id);
            return Ok(_mapper.Map<IEnumerable<MessageReadDto>>(messages));
        }

        [HttpGet(Name = "GetFileMessageById")]
        public async Task<ActionResult> GetFileMessageById(Guid id)
        {
            var pathToFile = _repository.GetPathFromMessage(id);
            FileService service = new FileService(_environment);
            return await service.DownloadFile(pathToFile);
        }

        //Get api/message/{id}
        [HttpGet("{id}", Name = "GetMessageById")]
        public ActionResult<MessageReadDto> GetMessageById(Guid id)
        {
            var message = _repository.GetMessageById(id);
            return Ok(_mapper.Map<MessageReadDto>(message));
        }

        //POST api/message
        [HttpPost]
        public ActionResult<MessageReadDto> CreateMessage([FromForm] FormFile objFile,Guid sendFrom, Guid sendTo, string description)
        {
            FileService service = new FileService(_environment);
            Task<string> path = null;
            var messageModel = new Message();
            if (objFile.file != null)
            {
                path = service.Uploadfile(objFile, Enums.DocumentKind.thesis);
                messageModel = new Message
                {
                    Id = Guid.NewGuid(),
                    Path = path.Result,
                    Date = DateTime.Now,
                    Description = description,
                    SendFrom = sendFrom,
                    SendTo = sendTo
                };
            }
            else
            {
                messageModel = new Message
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Description = description,
                    SendFrom = sendFrom,
                    SendTo = sendTo,
                    Path = "/"
                };
            }
            _repository.SaveMessage(messageModel);
            _repository.SaveChanges();
            return Ok();
        }
    }
}
