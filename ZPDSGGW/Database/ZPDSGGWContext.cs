using Microsoft.EntityFrameworkCore;
using ZPDSGGW.Models;

namespace ZPDSGGW.Database
{
    public class ZPDSGGWContext : DbContext
    {
        public ZPDSGGWContext(DbContextOptions<ZPDSGGWContext> opt) : base(opt)
        {

        }
        public DbSet<Proposal> Proposal { get; set; }
        public DbSet<InvitePromoter> InvitePromoter { get; set; }
    }
}