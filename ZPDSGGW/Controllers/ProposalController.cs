using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Controllers
{
    [ApiController]
    [Route("api/proposals")]
    public class ProposalController: ControllerBase
    {
        private readonly ILogger<ProposalController> _logger;
        private readonly IRepositoryProposals _repository;
        private readonly IMapper _mapper;

        public ProposalController(ILogger<ProposalController> logger, IRepositoryProposals repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProposalReadDto>> GetAllProposals()
        {
            var proposals = _repository.GetProposals();
            return Ok(_mapper.Map<IEnumerable<ProposalReadDto>>(proposals));
        }
        //Get api/proposals/{id}
        [HttpGet("{id}", Name = "GetProposalByStudentId")]
        public ActionResult<ProposalReadDto> GetProposalByStudentId(Guid id)
        {
            var proposal = _repository.GetProposalByStudentId(id);
            return Ok(_mapper.Map<ProposalReadDto>(proposal));
        }

        //POST api/proposals
        [HttpPost]
        public ActionResult <ProposalReadDto> CreateProposal(ProposalCreateDto proposalCreateDto)
        {
            Guid id = new Guid();
            Console.WriteLine(id);
            proposalCreateDto.Id = id;
            var proposal = _mapper.Map<Proposal>(proposalCreateDto);
            _repository.CreateProposal(proposal);
            _repository.SaveChanges();

            var proposalReadDto = _mapper.Map<ProposalReadDto>(proposal);

            return CreatedAtRoute(nameof(GetProposalByStudentId), new { Id = proposalReadDto.Id }, proposalReadDto);
        }
        //PUT api/proposals/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProposal(Guid id, ProposalUpdateDto proposalUpdateDto)
        {
            var proposalFromRepo = _repository.GetProposalById(id);
            if (proposalFromRepo == null) 
                return NotFound();
            _mapper.Map(proposalUpdateDto, proposalFromRepo);
            _repository.UpdateProposal(proposalFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        //PATCH api/proposals/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialProposalUpdate(Guid id, JsonPatchDocument<ProposalUpdateDto> json)
        {
            Console.WriteLine(json);
            var proposalFromRepo = _repository.GetProposalById(id);
            if (proposalFromRepo == null)
                return NotFound();
            var proposalToPatch = _mapper.Map<ProposalUpdateDto>(proposalFromRepo);
            json.ApplyTo(proposalToPatch, ModelState);
            if (!TryValidateModel(proposalToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(proposalToPatch, proposalFromRepo);
            _repository.UpdateProposal(proposalFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        //DELETE api/proposals/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProposal(Guid id)
        {
            var proposalFromRepo = _repository.GetProposalById(id);
            if (proposalFromRepo == null)
                return NotFound();
            _repository.DeleteProposal(proposalFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
