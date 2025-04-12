using aprendizahem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace aprendizahem.Data
 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
