using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepositoryPromoter
    {
        void CreatePromoter(Promoter promoter);
        IEnumerable<Promoter> GetAllPromoters();
        Promoter GetPromoterById(Guid id);
        void UpdatePromoter(Promoter promoter);
        void DeletePromoter(Promoter promoter);
        bool SaveChanges();

    }
}
