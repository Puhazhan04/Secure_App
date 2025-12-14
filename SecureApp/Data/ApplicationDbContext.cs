using Microsoft.EntityFrameworkCore;
using SecureApp.Models;

namespace SecureApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Abwesenheit> Abwesenheiten { get; set; }
    }
}
