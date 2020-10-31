using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Controllers
{
    [ApiController]
    [Route("api/proposals")]
    public class ProposalController: ControllerBase
    {
        private readonly ILogger<ProposalController> _logger;
        private readonly IRepository _repository;
        public ProposalController(ILogger<ProposalController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Proposal>> GetAllProposals()
        {
            var proposals = _repository.GetProposals();
            return Ok(proposals);
        }
        //Get api/proposals/{id}
        [HttpGet("{id}")]
        public ActionResult <Proposal> GetProposalById(Guid id)
        {
            var proposal = _repository.GetProposalById(id);
            return Ok(proposal);
        }

    }
}
