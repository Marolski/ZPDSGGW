using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ZPDSGGW.DTOs.Student;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRepositoryUser _repository;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IRepositoryUser repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUser(Roles role)
        {
            var users = _repository.GetAllUsers(role);
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserReadDto> GetUserById(Guid id)
        {
            var user = _repository.GetUserById(id);
            if (user != null)
                return Ok(_mapper.Map<UserReadDto>(user));
            return NotFound();
        }

        [HttpPost]
        public ActionResult<UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            _repository.CreateUser(user);
            _repository.SaveChanges();

            var studentReadDto = _mapper.Map<UserReadDto>(user);

            return CreatedAtRoute(nameof(GetUserById), new { Id = studentReadDto.Id }, studentReadDto);
        }
        //PUT api/proposals/{id}
        //[HttpPut("{id}")]
        //public ActionResult UpdatePromoter(Guid id, PromoterUpdateDto promoterUpdateDto)
        //{
        //    var promoterFromRepo = _repository.GetPromoterById(id);
        //    if (promoterFromRepo == null)
        //        return NotFound();
        //    _mapper.Map(promoterUpdateDto, promoterFromRepo);
        //    _repository.UpdatePromoter(promoterFromRepo);
        //    _repository.SaveChanges();

        //    return NoContent();
        //}

        [HttpPatch("{id}")]
        public ActionResult PartialUserUpdate(Guid id, JsonPatchDocument<UserUpdateDto> json)
        {
            var userFromRepo = _repository.GetUserById(id);
            if (userFromRepo == null)
                return NotFound();
            var userToPatch = _mapper.Map<UserUpdateDto>(userFromRepo);
            json.ApplyTo(userToPatch, ModelState);
            if (!TryValidateModel(userToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(userToPatch, userFromRepo);
            _repository.UpdateUser(userFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            var userFromRepo = _repository.GetUserById(id);
            if (userFromRepo == null)
                return NotFound();
            _repository.DeleteUser(userFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
