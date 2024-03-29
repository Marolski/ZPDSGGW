﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.DTOs.InvitationPromoter;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;
using ZPDSGGW.Services;

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
        [Authorize(Roles = Roles.Student + "," + Roles.Promoter)]
        [HttpGet]
        public ActionResult<IEnumerable<InvitationPromoterReadDto>> GetAllInvitations(Guid id)
        {
            var invitations = _repository.GetInvitations(id);
            if(invitations!=null)
                return Ok(_mapper.Map<IEnumerable<InvitationPromoterReadDto>>(invitations));
            return NotFound();
        }
        //Get api/invitation/{id}
        //get by student id
        [Authorize(Roles = Roles.Student + "," + Roles.Promoter)]
        [HttpGet("{id}", Name = "GetInvitation")]       
        public ActionResult<InvitationPromoterReadDto> GetInvitationByUserId(Guid id) 
        {
            var invitation = _repository.GetInvitation(id);
            return Ok(_mapper.Map<InvitationPromoterReadDto>(invitation));
        }
        //POST api/anvitation
        [Authorize(Roles = Roles.Student + "," + Roles.Promoter)]
        [HttpPost]
        public ActionResult<InvitationPromoterReadDto> CreateInvitation(InvitationPromoterCreateDto invitation)
        {
            Guid id = Guid.NewGuid();
            invitation.Id = id;
            var newInvitation = _mapper.Map<InvitationPromoter>(invitation);
            _repository.CreateInvitationToThePromoter(newInvitation);
            _repository.SaveChanges();

            var invitationPromoterReadDto = _mapper.Map<InvitationPromoterReadDto>(newInvitation);
            return CreatedAtRoute("GetInvitation", new { Id = invitationPromoterReadDto.StudentId }, invitationPromoterReadDto);
        }
        //PATCH api/invitation/{id}
        [Authorize(Roles = Roles.Student + "," + Roles.Promoter)]
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



    }
}
