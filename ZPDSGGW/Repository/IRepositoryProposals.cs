﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepositoryProposals
    {
        bool SaveChanges();
        IEnumerable<Proposal> GetProposals();
        Proposal GetProposalById(Guid id);
        Proposal GetProposalByStudentId(Guid id);
        Proposal GetProposalByPromoterId(Guid id);
        void CreateProposal(Proposal proposal);
        void UpdateProposal(Proposal proposal);
        void DeleteProposal(Proposal proposal);

    }
}
