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
        public DbSet<Promoter> Promoter { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promoter>().HasData(
                new Promoter
                {
                    Id = new System.Guid("f2f6784c-cac2-4949-b81d-63584314d797"),
                    Name = "Promotor1",
                    Surname = "Test",
                    Degrees = Enums.Degrees.dr,
                    Availability = 0,
                },
                new Promoter
                {
                    Id = new System.Guid("3b00f1d2-f701-4b63-9a22-5c3d602bf7d2"),
                    Name = "Promotor2",
                    Surname = "Test",
                    Degrees = Enums.Degrees.drHab,
                    Availability = 0,
                },
                new Promoter
                {
                    Id = new System.Guid("5a1298d3-c95b-4f0e-96d9-1b1881653777"),
                    Name = "Promotor3",
                    Surname = "Test",
                    Degrees = Enums.Degrees.prof,
                    Availability = 0,
                });
        }
    }
}