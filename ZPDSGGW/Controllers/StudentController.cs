using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.Student;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Controllers
{
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IRepositoryStudent _repository;
        private readonly IMapper _mapper;

        public StudentController(ILogger<StudentController> logger, IRepositoryStudent repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentReadDto>> GetAllPromoters()
        {
            var student = _repository.GetAllStudents();
            return Ok(_mapper.Map<IEnumerable<StudentReadDto>>(student));
        }
        //Get api/proposals/{id}
        [HttpGet("{id}", Name = "GetPromoterById")]
        public ActionResult<StudentReadDto> GetPromoterById(Guid id)
        {
            var student = _repository.GetStudentById(id);
            if (student != null)
                return Ok(_mapper.Map<StudentReadDto>(student));
            return NotFound();
        }
        //POST api/proposals
        [HttpPost]
        public ActionResult<StudentReadDto> CreatePromoter(StudentCreateDto studentCreateDto)
        {
            var student = _mapper.Map<Student>(studentCreateDto);
            _repository.CreateStudent(student);
            _repository.SaveChanges();

            var studentReadDto = _mapper.Map<StudentReadDto>(student);

            return CreatedAtRoute(nameof(GetPromoterById), new { Id = studentReadDto.Id }, studentReadDto);
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
        //PATCH api/proposals/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialPromoterUpdate(Guid id, JsonPatchDocument<StudentUpdateDto> json)
        {
            var studentFromRepo = _repository.GetStudentById(id);
            if (studentFromRepo == null)
                return NotFound();
            var studentToPatch = _mapper.Map<StudentUpdateDto>(studentFromRepo);
            json.ApplyTo(studentToPatch, ModelState);
            if (!TryValidateModel(studentToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(studentToPatch, studentFromRepo);
            _repository.UpdateStudent(studentFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        //DELETE api/proposals/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProposal(Guid id)
        {
            var studentFromRepo = _repository.GetStudentById(id);
            if (studentFromRepo == null)
                return NotFound();
            _repository.DeleteStudent(studentFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
