using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepository
    {
        IEnumerable<Proposal> GetProposals();
        Proposal GetProposalById(Guid id);
    }
}
