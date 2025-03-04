using Microsoft.EntityFrameworkCore;
using SelfHelpGroupAPI.Models;

namespace SelfHelpGroupAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
