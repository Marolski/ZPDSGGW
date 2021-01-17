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
        public DbSet<InvitationPromoter> InvitationPromoter { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<ThesisTopic> ThesisTopics { get; set; }
    }
}