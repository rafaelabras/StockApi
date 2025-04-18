using aprendizahem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace aprendizahem.Data
 
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
     : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
