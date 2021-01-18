using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    [Route("api/thesis")]
    public class ThesisTopicController : ControllerBase
    {
        private readonly ILogger<ThesisTopicController> _logger;
        private readonly IRepositoryThesisTopic _repository;
        private readonly IMapper _mapper;

        public ThesisTopicController(IRepositoryThesisTopic repository, ILogger<ThesisTopicController> logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// //////////////////////////////////////////////////dopisać mapowania Z imie i nazwisko promtora na model promotor
        /// </summary>
        /// <returns></returns>
        //Get api/thesis
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllTopics()
        {
            var topics = _repository.GetTopics();
            return Ok(topics);
        }

        [HttpGet("{id}", Name = "GetTopicById")]
        public ActionResult<ThesisReadDto> GetTopicById(Guid id)
        {
            var topic = _repository.GetTopicById(id);
            if (topic != null)
                return Ok(_mapper.Map<ThesisReadDto>(topic));
            return NotFound();
        }

        [HttpGet("promoter")]
        public ActionResult<IEnumerable<string>> GettopicFromPromoter(Guid id)
        {
            var topics = _repository.GetTopicsFromPromoter(id);
            return Ok(topics);
        }

        [HttpPost]
        public ActionResult<ThesisReadDto> CreateTopic(ThesisCreateDto createTopic)
        {
            var thesisTopic = _mapper.Map<ThesisTopic>(createTopic);
            _repository.CreateTopic(thesisTopic);
            _repository.SaveChanges();

            var thesisReadDto = _mapper.Map<ThesisReadDto>(thesisTopic);
            return CreatedAtRoute(nameof(GetTopicById), new { Id = thesisReadDto.Id }, thesisReadDto);
        }

        [HttpPatch]
        public ActionResult PartialUpdateInvitation(Guid id, JsonPatchDocument<ThesisUpdateDto> json)
        {
            var topicFromRepo = _repository.GetTopicById(id);
            if (topicFromRepo == null)
                return NotFound();
            var invitationToPatch = _mapper.Map<ThesisUpdateDto>(topicFromRepo);
            json.ApplyTo(invitationToPatch, ModelState);
            if (!TryValidateModel(invitationToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(invitationToPatch, topicFromRepo);
            _repository.UpdateTopic(topicFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}
