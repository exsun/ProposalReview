using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Scoring> Scorings { get; set; }
        public DbSet<Bonus> Bonuses { get; set; }
    }
}
