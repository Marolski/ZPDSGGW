using Microsoft.EntityFrameworkCore;
using ZPDSGGW.Models;

namespace ZPDSGGW.Database
{
    public class ZPDSGGWContext : DbContext
    {
        public ZPDSGGWContext(DbContextOptions<ZPDSGGWContext> opt) : base(opt)
        {

        }
        public DbSet<Proposal> Commands { get; set; }
    }
}