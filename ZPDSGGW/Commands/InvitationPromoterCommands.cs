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

        public IEnumerable<InvitationPromoter> GetInvitations(Guid id)
        {
            var invitation = _context.InvitationPromoter.Where(x => x.PromoterId == id);
            if (invitation == null)
                throw new ArgumentNullException(nameof(invitation));
            return invitation;
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
