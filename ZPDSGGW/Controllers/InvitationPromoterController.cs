using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.InvitationPromoter;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Controllers
{
    [ApiController]
    [Route("api/invitation")]
    public class InvitationPromoterController : ControllerBase
    {
        private readonly ILogger<InvitationPromoterController> _logger;
        private readonly IRepositoryInvitationPromoter _repository;
        private readonly IMapper _mapper;

        public InvitationPromoterController(IRepositoryInvitationPromoter repository, ILogger<InvitationPromoterController> logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        //Get api/invitation
        [HttpGet("{promoterSurname}", Name = "GetInvitationsByPromoterSurname")]
        public ActionResult<IEnumerable<InvitationPromoterReadDto>> GetAllInvitations(string promoterSurname)
        {
            var invitations = _repository.GetInvitations(promoterSurname);
            return Ok(_mapper.Map<IEnumerable<InvitationPromoterReadDto>>(invitations));
        }
        //Get api/invitation/{id}
        [HttpGet("{Id}", Name = "GetInvitationById")]
        public ActionResult<InvitationPromoterReadDto> GetInvitation(Guid id) 
        {
            var invitation = _repository.GetInvitation(id);
            if(invitation!=null)
                return Ok(_mapper.Map<InvitationPromoterReadDto>(invitation));
            return NotFound();
        }
        //POST api/anvitation
        [HttpPost]
        public ActionResult<InvitationPromoterReadDto> CreateInvitation(InvitationPromoterCreateDto invitation)
        {
            var newInvitation = _mapper.Map<InvitationPromoter>(invitation);
            _repository.CreateInvitationToThePromoter(newInvitation);
            _repository.SaveChanges();

            var invitationPromoterReadDto = _mapper.Map<InvitationPromoterReadDto>(newInvitation);
            return CreatedAtRoute(nameof(GetInvitation), new { Id = invitationPromoterReadDto.Id }, invitationPromoterReadDto);
        }
        //PATCH api/invitation/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateInvitation(Guid id, JsonPatchDocument<InvitationPromoterUpdateDto> json)
        {
            var invitationFromRepo = _repository.GetInvitation(id);
            if (invitationFromRepo == null)
                return NotFound();
            var invitationToPatch = _mapper.Map<InvitationPromoterUpdateDto>(invitationFromRepo);
            json.ApplyTo(invitationToPatch,ModelState);
            if (!TryValidateModel(invitationToPatch))
                return ValidationProblem(ModelState);
            _mapper.Map(invitationToPatch, invitationFromRepo);
            _repository.UpdateInvitation(invitationFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        ///////////////
        ///do napisania Mapowanie w InvitationProfile


    }
}
