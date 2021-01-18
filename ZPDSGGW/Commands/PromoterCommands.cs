using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Database;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Commands
{
    public class PromoterCommands : IRepositoryPromoter
    {
        private readonly ZPDSGGWContext _context;

        public PromoterCommands(ZPDSGGWContext context)
        {
            _context = context;
        }
        public void CreatePromoter(Promoter promoter)
        {
            if (promoter == null)
                throw new ArgumentNullException(nameof(promoter));
            _context.Add(promoter);
        }

        public void DeletePromoter(Promoter promoter)
        {
            if (promoter == null)
                throw new ArgumentNullException(nameof(promoter));
            _context.Promoter.Remove(promoter);
        }

        public IEnumerable<Promoter> GetAllPromoters()=> _context.Promoter.ToList();

        public Promoter GetPromoterById(Guid id) => _context.Promoter.FirstOrDefault(x => x.Id == id);

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public void UpdatePromoter(Promoter promoter)
        {
            //nothing
        }
    }
}
