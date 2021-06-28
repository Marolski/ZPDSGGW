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
        public DbSet<User> User { get; set; }
        public DbSet<Message> Message { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Proposal>().ToTable("Proposal");
        //    modelBuilder.Entity<InvitationPromoter>().ToTable("InvitationPromoter");
        //    modelBuilder.Entity<File>().ToTable("File");
        //    modelBuilder.Entity<ThesisTopic>().ToTable("ThesisTopic");
        //    modelBuilder.Entity<User>().ToTable("User");
        //    modelBuilder.Entity<Message>().ToTable("Message");
        //}
    }
}