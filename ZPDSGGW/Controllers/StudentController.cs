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
    [ApiController]
    [Route("api/student")]
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
        public ActionResult<IEnumerable<StudentReadDto>> GetAllStudent()
        {
            var student = _repository.GetAllStudents();
            return Ok(_mapper.Map<IEnumerable<StudentReadDto>>(student));
        }

        [HttpGet("{id}", Name = "GetStudentById")]
        public ActionResult<StudentReadDto> GetStudentById(Guid id)
        {
            var student = _repository.GetStudentById(id);
            if (student != null)
                return Ok(_mapper.Map<StudentReadDto>(student));
            return NotFound();
        }

        [HttpPost]
        public ActionResult<StudentReadDto> CreateStudent(StudentCreateDto studentCreateDto)
        {
            var student = _mapper.Map<Student>(studentCreateDto);
            _repository.CreateStudent(student);
            _repository.SaveChanges();

            var studentReadDto = _mapper.Map<StudentReadDto>(student);

            return CreatedAtRoute(nameof(GetStudentById), new { Id = studentReadDto.Id }, studentReadDto);
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
        public ActionResult PartialStudentUpdate(Guid id, JsonPatchDocument<StudentUpdateDto> json)
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

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(Guid id)
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
