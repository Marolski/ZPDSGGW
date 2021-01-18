using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.Promoter;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Controllers
{
    [ApiController]
    [Route("api/promoter")]
    public class PromoterController : ControllerBase
    {
        private readonly ILogger<PromoterController> _logger;
        private readonly IRepositoryPromoter _repository;
        private readonly IMapper _mapper;

        public PromoterController(ILogger<PromoterController> logger, IRepositoryPromoter repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PromoterReadDto>> GetAllPromoters()
        {
            var promoters = _repository.GetAllPromoters();
            return Ok(_mapper.Map<IEnumerable<PromoterReadDto>>(promoters));
        }
        //Get api/proposals/{id}
        [HttpGet("{id}", Name = "GetPromoterById")]
        public ActionResult<PromoterReadDto> GetPromoterById(Guid id)
        {
            var promoter = _repository.GetPromoterById(id);
            if (promoter != null)
                return Ok(_mapper.Map<PromoterReadDto>(promoter));
            return NotFound();
        }
        //POST api/proposals
        [HttpPost]
        public ActionResult<PromoterReadDto> CreatePromoter(PromoterCreateDto proposalCreateDto)
        {
            var promoter = _mapper.Map<Promoter>(proposalCreateDto);
            _repository.CreatePromoter(promoter);
            _repository.SaveChanges();

            var promoterReadDto = _mapper.Map<PromoterReadDto>(promoter);

            return CreatedAtRoute(nameof(GetPromoterById), new { Id = promoterReadDto.Id }, promoterReadDto);
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
        public ActionResult PartialPromoterUpdate(Guid id, JsonPatchDocument<PromoterUpdateDto> json)
        {
            var promoterFromRepo = _repository.GetPromoterById(id);
            if (promoterFromRepo == null)
                return NotFound();
            var promoterToPatch = _mapper.Map<PromoterUpdateDto>(promoterFromRepo);
            json.ApplyTo(promoterToPatch, ModelState);
            if (!TryValidateModel(promoterToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(promoterToPatch, promoterFromRepo);
            _repository.UpdatePromoter(promoterFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        //DELETE api/proposals/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProposal(Guid id)
        {
            var PromoterFromRepo = _repository.GetPromoterById(id);
            if (PromoterFromRepo == null)
                return NotFound();
            _repository.DeletePromoter(PromoterFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
