using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.ThesisDto;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRepositoryThesisTopic _repository;
        private readonly IMapper _mapper;

        public UserController(IRepositoryThesisTopic repository, ILogger<UserController> logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<User>> GetAllPromoters()
        //{
        //    var topics = _repository.GetPromoters();
        //    return Ok(_mapper.Map<IEnumerable<ThesisReadDto>>(topics));
        //}
    }
}
