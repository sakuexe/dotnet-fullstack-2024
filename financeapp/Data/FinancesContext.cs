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

        public DbSet<financeapp.Models.User> Users { get; set; } = default!;
        public DbSet<financeapp.Models.Finance> Finances { get; set; } = default!;
    }
}
