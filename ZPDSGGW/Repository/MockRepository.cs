using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public class MockRepository : IRepository
    {
        public Guid user1 = Guid.Parse("80e4ed42-e6e7-4fcc-b85e-c489b747beb3"); 
        public Guid user2 = Guid.Parse("d1dcd728-fa3d-457b-970d-c53e37b36b6c");
        public Guid user3 = Guid.Parse("ba4d69d4-da19-47f8-940c-25f3bfc86beb");
        
        public Proposal GetProposalById(Guid id)
        {
            return new Proposal { Date = DateTime.UtcNow, Degree = Degrees.inz, Id = user1, Name = "Marcin", Surname = "Testowy", PromoterName = "Andrew", PromoterSurname = "Skrzat" };
        }

        public IEnumerable<Proposal> GetProposals()
        {
            var proposals = new List<Proposal>
            {
                new Proposal { Date = DateTime.UtcNow, Degree = Degrees.inz, Id = user1, Name="Marcin", Surname="Testowy1", PromoterName = "Andrew", PromoterSurname= "Skrzat1"},
                new Proposal { Date = DateTime.UtcNow, Degree = Degrees.mgr, Id = user2, Name="Kacper", Surname="Testowy2", PromoterName = "Maciej", PromoterSurname= "Skrzat2"},
                new Proposal { Date = DateTime.UtcNow, Degree = Degrees.dr, Id = user3, Name="Wojtek", Surname="Testowy3", PromoterName = "Zbyszek", PromoterSurname= "Skrzat3"}
            };
            return proposals;
        }
    }
}
