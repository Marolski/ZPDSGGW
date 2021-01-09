using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Database;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Commands
{
    public class InvitationPromoterCommands : IRepositoryInvitationPromoter
    {
        private readonly ZPDSGGWContext _context;

        public InvitationPromoterCommands(ZPDSGGWContext context)
        {
            _context = context;
        }

        public void CreateInvitationToThePromoter(InvitationPromoter invitation)
        {
            if (invitation == null)
                throw new ArgumentNullException(nameof(invitation));
            _context.Add(invitation);
        }

        public InvitationPromoter GetInvitation(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            return _context.InvitationPromoter.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<InvitationPromoter> GetInvitations(string promoterSurname)
        {
            if (promoterSurname == null || promoterSurname == "")
                throw new ArgumentNullException(promoterSurname);
            var invitations = _context.InvitationPromoter;
            var invitationsFound = new List<InvitationPromoter>();
            foreach (var invitation in invitations)
            {
                if (invitation.PromoterSurname == promoterSurname)
                    invitationsFound.Add(invitation);
            }
            return invitationsFound.ToList();
        }

        public void UpdateInvitation(InvitationPromoter invitation)
        {
            //nothing
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
