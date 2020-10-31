using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Database;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Commands
{
    public class ProposalCommands : IRepository
    {
        private readonly ZPDSGGWContext _context;

        public ProposalCommands(ZPDSGGWContext context)
        {
            _context = context;
        }

        public void CreateProposal(Proposal proposal)
        {
            if (proposal==null)
                throw new ArgumentNullException(nameof(proposal));
            _context.Add(proposal);
        }

        public void DeleteProposal(Proposal proposal)
        {
            if (proposal == null)
                throw new ArgumentNullException(nameof(proposal));
            _context.Proposal.Remove(proposal);
        }

        public Proposal GetProposalById(Guid id)
        {
            return _context.Proposal.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Proposal> GetProposals()
        {
            return _context.Proposal.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateProposal(Proposal proposal)
        {
            //nothing
        }
    }
}
