using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public class MockRepository : IRepositoryProposals
    {
        public Guid user1 = Guid.Parse("80e4ed42-e6e7-4fcc-b85e-c489b747beb3"); 
        public Guid user2 = Guid.Parse("d1dcd728-fa3d-457b-970d-c53e37b36b6c");
        public Guid user3 = Guid.Parse("ba4d69d4-da19-47f8-940c-25f3bfc86beb");

        public void CreateProposal(Proposal proposal)
        {
            throw new NotImplementedException();
        }

        public void DeleteProposal(Proposal proposal)
        {
            throw new NotImplementedException();
        }

        public Proposal GetProposalById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proposal> GetProposals()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateProposal(Proposal proposal)
        {
            throw new NotImplementedException();
        }
    }
}
