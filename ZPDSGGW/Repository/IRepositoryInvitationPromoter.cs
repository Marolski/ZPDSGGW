using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepositoryInvitationPromoter
    {
        bool SaveChanges();
        void CreateInvitationToThePromoter(InvitationPromoter invitation);
        InvitationPromoter GetInvitation(Guid id);
        IEnumerable<InvitationPromoter> GetInvitations(Guid id);
        void UpdateInvitation(InvitationPromoter invitation);

    }
}
