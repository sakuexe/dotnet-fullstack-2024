using Microsoft.EntityFrameworkCore;

namespace financeapp.Data
{
    public class FinancesContext : DbContext
    {
        private readonly DbContextOptions<FinancesContext> _options;

        public FinancesContext (DbContextOptions<FinancesContext> options)
            : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // make the username and email unique
            modelBuilder.Entity<Models.User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Models.User>().HasIndex(u => u.Email).IsUnique();
        }

        public DbSet<financeapp.Models.User> Users { get; set; } = default!;
        public DbSet<financeapp.Models.Finance> Finances { get; set; } = default!;
    }
}
