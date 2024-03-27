using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using efwebtutorial.Models;

namespace efwebtutorial.Data
{
    public class EfWebTutorialContext : DbContext
    {
        public EfWebTutorialContext (DbContextOptions<EfWebTutorialContext> options)
            : base(options)
        {
        }

        public DbSet<efwebtutorial.Models.Movie> Movie { get; set; } = default!;
    }
}
